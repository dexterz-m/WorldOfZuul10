using WorldOfZuul.Jobs;

namespace WorldOfZuul.RoomType
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public List<Job?> Jobs { get; private set; } = new List<Job?>();

        // SustainabilityPoints proxy to Game.SustainabilityPoints
        
        
        protected Room(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }

        protected Room(string shortDesc, string longDesc, Job job)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            this.Jobs.Add(job);
        }
        public Room(string shortDesc, string longDesc, List<Job?> jobs)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            this.Jobs = jobs;
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
