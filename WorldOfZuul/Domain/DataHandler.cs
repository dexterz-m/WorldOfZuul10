using System.Collections.Generic;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Domain;

public class DataHandler
{
    public static List<Room> Rooms = new List<Room?>();
    public static List<Villager> Villagers = new List<Villager?>();
    public static Resources Resources = new Resources();
    public static List<Job> Jobs = new List<Job?>();
    public static Advisor Advisor = new Advisor();
    
    public static Room CurrentRoom { get; private set; }

    private int CurrentDay { get; set; }
    public static int TurnsThisDay { get; set; }
    public static int TotalTurns { get; set; }
    private const int MaxTurnPerDay = 10;
    private const int MaxDay = 10;
    private bool _continuePlaying = true; // moved to field so rooms can change it via requests

    public DataHandler(List<Room> rooms, List<Villager> villagers, Resources resources, Advisor advisor, List<Job> jobs)
    {
        Rooms = rooms;
        Villagers = villagers;
        Resources = resources;
        Advisor = advisor;
        Jobs = jobs;
        CurrentRoom = FindRoomByName("Village");
    }

    public void ChangeRoom(string roomName)
    {
        Room room = FindRoomByName(roomName);
        CurrentRoom = room;
    }

    public Room FindRoomByName(string roomName)
    {
        int id = -1;

        foreach (Room rName in Rooms!)
        {
            if (roomName?.ToLower() == rName!.ShortDescription.ToLower())
            {
                id = Rooms.IndexOf(rName);
            }
        }

        return Rooms[id];
    }



    public Villager? FindVillagerById(int id)
    {
        return Villagers.Find(v => v.Id == id);
    }

    public Job? FindJobById(int id)
    {
        return Jobs.Find(j => j.Id == id);
    }

    private void AssignVillager(int villagerId, int jobId)
    {
        //var villager = Villagers?.FirstOrDefault(villager => villager.Id == villagerId);
        //if (villager == null)
        //{
        //    Console.WriteLine($"No villager with ID {villagerId} found.");
        //    return;
        //}

        //Job? targetJob = null;
        //foreach (var room in Rooms)
        //{
        //    if (room?.Jobs == null) continue;
        //    foreach (var job in room.Jobs.OfType<Job>().Where(job => job.Id == jobId))
        //    {
        //        targetJob = job;
        //    }
        //    if (targetJob != null) break;
        //}

        //if (targetJob == null)
        //{
        //    Console.WriteLine($"No job with ID {jobId} found.");
        //    return;
        //}

        //foreach (var room in Rooms)
        //{
        //    if (room?.Jobs == null) continue;
        //    foreach (var job in room.Jobs)
        //    {
        //        job?.Villagers?.Remove(villager);
        //    }
        //}

        //if (targetJob.Villagers != null && targetJob.Villagers.Contains(villager))
        //{
        //    Console.WriteLine($"Villager with ID {villagerId} already assigned to {targetJob.Name}.");
        //    return;
        //}

        //targetJob.AddVillager(villager);
    }

    string[] GetUiData(Command command)
    {
        switch (command.Name)
        {
            case "ls":
                List(command.SecondWord == null ? null : Convert.ToChar(command.SecondWord));
                break;
            case "cd":
                ChangeRoom(command.SecondWord);
                break;
            case "sleep":
                var turnLeft = MaxTurnPerDay - TurnsThisDay;
                Console.Clear();
                NextTurn(turnLeft);
                break;
            case "quit":
                _continuePlaying = false;
                break;
            case "talk":
                _advisor.Talk();
                NextTurn();
                break;
            default:
                // Not a global command: pass it to the current room to handle
                _currentRoom?.CommandList(command);
                break;
        }

        return new string[] { }; // to do
    }

    public IEnumerable<Job> GetAllJobs()
    {
        return Jobs;
    }
    public IEnumerable<Room> GetAllRooms()
    {
        return Rooms;
    }
    public IEnumerable<Villager> GetAllVillagers()
    {
        return Villagers;
    }

    public void Sleep()
    {
        var turnLeft = MaxTurnPerDay - TurnsThisDay;
        Console.Clear();
        NextTurn(turnLeft);
    }

    public static void NextTurn(int turns = 1)
    {
        TurnsThisDay += turns;
        Farmland? farm = Rooms.FirstOrDefault(room => room?.ShortDescription == "Farmland") as Farmland;
        Village? village = Rooms.FirstOrDefault(room => room?.ShortDescription == "Village") as Village;
        for (int i = 0; i < turns; i++)
        {
            village?.FoodLoss();
            farm?.RipenFarmland();
            Resources.TurnToTrees();
        }
    }
}