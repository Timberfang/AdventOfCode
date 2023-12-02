// See https://aka.ms/new-console-template for more information
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions;

namespace AdventOfCode.Solutions
{
    class Program
    {
         static void Main()
         {
             // var Output = Day1.Puzzle(File.ReadAllLines(".\\input.txt"));
             var Output = Day2.Puzzle(File.ReadAllLines(".\\input.txt"),12,13,14);
             Console.WriteLine($"Output: {Output}");
         }
    }
}