using WorldOfZuul.Jobs;

namespace WorldOfZuul.RoomType
{
    public class Village : Room
    {
        int villigers { get; set; } = 20;
        int houses { get; set; } = 5;
        public Village(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Unemployed())
        {

        }


        public override void EnterRoom()
        {

            // Display current state of the village
            Console.WriteLine("Below are the current stats:");
            Console.WriteLine($"Villagers: {villigers}");
            Console.WriteLine($"Houses: {houses}");
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
                    feed(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord));
                    break;
                case "assign":
                    Assign(command.SecondWord, command.ThirdWord);
                    break;
                default:
                    Console.WriteLine("I don't know what command.");
                    break;
            }
        }

        void feed(int villagerID, int amount)
        {

        }

        private static void Assign(string? villagerId, string? jobId)
        {
            int vId;
            int jId;
            try
            {
                vId = Convert.ToInt32(villagerId);
                jId = Convert.ToInt32(jobId);
            }
            catch (FormatException)
            {
                Console.WriteLine("VillagerID and jobID must be a number! Try again or see 'help' for syntax.");
                return;
            }

            //TODO: Add upper limit based on villagers list
            if (vId < 0)
            {
                Console.WriteLine("No Villager with id {0}! Try 'ls v' to see villagers", vId);
            }
            //TODO: Add upper limit based on jobs
            if (jId < 0)
            {
                Console.WriteLine("No Villager with id {0}! Try 'ls v' to see villagers", jId);
            }


            //TODO: add villager to a room based on job
            /*
             * _rooms.AssignVillager(villagerId, jId);
             * _rooms.Remove(villagerId);
             */

            //TODO: add resource gain based on villager experience
        }

    }
}
