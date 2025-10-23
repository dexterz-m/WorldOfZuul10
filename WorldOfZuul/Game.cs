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
            Room village = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            Room forest = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            Room farmlands = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");

            _rooms.Add(village);
            _rooms.Add(forest);
            _rooms.Add(farmlands);
            
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

        private void ChangeRoom(string? idString)
        {
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

        private static void List(char? type)
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
                    Console.WriteLine("Rooms");
                    break;
                default:
                    Console.WriteLine("Wrong command! Try 'help' to see syntax.");
                    break;
            }
        }
    }
}
