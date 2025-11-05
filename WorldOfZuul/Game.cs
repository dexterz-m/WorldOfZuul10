using WorldOfZuul.Jobs;
using WorldOfZuul.RoomType;

namespace WorldOfZuul
{
    public class Game
    {
        private readonly List<Room?> _rooms;
        private readonly List<Villager> _villagers;
        private Room? _currentRoom;
        private int _currentDay;
        //TODO: implement turn system
        //private int _currentTurn = 0;
        //private const int MaxTurnPerDay = 10;
        private const int MaxDay = 10;
        private bool _continuePlaying = true; // moved to field so rooms can change it via requests
        public static Resources resources;

        // Advisor NPC
        private readonly Advisor advisor = new();

        //Sustainability variable
        private int _sustainability;

        // Global sustainability points defined in Game (static so accessible from Room)
        public static int SustainabilityPoints { get; set; } = 10;

        public Game()
        {
            _rooms = new List<Room?>();
            CreateRooms();
            _villagers = new List<Villager>();
            CreateVillagers();
            resources = new Resources();
        }

        private void CreateRooms()
        {
            
            Farmland farmlandMain = new("Farmland", "(Placeholder farmlandMain)");
            Forest forest = new("Forest", "(Placeholder forest)");
            Village village = new("Village", "(Placeholder village)");
            Lake lake = new("Lake", "(Placeholder lake)");
            School school = new("School", "(Placeholder school)");


            _rooms.Add(village);
            _rooms.Add(forest);
            _rooms.Add(farmlandMain);
            _rooms.Add(lake);
            _rooms.Add(school);
            
            _currentRoom = _rooms[0];
        }

        private void CreateVillagers()
        {
            var job = _rooms[0].Job;
            if (job == null) return;
            var v1 = new Villager(0, "asd");
            _villagers.Add(v1);
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();
            Console.WriteLine($"You are starting in the {_currentRoom?.ShortDescription}");
            _currentRoom?.EnterRoom();

            while (_continuePlaying && _currentDay <= MaxDay)
            {
                Console.WriteLine(_currentRoom?.ShortDescription);
                RoomInfo(_currentRoom!.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

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
                        resources.Hunger = -35;
                        Console.WriteLine($"Day advanced to {_currentDay}.");
                        break;
                    case "quit":
                        _continuePlaying = false;
                        break;
                    case "assign":
                        AssignVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                        break;
                    case "feed":
                        resources.Food = - 1;
                        resources.Hunger = 50;
                        break;
                    case "hunt":
                        resources.Food = 1;
                        resources.Animals = -1;
                        _sustainability -= 5;
                        break;
                    case "farm":
                        resources.GrainSeeds = - 1;
                        break;
                    case "harvest":
                        resources.Grains = 1;
                        _sustainability -= 5;
                        break;
                    case "chop":
                        resources.Wood = 1;
                        resources.Saplings = 2;
                        resources.Trees = -1;
                        _sustainability -= 5;
                        break;
                    case "plant":
                        resources.Saplings = -1;
                        _sustainability += 10;
                        break;
                    case "cook":
                        resources.Grains = -1;
                        resources.Food = 1;
                        break;
                    case "talk":
                        advisor.Talk();
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

            Console.WriteLine("Thank you for playing World of Zuul!");
        }

        private void ChangeRoom(string? nameString)
        {

            Console.Clear();
            int id = -1;
            
            foreach (Room rName in _rooms!)
            {
                if (nameString?.ToLower() == rName!.ShortDescription.ToLower())
                {
                    id = _rooms.IndexOf(rName);
                }
            }

            if (id != -1 && id < _rooms.Count)
            {
                _currentRoom = _rooms[id];
                
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
                    Console.WriteLine("Fishes: 7");
                    Console.WriteLine("");
                    Console.WriteLine("catch Fish");
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



        private void List(char? type)
        {
            switch (type)
            {
                case 'v':
                    foreach (var villager in _villagers)
                    {
                        Console.WriteLine($"{villager.Id} | {villager.Name}");
                    }
                    break;
                case 'j':
                    Console.WriteLine("Jobs");
                    break;
                case 'r':
                    foreach (Room roomName in _rooms!)
                    {
                        Console.WriteLine(roomName!.ShortDescription);
                    }
                    break;
                case 'i':
                    Console.WriteLine($"Food : {resources.Food}");
                    Console.WriteLine($"Hunger : {resources.Hunger}");
                    Console.WriteLine($"Saplings : {resources.Saplings}");
                    Console.WriteLine($"Animals : {resources.Animals}");
                    Console.WriteLine($"Grains : {resources.Grains}");
                    Console.WriteLine($"GrainSeeds : {resources.GrainSeeds}");
                    Console.WriteLine($"Trees : {resources.Trees}");
                    Console.WriteLine($"Wood : {resources.Wood}");
                    break;
                default:
                    Console.WriteLine("Wrong command! Try 'help' to see syntax.");
                    break;
            }
        }

        private void AssignVillager(int villagerId, int jobId)
        {
            foreach (var r in _rooms.Where(r => r is { Job: not null } && r.Job.Id  == jobId)) 
            {
                r?.Job?.AddVillager(_villagers[villagerId]);
            }
        }
    }
}
