using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Parser
    {
        private readonly CommandWords _commandWords = new();

        public Command? GetCommand(string inputLine)
        {
            string[] words = inputLine.Split();

            if (words.Length == 0 || !_commandWords.IsValidCommand(words[0]))
            {
                return null;
            }

            return words.Length switch
            {
                1 => new Command(words[0]),
                2 => new Command(words[0], words[1]),
                3 => new Command(words[0], words[1], words[2]),
                _ => null
            };
        }
    }

}
