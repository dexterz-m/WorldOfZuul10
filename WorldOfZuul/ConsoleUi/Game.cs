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
            mainUiTemplate.RenderMain(_currentDay, MaxDay, CurrentTurn, MaxTurnPerDay, dh);
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
                    foreach (string s in dh.GetUiData(command))
                    {
                        Console.WriteLine(s);
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
