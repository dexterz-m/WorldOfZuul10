namespace WorldOfZuul
{
    public class Game
    {
        private readonly List<Room?> _rooms;
        private Room? _currentRoom;
        private int _currentDay;
        private const int MaxDay = 10;

        public Game()
        {
            _rooms = new List<Room?>();
            CreateRooms();
        }

        private void CreateRooms()
        {
            //Room? farmland1 = new("Farmland1", "(Placeholder farmlands1)");
            //Room? farmland2 = new("Farmlands2", "(Placeholder farmlands2)");
            //Room? farmland3 = new("Farmlands3", "(Placeholder farmlands3)");
            Room farmlandMain = new("FarmlandMain", "(Placeholder farmlandMain)");
            
            Room forest = new("Forest", "(Placeholder forest)");
            Room village = new("Village", "(Placeholder village)");
            Room lake = new("Lake", "(Placeholder lake)");
            Room school = new("School", "(Placeholder school)");

            _rooms.Add(village);
            _rooms.Add(forest);
            _rooms.Add(farmlandMain);
            _rooms.Add(lake);
            _rooms.Add(school);
            
            /*
            village.SetExits(farmlandMain, school, lake, forest); // North, East, South, West
            farmlandMain.SetExits(farmland2, farmland3, village, farmland1); 
            farmland1.SetExit("east", farmlandMain);
            farmland2.SetExit("south", farmlandMain);
            farmland3.SetExit("west", farmlandMain);
            forest.SetExits(null, village, lake, null); 
            lake.SetExits(village, null, null, forest);
            school.SetExit("west", village);
            */
            _currentRoom = _rooms[0];
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying && _currentDay <= MaxDay)
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

                switch(command.Name)
                {
                    case "ls":
                        List(command.SecondWord == null? null :  Convert.ToChar(command.SecondWord));
                        break;
                    case "cd":
                        ChangeRoom(command.SecondWord);
                        break;
                    case "feed":
                        Feed();
                        break;
                    case "assign":
                        Assign(command.SecondWord, command.ThirdWord);
                        break;
                    case "sleep":
                        _currentDay++;
                        break;
                    case "quit":
                        continuePlaying = false;
                        break;
                    case "help":
                        PrintHelp();
                        break;
                    case "hunt":
                        food++;
                        break;
                    case "farm";
                        break;
                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }

        private void Feed()
        {
            //TODO: Add daily food consumption to a villager
            //Implement after Villagers and Resource 
            throw new NotImplementedException();
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
            
            if(id != -1 && id < _rooms.Count)
            {
                _currentRoom = _rooms[id];
                
            }
            else
            {
                Console.WriteLine("No room with this name! Try again or see 'help' for syntax.");
            }
            
            /*
            int id;
            try {
                id = Convert.ToInt32(idString);
            }
            catch (FormatException) {
                Console.WriteLine("Room ID must be a number! Try again or see 'help' for syntax.");
                return;
            }

            if (id >= _rooms.Count || id < 0) {
                Console.WriteLine("No room with id {}! Try 'ls r' to see all the rooms.");
            }
            _currentRoom = _rooms[id];
            */
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
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
        
        private static void Assign(string? villagerId, string? jobId)
        {
            int vId;
            int jId;
            try
            {
                vId = Convert.ToInt32(villagerId);
                jId = Convert.ToInt32(jobId);
            }
            catch (FormatException)
            {
                Console.WriteLine("VillagerID and jobID must be a number! Try again or see 'help' for syntax.");
                return;
            }

            //TODO: Add upper limit based on villagers list
            if (vId < 0 )
            {
                Console.WriteLine("No Villager with id {0}! Try 'ls v' to see villagers", vId);
            }
            //TODO: Add upper limit based on jobs
            if (jId < 0 )
            {
                Console.WriteLine("No Villager with id {0}! Try 'ls v' to see villagers", jId);
            }


            //TODO: add villager to a room based on job
            /*
             * _rooms.AssignVillager(villagerId, jId);
             * _rooms.Remove(villagerId);
             */
            
            //TODO: add resource gain based on villager experience
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
