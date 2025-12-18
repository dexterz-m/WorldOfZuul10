using WorldOfZuul.ConsoleUi.Templates;
using WorldOfZuul.Data;
using WorldOfZuul.Domain;
using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.ConsoleUi
{
    public class Game
    {
        private const int MaxTurnPerDay = 10;
        private const int MaxDay = 10;

        public static int SustainabilityPoints { get; set; } = 10;

        public string CommandText;

        public int t;

        public Game()
        {
        }

        public void Play(DataInitializer initializer)
        {
            DataHandler dh = new DataHandler(initializer);
            Parser parser = new();
            MainUiTemplate mainUiTemplate = new MainUiTemplate();

           

            while (dh.ContinuePlaying)
            {
                SustainabilityPoints = dh.Resources.SustainabilityPoints;

                
                mainUiTemplate.RenderMain(
                   dh.CurrentDay,
                   MaxDay,
                   dh.TurnsThisDay,
                   MaxTurnPerDay,
                   dh,
                   CommandText
               );


                Console.Write("> ");
                

                var input = Console.ReadLine();

                switch (input)
                {
                    case null:
                    case string s when string.IsNullOrWhiteSpace(s):
                        CommandText = "Please enter a command.";
                        dh.t++;
                        continue;

                    default:
                        var command = parser.GetCommand(input);

                        switch (command)
                        {
                            case null:
                                CommandText = "I don't know that command.";
                                dh.t++;
                                continue;

                            default:
                                CommandText = dh.HandleCommand(command);
                                break;
                        }
                        break;
                }





                SustainabilityPoints = dh.Resources.SustainabilityPoints;

                if (dh.Resources.SustainabilityPoints < 0)
                {
                    Console.WriteLine("You lost");
                    break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }
    }
}
