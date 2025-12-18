using System;
using System.Collections.Generic;
using System.Linq;
using WorldOfZuul.Data;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Domain
{
    public class DataHandler
    {
        public static DataHandler? Instance { get; private set; }

        public List<Room> Rooms { get; private set; }
        public List<Villager> Villagers { get; private set; }
        public Resources Resources { get; private set; }
        public List<Job> Jobs { get; private set; }
        public Advisor Advisor { get; private set; } = new Advisor();

        public Room CurrentRoom { get; private set; }
        public int CurrentDay { get; private set; }
        public int TurnsThisDay { get; private set; }
        public int TotalTurns { get; private set; }
        public int t;

        private const int MaxTurnPerDay = 10;
        private const int MaxDay = 10;
        private bool _continuePlaying = true;
        private bool _inAdvisorChat = false;

        public bool ContinuePlaying => _continuePlaying;

        private static readonly HashSet<string> TurnRoomCommands = new(StringComparer.OrdinalIgnoreCase)
        {
            "feed",
            "assign",
            "build",
            "cut",
            "plant",
            "kill",
            "hunt",
            "farm",
            "harvest",
            "cook",
            "catch",
            "learn"
        };

        public DataHandler(IDataInitializer dataInitializer)
        {
            Instance = this;

            Rooms = dataInitializer.rooms;
            Villagers = dataInitializer.villagers;
            Resources = dataInitializer.resources;
            Jobs = dataInitializer.jobs;

            CurrentRoom = FindRoomByName("Village") ?? Rooms.First();
            CurrentDay = 1;
            TurnsThisDay = 0;
            TotalTurns = 0;
        }

        public string HandleCommand(Command command)
        {
            if (command == null)
            {
                return "Invalid command.";
            }

            string ui;

            switch (command.Name)
            {
                case "ls":
                    ui = HandleList(command.SecondWord);
                    t++;
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
                    ui = CurrentRoom.RoomCommandHandler(command, Resources);
                    t++;
                    if (TurnRoomCommands.Contains(command.Name))
                    {
                        NextTurn();
                    }
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
                result += $"ID: {villager.Id} | Name: {villager.Name} | Hunger: {villager.Hunger} | CanWork: {villager.CanWork} | Job: {job?.Name ?? "Unemployed"}\n";
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
            Room? room = FindRoomByName(roomName);
            if (room == null)
            {
                t++;
                return "Please specify a valid room name.";
            }

            CurrentRoom = room;
            Console.Clear();
            return CurrentRoom.GetEnterRoomMessage();
        }

        private string HandleSleep()
        {
            int turnsLeft = MaxTurnPerDay - TurnsThisDay;
            int sleptDay = CurrentDay;

            NextTurn(turnsLeft);

            return $"You slept through the rest of day {sleptDay}.\n" +
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

        public Room? FindRoomByName(string? roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName)) return null;

            return Rooms.FirstOrDefault(r =>
                string.Equals(r.ShortDescription, roomName, StringComparison.OrdinalIgnoreCase));
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
                if (job.Villagers?.Any(v => v.Id == villager.Id) == true)
                {
                    return job;
                }
            }
            return null;
        }

        public void NextTurn(int turns = 1)
        {
            if (turns <= 0) return;

            var village = Rooms.FirstOrDefault(r => r is Village) as Village;
            var farmland = Rooms.FirstOrDefault(r => r is Farmland) as Farmland;

            for (int i = 0; i < turns; i++)
            {
                if (!_continuePlaying) break;

                TurnsThisDay++;
                TotalTurns++;

                village?.FoodLoss();
                farmland?.RipenFarmland(TotalTurns);
                Resources.TurnToTrees(TotalTurns);

                if (TurnsThisDay >= MaxTurnPerDay)
                {
                    CurrentDay++;
                    TurnsThisDay = 0;

                    if (CurrentDay > MaxDay)
                    {
                        _continuePlaying = false;
                        break;
                    }
                }
            }
        }
    }
}
