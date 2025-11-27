namespace WorldOfZuul.RoomType
{
    public class Farmland : Room
    {
        private int FarmlandAmount {  get; set; }
        private int PossibleFarmland { get; set; } = 1;
        // stores the turn when the farmland was planted
        private List<int> FarmlandPlanted { get; set; } = new List<int>();
        private int FarmlandRipped { get; set; } = 0;
        Random Rnd = new  Random();

        public Farmland(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
            FarmlandAmount = 1;
        }

        public override void EnterRoom()
        {
            Console.Clear();
            Console.WriteLine("You have entered the Farmland. Below are the current stats:");
            Console.WriteLine($"Farmlands: {FarmlandAmount}");
            Console.WriteLine($"Free farmlands: {PossibleFarmland - FarmlandAmount}");
            Console.WriteLine();

            Console.WriteLine("Available actions:");
            Console.WriteLine(" - build farmland           : Build a new farmland");
            Console.WriteLine(" - cut forest               : Cut 5 trees to make freeland (reduces sustainability)");
            Console.WriteLine(" - plant farmland           : Plant on your farmland");
            Console.WriteLine(" - farm                     : Farm your planted farmland");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }
        
        public override void CommandList(Command command)
        {
            switch (command.Name)
            {
                case "build":
                        BuildFarmland();
                    break;
                case "cut":
                        CutForest();
                    break;
                case "harvest":
                    Harvest();
                    break;
                case "plant":
                        PlantFarmland();
                    break;
                default:
                    Console.WriteLine("Invalid command in the farmland.");
                    break;
            }
        }
        
        private void BuildFarmland()
        {
            switch (Game.Resources.Wood)
            {
                case >= 5 when FarmlandAmount < PossibleFarmland:
                    FarmlandAmount += 1;
                    Game.Resources.Wood -= 5;
                    Console.WriteLine($"You have built a new farmland. Now you have: {FarmlandAmount} farmlands.");
                    Game.NextTurn();
                    break;
                case < 5 when FarmlandAmount == PossibleFarmland:
                    Console.WriteLine("You dont have enough wood and freeland to build farmland!!");
                    break;
                case < 5:
                    Console.WriteLine("You dont have enough wood to build farmland!!");
                    break;
                default:
                    Console.WriteLine("You dont have enough freeland to build farmland!!");
                    break;
            }
        }
        
        private void CutForest()
        {
            if (PossibleFarmland > FarmlandAmount)
            {
                Console.WriteLine("There is freeland no need to cut more trees for now.");
                return;
            }
            if (Game.Resources.Trees <= 0)
            {
                Console.WriteLine("No trees left to cut.");
                return;
            }
            
            Game.Resources.Trees -= 5;
            Game.Resources.Wood += 10;
            Game.SustainabilityPoints -= 10;
            PossibleFarmland++;

            Console.WriteLine("You now have space for 1 more farmland.");
            
            Console.WriteLine($"Sustainability Points: {Game.SustainabilityPoints}");
            Game.NextTurn();
        }
        
        private void PlantFarmland()
        {
            if(FarmlandPlanted.Count < FarmlandAmount && Game.Resources.GrainSeeds >= 4)
            {
                FarmlandPlanted.Add(Game.TotalTurns);
                Game.Resources.GrainSeeds -= 4;
                Game.SustainabilityPoints += 8;

                Game.NextTurn();
                Console.WriteLine("You have planted 1 more farmland.");
                Console.WriteLine($"Now you have {FarmlandPlanted} planted farmlands.");
            }
            else
            {
                if (FarmlandPlanted.Count == FarmlandAmount && Game.Resources.GrainSeeds < 4)
                {
                    Console.WriteLine("All your farmlands are planted and you dont have enough Grain seeds to plant a farmland");
                }
                else if(FarmlandPlanted.Count == FarmlandAmount)
                {
                    Console.WriteLine("All your farmlands are planted.");
                }
                else
                {
                    Console.WriteLine("You dont have enough Grain seeds to plant a farmland");
                }
                
            }
        }
        
        private void Harvest()
        {

            if (FarmlandRipped > 0)
            {
                FarmlandRipped -= 1;
                Game.Resources.GrainSeeds += Rnd.Next(3, 7);
                Game.Resources.Food += 4;
                Game.SustainabilityPoints -= 4;
                Game.NextTurn();
                Console.WriteLine($"Now you have {FarmlandPlanted.Count} planted farmlands.");
            }
            else
            {
                Console.WriteLine("None of your farmlands are ripe.");
            }
        }
        
        public void RipenFarmland()
        {
            foreach (var farmland in FarmlandPlanted.Where(farmland => Game.TotalTurns - farmland >= 3).ToList())
            {
                FarmlandRipped += 1;
                FarmlandPlanted.Remove(farmland);
            }
        }
    }
}