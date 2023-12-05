using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day4
    {
        public static int Puzzle(string[] input, bool part2)
        {
            int Output = 0;

            foreach (string line in input)
            {
                string[] LineArray = line.Split('|');
                Console.WriteLine(LineArray[0]);
                Console.WriteLine(LineArray[1]);
            }

            return Output;
        }
    }
}
