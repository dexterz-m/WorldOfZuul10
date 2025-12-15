using System.Collections.Generic;
using WorldOfZuul.Data;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Domain;

public class DataHandler
{
    public List<Room> Rooms { get; private set; }
    public List<Villager> Villagers { get; private set; }
    public Resources Resources { get; private set; }
    public List<Job> Jobs { get; private set; }
    public Advisor Advisor { get; private set; } = new Advisor();

    public Room CurrentRoom { get; private set; }
    public int CurrentDay { get; private set; }
    public int TurnsThisDay { get; private set; }
    public int TotalTurns { get; private set; }
    
    private const int MaxTurnPerDay = 10;
    private const int MaxDay = 10;
    private bool _continuePlaying = true;
    private bool _inAdvisorChat = false;

    // Parameter changed for Game.cs
    public DataHandler(IDataInitializer dataInitializer)
    {
        Rooms = dataInitializer.rooms;
        Villagers = dataInitializer.villagers;
        Resources = dataInitializer.resources;
        Jobs = dataInitializer.jobs;
        CurrentRoom = FindRoomByName("Village");
        CurrentDay = 1;
        TurnsThisDay = 0;
        TotalTurns = 0;
    }

    // Main UI connector - single point of contact
    public string HandleCommand(Command command)
    {
        string ui;

        if (command == null)
        {
            return "Invalid command.";
        }

        switch (command.Name)
        {
            case "ls":
                ui = HandleList(command.SecondWord);
                break;
            case "cd":
                ui = HandleChangeRoom(command.SecondWord);
                break;
            case "sleep":
                ui = HandleSleep();
                break;
            case "quit":
                ui = HandleQuit();
                break;
            case "talk":
                ui = HandleTalk();
                break;
            default:
                // Not a global command: pass it to the current room to handle
                ui = CurrentRoom.RoomCommandHandler(command, Resources);
                break;
        }
        return ui;
    }

    private string HandleList(string? type)
    {
        if (string.IsNullOrEmpty(type))
        {
            return "Please specify what to list: 'v' for villagers, 'j' for jobs, 'r' for rooms, 'i' for resources.";
        }

        return type.ToLower() switch
        {
            "v" => ListVillagers(),
            "j" => ListJobs(),
            "r" => ListRooms(),
            "i" => ListResources(),
            _ => "Unknown list type. Try: 'v' (villagers), 'j' (jobs), 'r' (rooms), 'i' (resources)."
        };
    }

    private string ListVillagers()
    {
        string result = "";
        foreach (var villager in Villagers)
        {
            var job = FindVillagerJob(villager);
            result += $"ID: {villager.Id} | Name: {villager.Name} | Job: {job?.Name ?? "Unemployed"}\n";
        }
        return result;
    }

    private string ListJobs()
    {
        string result = "";
        foreach (var job in Jobs)
        {
            result += $"ID: {job?.Id} | {job?.Name} | {job?.Description} | Assigned Villagers: {job?.Villagers?.Count ?? 0}\n";
        }
        return result;
    }

    private string ListRooms()
    {
        string result = "";
        foreach (Room room in Rooms)
        {
            result += $"{room.ShortDescription}\n";
        }
        return result;
    }

    private string ListResources()
    {
        return $"Sustainability Points: {Resources.SustainabilityPoints}\n" +
               $"Food: {Resources.Food}\n" +
               $"Hunger: {Resources.Hunger}\n" +
               $"Trees: {Resources.Trees}\n" +
               $"Animals: {Resources.Animals}\n" +
               $"Wood: {Resources.Wood}\n" +
               $"Saplings: {Resources.Saplings}\n" +
               $"GrainSeeds: {Resources.GrainSeeds}\n";
    }

    private string HandleChangeRoom(string? roomName)
    {
        try
        {
            Room room = FindRoomByName(roomName);
            if (room == null)
            {
                return "Please specify a valid room name.";
            }
            CurrentRoom = room;
            NextTurn();
            return CurrentRoom.GetEnterRoomMessage();
        }
        catch (Exception ex)
        {
            return $"Could not change room: {ex.Message}";
        }
    }

    private string HandleSleep()
    {
        int turnsLeft = MaxTurnPerDay - TurnsThisDay;
        NextTurn(turnsLeft);
        CurrentDay++;
        TurnsThisDay = 0;
        
        return $"You slept through the rest of day {CurrentDay - 1}.\n" +
               $"It is now Day {CurrentDay}.\n" +
               $"Sustainability Points: {Resources.SustainabilityPoints}\n" +
               ListResources();
    }

    private string HandleQuit()
    {
        _continuePlaying = false;
        return "Thank you for playing! Final score:\n" + 
               $"Days survived: {CurrentDay}\n" +
               $"Sustainability Points: {Resources.SustainabilityPoints}\n" +
               ListResources();
    }

    private string HandleTalk()
    {
        _inAdvisorChat = true;
        NextTurn();
        
        if (!Advisor.IsIntroduced)
        {
            Advisor.MarkAsIntroduced();
            return Advisor.GetIntroduction() + "\n\n" + Advisor.GetHelpText();
        }
        
        return "You are talking with the advisor. What would you like to know?\n" + Advisor.GetHelpText();
    }

    public Room FindRoomByName(string roomName)
    {
        int id = -1;

        foreach (var rName in Rooms!.Where(rName => roomName?.ToLower() == rName!.ShortDescription.ToLower()))
        {
            id = Rooms.IndexOf(rName);
        }

        if (id != -1 && id < Rooms.Count)
        {
            return Rooms[id];
        }
        else
        {
            return null;
        }
    }

    public Villager? FindVillagerById(int id)
    {
        return Villagers.Find(v => v.Id == id);
    }

    public Job? FindJobById(int id)
    {
        return Jobs.Find(j => j.Id == id);
    }

    public Job? FindVillagerJob(Villager villager)
    {
        foreach (var job in Jobs)
        {
            if (job.Villagers?.Contains(villager) == true)
            {
                return job;
            }
        }
        return null;
    }

    public void NextTurn(int turns = 1)
    {
        TurnsThisDay += turns;
        TotalTurns += turns;
        
        // Process turn-based events
        Resources.Hunger += turns * 5; // Hunger increases each turn
        
        // Apply job work
        foreach (var job in Jobs)
        {
            if (job.Villagers?.Count > 0)
            {
                job.Work();
            }
        }
        
        // Process farmland ripening
        var farmland = Rooms.FirstOrDefault(r => r is Farmland) as Farmland;
        farmland?.RipenFarmland(TotalTurns);
        
        // Check for game over conditions
        if (CurrentDay >= MaxDay)
        {
            _continuePlaying = false;
        }
        
    }

}