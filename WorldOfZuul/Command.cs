namespace WorldOfZuul
{
    public class Command
    {
        public string Name { get; }
        public string? SecondWord { get; } // this might be used for future expansions, such as "take apple".
        public string?  ThirdWord { get; }

        public Command(string name, string? secondWord = null,  string? thirdWord = null)
        {
            Name = name;
            SecondWord = secondWord;
            ThirdWord = thirdWord;
        }
    }
}