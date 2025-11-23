namespace WorldOfZuul.RoomType
{
    public class School : Room
    {
        public School(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {

        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have entered the School. Learn about sustainability here.");
            Console.WriteLine();

            // Display current sustainability points to give context
            Console.WriteLine($"Sustainability Points: {Game.SustainabilityPoints}");
            Console.WriteLine();

            Console.WriteLine("Available actions:");

            Console.WriteLine("about : About the project");
            Console.WriteLine("learn : Learn about sustainability");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }

        public override void CommandList(Command command)
        {
            switch (command.Name)
            {
                case "about":
                    AboutProject();
                    break;
                case "learn":
                case "read":
                    LearnSustainability();
                    break;
                default:
                    Console.WriteLine("Invalid command in the school. Try 'about' or 'learn'.");
                    break;
            }
        }

        private void AboutProject()
        {
            Console.WriteLine("About this project:");
            Console.WriteLine("This is a learning game demonstrating simple resource and villager management.");
            Console.WriteLine("Make choices that affect sustainability points and the world state.");
            Console.WriteLine();
            Console.WriteLine("Purpose:");
            Console.WriteLine("- The game is educational: it illustrates consequences of consumption and resource use.");
            Console.WriteLine("- Players explore trade-offs between short-term gains and long-term sustainability.");
            Console.WriteLine("- It connects simple game mechanics to the UN Sustainable Development Goals to raise awareness.");
            Console.WriteLine();           
            Console.WriteLine("Sustainability basics:");
            Console.WriteLine("- Sustainable actions increase sustainability points and help preserve resources.");
            Console.WriteLine("- Unsustainable actions decrease sustainability points and can lead to resource depletion.");
            Console.WriteLine($"Current Sustainability Points: {Game.SustainabilityPoints}");
            Console.WriteLine();
        }

        private void LearnSustainability()
        {
            Console.WriteLine("According to the SDG report of 2025, one of the main sustainability problems is overconsumption");
            Console.WriteLine("which will be the main focus of the game.The challenge we are faced with is to ensure sustainable" );
            Console.WriteLine("consumption and production patterns, either in the food industry, in the retail industry or just when");
            Console.WriteLine("using natural resources.");

        }
    }
}
