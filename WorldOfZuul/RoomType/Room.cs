using WorldOfZuul.Jobs;

namespace WorldOfZuul.RoomType
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public Job? Job { get; private set; }

        // SustainabilityPoints proxy to Game.SustainabilityPoints
        public int SustainabilityPoints
        {
            get { return Game.SustainabilityPoints; }
            set { Game.SustainabilityPoints = value; }
        }

        public Room(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }
        public Room(string shortDesc, string longDesc, Job job)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            this.Job = job;
        }
        

        public virtual void EnterRoom()
        {
            Console.WriteLine($"Entering {ShortDescription}");
        }
        public virtual void CommandList(Command command)
        {
            Console.WriteLine("No specific commands available in this room.");
        }
    }
}
