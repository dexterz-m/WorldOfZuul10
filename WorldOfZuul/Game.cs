using WorldOfZuul.RoomType;

namespace WorldOfZuul
{
    public class Game
    {
        private readonly List<Room?> _rooms;
        private readonly List<Villager> _villagers;
        private Room? _currentRoom;
        private int _currentDay;
        private int _currentTurn = 0;
        private const int MaxTurnPerDay = 10;
        private const int MaxDay = 10;
        private bool _continuePlaying = true; // moved to field so rooms can change it via requests

        //Sustainability variable
        private int sustainability;

        // Food and farming variables
        private int food;
        private int grainseeds;
        private int grains;
        private int hunger; // This can be assigned as 100 in start as 100%, but everyday it reduces by 25-40%, so player have to feed villagers everyday.

        // animals and forest variables 
        private int animals;
        private int trees;
        private int wood;
        private int saplings;


        // Global sustainability points defined in Game (static so accessible from Room)
        public static int SustainabilityPoints { get; set; } = 10;

        public Game()
        {
            _rooms = new List<Room?>();
            CreateRooms();
            _villagers = new List<Villager>();
            CreateVillagers();

            // Deafult values for trackable variables.
            // We have to discuss with what values does the player start
            // Now they are just defined.

            // Sustainability point tracker

            sustainability = 50; // Later we cauculate default starting points, so it's possible for player to play.

            // Food and farming (Starting values should be discussed).

            food = 2;
            grainseeds = 5;
            grains = 0;
            hunger = 50;


            // animals and forest related (Starting values should be discussed).
            animals = 5;
            trees = 20;
            saplings = 0;
            wood = 0;

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
                //Console.WriteLine(_currentRoom?.ShortDescription);
                //RoomInfo(_currentRoom!.ShortDescription);
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
                        hunger -= 35;
                        Console.WriteLine($"Day advanced to {_currentDay}.");
                        break;
                    case "quit":
                        _continuePlaying = false;
                        break;
                    case "assign":
                        AssignVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                        break;
                    case "feed":
                        food--;
                        hunger += 50;  //Player can feed villagers 2 times a day to gain up to 100%.
                        break;
                    case "hunt":
                        food++;
                        animals--;
                        sustainability -= 5;
                        break;
                    case "farm":
                        grainseeds--;
                        break;
                    case "harvest":
                        grains++;
                        sustainability -= 5;
                        break;
                    case "chop":
                        wood++;
                        saplings += 2;
                        trees--;
                        sustainability -= 5;
                        break;
                    case "plant":
                        saplings--;
                        sustainability += 10;
                        break;
                    case "cook":
                        grains -= 2;
                        food++;
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

            int id = -1;

            foreach (Room rName in _rooms!)
            {
                if (nameString == rName!.ShortDescription)
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
