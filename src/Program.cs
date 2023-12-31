﻿// See https://aka.ms/new-console-template for more information
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
            long Output = Day6.Puzzle(File.ReadAllLines(".\\input-day6.txt"), true);
            File.WriteAllText(".\\log.txt", $"Output: {Output}");
        }
    }
}