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
                    dh
                );

                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
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

                Console.WriteLine(dh.HandleCommand(command));

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
