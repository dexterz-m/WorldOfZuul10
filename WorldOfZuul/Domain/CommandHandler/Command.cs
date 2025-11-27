namespace WorldOfZuul.Domain.CommandHandler
{
    public class Command
    {
        public string Name { get; }
        public string? SecondWord { get; } 
        public string?  ThirdWord { get; }

        public Command(string name, string? secondWord = null,  string? thirdWord = null)
        {
            Name = name;
            SecondWord = secondWord;
            ThirdWord = thirdWord;
        }
    }
}
