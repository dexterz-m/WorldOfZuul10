using WorldOfZuul.Jobs;
using WorldOfZuul.RoomType;

namespace WorldOfZuul
{
    public class Game
    {
        public static readonly List<Room?> Rooms  = new List<Room?>();
        private static readonly List<Villager>? Villagers = new List<Villager>();
        public static readonly Resources Resources= new Resources();
        public static int SustainabilityPoints { get; set; } = 10;
        public static int CurrentTurn {get; set;}
        
        private Room? _currentRoom;
        private int _currentDay;
        private const int MaxTurnPerDay = 10;
        private const int MaxDay = 10;
        private bool _continuePlaying = true; // moved to field so rooms can change it via requests

        // Advisor NPC
        private readonly Advisor _advisor = new();

        //Sustainability variable
        private int _sustainability;

        // Global sustainability points defined in Game (static so accessible from Room)

        public Game()
        {
            CreateRooms();
            CreateVillagers();
        }

        private void CreateRooms()
        {
            
            Farmland farmlandMain = new("Farmland", "(Placeholder farmlandMain)");
            Forest forest = new("Forest", "(Placeholder forest)");
            Village village = new("Village", "(Placeholder village)");
            Lake lake = new("Lake", "(Placeholder lake)");
            School school = new("School", "(Placeholder school)");


            Rooms.Add(village);
            Rooms.Add(forest);
            Rooms.Add(farmlandMain);
            Rooms.Add(lake);
            Rooms.Add(school);
            
            _currentRoom = Rooms[0];
        }

        private void CreateVillagers()
        {
            var job = Rooms[0]?.Jobs;
            if (job == null) return;
            var v1 = new Villager(0, "asd");
            Villagers?.Add(v1);
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();
            Console.WriteLine($"You are starting in the {_currentRoom?.ShortDescription}");
            _currentRoom?.EnterRoom();

            while (_continuePlaying && _currentDay <= MaxDay)
            {
                CurrentTurn = 0;
                while (_continuePlaying && CurrentTurn <= MaxTurnPerDay)
                {
                    Console.WriteLine(_currentRoom?.ShortDescription);
                    RoomInfo(_currentRoom!.ShortDescription);
                    Console.Write("> ");

                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Please enter a command.");
                        continue;
                    }

                    var command = parser.GetCommand(input);

                    if (command == null)
                    {
                        Console.WriteLine("I don't know that command.");
                        continue;
                    }

                    // Handle global commands here so they work from any room
                    switch (command.Name)
                    {
                        case "ls":
                            List(command.SecondWord == null ? null : Convert.ToChar(command.SecondWord));
                            break;
                        case "cd":
                            ChangeRoom(command.SecondWord);
                            break;
                        case "sleep":
                            _currentDay++;
                            Resources.Hunger = -35;
                            Console.WriteLine($"Day advanced to {_currentDay}.");
                            break;
                        case "quit":
                            _continuePlaying = false;
                            break;
                        case "assign":
                            AssignVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                            CurrentTurn++;
                            break;
                        case "feed":
                            Resources.Food = - 1;
                            Resources.Hunger = 50;
                            CurrentTurn++;
                            break;
                        case "hunt":
                            Resources.Food = 1;
                            Resources.Animals = -1;
                            _sustainability -= 5;
                            break;
                        case "farm":
                            Resources.GrainSeeds = - 1;
                            break;
                        case "harvest":
                            Resources.Grains = 1;
                            _sustainability -= 5;
                            break;
                        case "chop":
                            Resources.Wood = 1;
                            Resources.Saplings = 2;
                            Resources.Trees = -1;
                            _sustainability -= 5;
                            break;
                        case "plant":
                            Resources.Saplings = -1;
                            _sustainability += 10;
                            break;
                        case "cook":
                            Resources.Grains = -1;
                            Resources.Food = 1;
                            break;
                        case "talk":
                            _advisor.Talk();
                            break;
                        default:
                            // Not a global command: pass it to the current room to handle
                            _currentRoom?.CommandList(command);
                            break;
                    }


                    // Prevent SustainabilityPoints from going negative
                    if (SustainabilityPoints < 0)
                    {
                        Console.WriteLine($"You lost");
                        _continuePlaying = false;
                        // end of the game
                    }
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }

        private void ChangeRoom(string? nameString)
        {
            Console.Clear();
            int id = -1;
            
            foreach (Room rName in Rooms!)
            {
                if (nameString?.ToLower() == rName!.ShortDescription.ToLower())
                {
                    id = Rooms.IndexOf(rName);
                }
            }

            if (id != -1 && id < Rooms.Count)
            {
                _currentRoom = Rooms[id];
                
            }
            else
            {
                Console.WriteLine("No room with this name! Try again or see 'help' for syntax.");
            }

            Console.WriteLine("You have entered the " + _currentRoom?.ShortDescription);
            _currentRoom?.EnterRoom();
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            //PrintHelp();

        }
        private static void RoomInfo(string shortDesc)
        {
            switch (shortDesc)
            {
                case "Forest":
                    Console.WriteLine("Trees: 30"); // placeholders for roominfos and potential commands
                    Console.WriteLine("Animals: 10"); 
                    Console.WriteLine("");
                    Console.WriteLine("cut tree");
                    Console.WriteLine("plant tree");
                    Console.WriteLine("kill animal");
                    Console.WriteLine("");
                    break;
                case "Village":
                    Console.WriteLine("Villigers: 20");
                    Console.WriteLine("Houses: 5");
                    Console.WriteLine("");
                    Console.WriteLine("feed -[VILLAGER ID] [AMOUNT/DAY]       Feeds villager and activates it");
                    Console.WriteLine("assign -[VILLAGER ID] [JOB NAME]       Assigns villager to a task");
                    Console.WriteLine("");
                    break;
                case "Lake":
                    Console.WriteLine("");
                    Console.WriteLine("Catch Fish");
                    Console.WriteLine("Feed Fish");
                    Console.WriteLine("");
                    break;
                case "School":
                    Console.WriteLine("Read about sustainability");
                    Console.WriteLine("");
                    break;
                case "FarmlandMain":
                    Console.WriteLine("Farmlands: 1");
                    Console.WriteLine("");
                    Console.WriteLine("Build framland");
                    Console.WriteLine("Cut down forest for farmland");
                    Console.WriteLine("");
                    break;
            }
            
            
        }



        private static void List(char? type)
        {
            switch (type)
            {
                case 'v':
                    if (Villagers != null)
                        foreach (var villager in Villagers)
                        {
                            Console.WriteLine($"{villager.Id} | {villager.Name}");
                        }

                    break;
                case 'j':
                    Console.WriteLine("Jobs");
                    break;
                case 'r':
                    foreach (Room roomName in Rooms!)
                    {
                        Console.WriteLine(roomName!.ShortDescription);
                    }
                    break;
                case 'i':
                    Console.WriteLine($"Food : {Resources.Food}");
                    Console.WriteLine($"Hunger : {Resources.Hunger}");
                    Console.WriteLine($"Saplings : {Resources.Saplings}");
                    Console.WriteLine($"Animals : {Resources.Animals}");
                    Console.WriteLine($"Grains : {Resources.Grains}");
                    Console.WriteLine($"GrainSeeds : {Resources.GrainSeeds}");
                    Console.WriteLine($"Trees : {Resources.Trees}");
                    Console.WriteLine($"Wood : {Resources.Wood}");
                    break;
                default:
                    Console.WriteLine("Wrong command! Try 'help' to see syntax.");
                    break;
            }
        }

        private void AssignVillager(int villagerId, int jobId)
        {
            var villager = Villagers?.FirstOrDefault(villager => villager.Id == villagerId);
            if (villager == null)
            {
                Console.WriteLine($"No villager with ID {villagerId} found.");
                return;
            }

            Job? targetJob = null;
            foreach (var room in Rooms)
            {
                if (room?.Jobs == null) continue;
                foreach (var job in room.Jobs.OfType<Job>().Where(job => job.Id == jobId))
                {
                    targetJob = job;
                }
                if (targetJob != null) break;
            }

            if (targetJob == null)
            {
                Console.WriteLine($"No job with ID {jobId} found.");
                return;
            }

            foreach (var room in Rooms)
            {
                if (room?.Jobs == null) continue;
                foreach (var job in room.Jobs)
                {
                    job?.Villagers?.Remove(villager);
                }
            }

            if (targetJob.Villagers != null && targetJob.Villagers.Contains(villager))
            {
                Console.WriteLine($"Villager with ID {villagerId} already assigned to {targetJob.Name}.");
                return;
            }
            
            targetJob.AddVillager(villager);
        }
    }
}
