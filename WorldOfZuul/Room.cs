namespace WorldOfZuul
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set;}
        public Dictionary<string, Room> Exits { get; private set; } = new();
        
        List<int> VillagerIds = new List<int>();
        List<int> JobIds = new List<int>();
        
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
        
        public void AssignVillager(int  villagerId, int jobId)
        {
            VillagerIds.Add(villagerId);
            JobIds.Add(jobId);
        }
        
        public void RemoveVillager(int  villagerId)
        {
            JobIds.RemoveAt(VillagerIds.IndexOf(villagerId));
            VillagerIds.Remove(villagerId);
            
        }

        public void PrintVl()
        {
            foreach (int vId in VillagerIds)
            {
                Console.WriteLine(vId);
            }
            
        }
        
        public void PrintJb()
        {
            foreach (int jId in JobIds)
            {
                Console.WriteLine(jId);
            }
            
        }
        

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
    }
}
