namespace WorldOfZuul.RoomType
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();

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

        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }


        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
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
