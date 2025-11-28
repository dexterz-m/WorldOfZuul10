using System.Reflection.Metadata;
using WorldOfZuul.ConsoleUi.Templates;
using WorldOfZuul.Data;
using WorldOfZuul.Domain;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Rooms;


namespace WorldOfZuul.ConsoleUi
{
    public class Game
    {
        // Only handle turns, days 
        private int CurrentTurn {get; set;}
        private int _currentDay;
        private const int MaxTurnPerDay = 10;
        private const int MaxDay = 10;
        private bool _continuePlaying = true;
        public static int SustainabilityPoints { get; set; } = 10;
        
        
        //How it should look:
        /*
         * {MainUiTemplate}
         *
         *
         * 
         * >
         */

        public Game()
        {
            
        }
        public void Play(IDataInitializer initializer)
        {
            ListUiTemplate listUi = new ListUiTemplate();
            DataHandler dh = new DataHandler(initializer);
            Parser parser = new();

            // -- PrintWelcome();
            
            
            MainUiTemplate mainUiTemplate = new MainUiTemplate();
            mainUiTemplate.RenderMain(_currentDay, MaxDay, CurrentTurn, MaxTurnPerDay, dh.CurrentRoom);
            while (_continuePlaying && _currentDay <= MaxDay)
            {
                CurrentTurn = 0;
                while (_continuePlaying && CurrentTurn <= MaxTurnPerDay)
                {
                    
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
                            listUi.List(command.SecondWord == null ? null : Convert.ToChar(command.SecondWord));
                            break;
                        case "cd":
                            //ChangeRoom(command.SecondWord);
                            break;
                        case "sleep":
                            _currentDay++;
                            dh.Resources.Hunger = -35;
                            Console.WriteLine($"Day advanced to {_currentDay}.");
                            break;
                        case "quit":
                            _continuePlaying = false;
                            break;
                        case "assign":
                            //AssignVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                            CurrentTurn++;
                            break;
                        case "feed":
                            if (command.SecondWord == "villigers")
                            {
                                dh.Resources.Food = - 1;
                                dh.Resources.Hunger = 50;
                                CurrentTurn++;
                            }
                            else
                            {
                                //_currentRoom?.CommandList(command);
                            }
                            break;
                        case "hunt":
                            dh.Resources.Food = 1;
                            dh.Resources.Animals = -1;
                            SustainabilityPoints -= 5;
                            break;
                        case "harvest":
                            SustainabilityPoints -= 5;
                            break;
                        case "chop":
                            dh.Resources.Wood = 1;
                            dh.Resources.Saplings = 2;
                            dh.Resources.Trees = -1;
                            SustainabilityPoints -= 5;
                            break;
                        case "plant":
                            if (command.SecondWord == "trees")
                            {
                                dh.Resources.Saplings -= 1;
                                SustainabilityPoints += 10;
                            }
                            else
                            {
                                //_currentRoom?.CommandList(command);
                            }
                            break;
                        case "cook":
                            dh.Resources.Food = 1;
                            break;
                        case "talk":
                            //_advisor.Talk();
                            break;
                        default:
                            // Not a global command: pass it to the current room to handle
                            //_currentRoom?.CommandList(command);
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
        
        

    }
}
