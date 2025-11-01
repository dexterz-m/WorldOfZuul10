using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul.RoomType
{
    public class Farmland : Room
    {
        int FarmlandSize {  get; set; }


        public Farmland(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
            FarmlandSize = 1;
        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have entered the Farmland. Below are the current stats:");
            Console.WriteLine($"Farmlands: {FarmlandSize}");
            Console.WriteLine();

            Console.WriteLine("Available actions:");
            Console.WriteLine(" - build farmland           : Build a new farmland");
            Console.WriteLine(" - cut forest for farmland  : Convert forest to farmland (reduces sustainability)");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }

        public void BuildFarmland()
        {
            //logic//
        }

        public void CutForest()
        {
            //logic//
        }
    }
}
