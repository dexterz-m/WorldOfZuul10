using WorldOfZuul.Jobs;

namespace WorldOfZuul.RoomType
{
    public class Village : Room
    {
        private int Villagers { get; set; }
        private int Houses { get; set; } = 5;
        public Village(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Unemployed())
        {

        }


        public override void EnterRoom()
        {

            Console.Clear();
            // Display current state of the village
            Console.WriteLine("Below are the current stats:");
            Console.WriteLine($"Villagers: {Villagers}");
            Console.WriteLine($"Houses: {Houses}");
            Console.WriteLine();

            // List available actions for the player
            Console.WriteLine("Here are available commands");
            Console.WriteLine("ls v                                   List out all villagers and their status");
            Console.WriteLine("ls r                                   List out all rooms");
            Console.WriteLine("ls j                                   List out all jobs");
            Console.WriteLine("cd [ROOM NAME]                         Goes to room");
            Console.WriteLine();
            Console.WriteLine("feed -[VILLAGER ID] [AMOUNT/DAY]       Feeds villager and activates it");
            Console.WriteLine("assign -[VILLAGER ID] [JOB NAME]       Assigns villager to a task");
            Console.WriteLine("sleep                                  Skip the remaining moves");

            // ready for command input, Game will handle reading commands
        }

        public override void CommandList(Command command)
        {
            switch (command.Name)
            {               
                case "feed":
                    FeedVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                    break;
                case "assign":
                    AssignVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                    break;
                case "cook":
                    Cook(Convert.ToInt32(command.SecondWord ?? "1"));
                    break;
                default:
                    Console.WriteLine("I don't know what command.");
                    break;
            }
        }

        
        private static void FeedVillager(int villagerId, int foodAmount)
        {
            var villager = Game.Villagers?.FirstOrDefault(villager => villager.Id == villagerId);
            if (villager == null)
            {
                Console.WriteLine($"No villager with ID {villagerId} found.");
                return;
            }
            villager.Feed(foodAmount);
            Game.NextTurn();
        }
        
        private static void AssignVillager(int villagerId, int jobId)
        {
            var villager = Game.Villagers?.FirstOrDefault(villager => villager.Id == villagerId);
            if (villager == null)
            {
                Console.WriteLine($"No villager with ID {villagerId} found.");
                return;
            }
            if (!villager.CanWork)
            {
                Console.WriteLine($"Villager with ID {villagerId} is not able to work. Try feeding them first.");
                return;
            }

            Job? targetJob = null;
            foreach (var room in Game.Rooms)
            {
                if (room?.Jobs == null) continue;
                foreach (var job in room.Jobs.OfType<Job>().Where(job => job.Id == jobId))
                {
                    targetJob = job;
                }
                if (targetJob != null) break;
            }

            if (targetJob == null)
            {
                Console.WriteLine($"No job with ID {jobId} found.");
                return;
            }

            foreach (var room in Game.Rooms)
            {
                if (room?.Jobs == null) continue;
                foreach (var job in room.Jobs)
                {
                    job?.Villagers?.Remove(villager);
                }
            }

            if (targetJob.Villagers != null && targetJob.Villagers.Contains(villager))
            {
                Console.WriteLine($"Villager with ID {villagerId} already assigned to {targetJob.Name}.");
                return;
            }
            
            targetJob.AddVillager(villager);
            Game.NextTurn();
        }
        
        private static void Cook(int amount)
        {
            if (Game.Resources.Grains < amount)
            {
                Console.WriteLine($"Not enough grains to cook {amount} food. You have {Game.Resources.Grains} grains.");
                return;
            }
            Game.Resources.Grains = -amount;
            Game.Resources.Food = amount;
            Game.NextTurn();
        }
    }
}
