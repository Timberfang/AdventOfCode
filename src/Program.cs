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
            // int Output = Day3.Puzzle(File.ReadAllLines(".\\input-day3.txt"), true); // Note: dataset padded by one column with '.' characters
            // int Output = Day4.Puzzle(File.ReadAllLines(".\\input-day4.txt"), true);

            // Set the paths for input and data files
            const string inputFilePath = ".\\input-day5.txt";
            const string dataFilePath = ".\\data-day5.txt";
            int DataGroup = 1; // Data group counter

            // Loop through the lines and replace the first line in the input file
            foreach (string dataLine in File.ReadAllLines(dataFilePath))
            {
                ReplaceFirstLine(inputFilePath, dataLine);
                File.AppendAllText(".\\log.txt", $"Replaced first line with: {dataLine}" + Environment.NewLine);
                File.AppendAllText(".\\log.txt", $"Running..." + Environment.NewLine);
                long Output = Day5.Puzzle(File.ReadAllLines(".\\input-day5.txt"), true);
                File.AppendAllText(".\\log.txt", $"Data Group {DataGroup} Output: {Output}" + Environment.NewLine);
                DataGroup++;
            }
        }

        static void ReplaceFirstLine(string filePath, string newLine)
        {
            // Read all lines from the file and replace first line with a line from the partitioned data set
            string[] lines = File.ReadAllLines(filePath);
            lines[0] = newLine;
            File.WriteAllLines(filePath, lines);
        }
    }
}