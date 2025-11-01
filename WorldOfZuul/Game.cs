using WorldOfZuul.RoomType;

namespace WorldOfZuul
{
    public class Game
    {
        private readonly List<Room?> _rooms;
        private Room? _currentRoom;
        private int _currentDay;
        private const int MaxDay = 10;
        private bool _continuePlaying = true; // moved to field so rooms can change it via requests

        // Global sustainability points defined in Game (static so accessible from Room)
        public static int SustainabilityPoints { get; set; } = 10;

        public Game()
        {
            _rooms = new List<Room?>();
            CreateRooms();
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
                        Console.WriteLine($"Day advanced to {_currentDay}.");
                        break;
                    case "quit":
                        _continuePlaying = false;
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

        private static void PrintHelp()
        {
            Console.WriteLine("Here are available commands");
            Console.WriteLine("help                                   Bring up this information");
            Console.WriteLine("ls v                                   List out all villagers and their status");
            Console.WriteLine("ls r                                   List out all rooms");
            Console.WriteLine("ls j                                   List out all jobs");
            Console.WriteLine("cd [ROOM NAME]                         Goes to room");
            Console.WriteLine("feed -[VILLAGER ID] [AMOUNT/DAY]       Feeds villager and activates it");
            Console.WriteLine("assign -[VILLAGER ID] [JOB NAME]       Assigns villager to a task");
            Console.WriteLine("sleep                                  Skip the remaining moves");
            Console.WriteLine("exit                                   Exit game");
            Console.ReadKey();
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
                    Console.WriteLine("Villagers");
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
    }
}
