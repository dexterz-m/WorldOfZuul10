namespace WorldOfZuul.Domain.CommandHandler
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; }

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
