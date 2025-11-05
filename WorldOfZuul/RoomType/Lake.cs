namespace WorldOfZuul.RoomType
{
    public class Lake : Room
    {
        int Fishes {  get; set; } = 10;

        public Lake(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {

        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have arrived at the lake. Below are the current stats:");
            Console.WriteLine($"Fishes: {Fishes}");
            Console.WriteLine();

            Console.WriteLine("Available actions:");
            Console.WriteLine(" - fish   : Catch fish");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }

        void GoFishing()
        {

        }
    }
}
