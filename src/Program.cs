// See https://aka.ms/new-console-template for more information
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent.Solutions
{
    class Program
    {
        static void Main()
        {
            var Output = Day1.Puzzle(File.ReadAllLines(".\\input.txt"));
            Console.WriteLine($"Output: {Output}");
        }
    }
}