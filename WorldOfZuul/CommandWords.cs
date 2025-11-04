namespace WorldOfZuul
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "ls", "feed", "talk", "sleep", "assign", "build", "help", "quit", "cd", "cut", "plant", "kill", "build-farmland" };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
