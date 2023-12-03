// See https://aka.ms/new-console-template for more information
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.src.solutions;

namespace AdventOfCode.src
{
    class Program
    {
         static void Main()
         {
            // int Output = Day1.Puzzle(File.ReadAllLines(".\\input-day1.txt"));
            // var Output = Day2.Puzzle(File.ReadAllLines(".\\input-day2.txt"),12,13,14,true);
            int Output = Day3.Puzzle(File.ReadAllLines(".\\input-day3.txt"));
            Console.WriteLine($"Output: {Output}");
        }
    }
}