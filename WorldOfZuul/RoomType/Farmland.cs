namespace WorldOfZuul.RoomType
{
    public class Farmland : Room
    {
        int FarmlandAmount {  get; set; }


        public Farmland(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
            FarmlandAmount = 1;
        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have entered the Farmland. Below are the current stats:");
            Console.WriteLine($"Farmlands: {FarmlandAmount}");
            Console.WriteLine();

            Console.WriteLine("Available actions:");
            Console.WriteLine(" - build-farmland           : Build a new farmland");
            Console.WriteLine(" - cut forest for farmland  : Convert forest to farmland (reduces sustainability)");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }
        
        public override void CommandList(Command command)
        {
            switch (command.Name)
            {
                case "build-farmland":
                    BuildFarmland();
                    break;
                default:
                    Console.WriteLine("Invalid command in the farmland.");
                    break;
            }
        }

        public void BuildFarmland()
        {
            if (true)
            {
                FarmlandAmount += 1;
                Console.WriteLine($"You have built a new farmland. Now you have: {FarmlandAmount} farmlands.");
            }
            else
            {
                Console.WriteLine("You dont have enough wood to build farmland!!");
            }
        }

        public void CutForest()
        {
            //logic//
        }
        
        
    }
}
