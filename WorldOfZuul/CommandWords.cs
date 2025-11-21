namespace WorldOfZuul
{
    public class CommandWords
    {
        private List<string> ValidCommands { get; } = new List<string>
        {
            "ls",
            "feed",
            "talk", 
            "sleep", 
            "assign", 
            "build", 
            "help", 
            "quit", 
            "cd", 
            "cut", 
            "plant", 
            "kill", 
            "build", 
            "about", 
            "learn",
            "hunt",
            "farm",
            "cut-forest",
            "harvest",
            "cook",
            "cut",
            "catch"
        };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
