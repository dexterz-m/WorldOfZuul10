namespace WorldOfZuul
{
    public class CommandWords
    {
        private List<string> ValidCommands { get; } = new List<string>
        {
            "ls", 
            "feed", //turn
            "talk", //turn
            "sleep", 
            "assign", //turn
            "build", //turn
            "help", 
            "quit", 
            "cd", //turn
            "cut", //turn
            "plant", //turn
            "kill", //turn
            "build-farmland", //turn
            "about", 
            "learn",
            "hunt", //turn
            "farm", //turn
            "cut-forest", //turn
            "harvest", //turn
            "cook", //turn
        };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
