using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul.RoomType
{
    public class School : Room
    {
        public School(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {

        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have entered the School. Learn about sustainability here.");
            Console.WriteLine();
            Console.WriteLine("Available actions:");
            Console.WriteLine(" - read : Read about sustainability");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }
    }
}
