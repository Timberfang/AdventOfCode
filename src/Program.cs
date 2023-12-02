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

    public class Day1
    {
        public static int Puzzle(string[] Input)
        {
            int CalibrationOutput = 0; // Initialize for later use
            foreach (string CalibrationValue in Input)
            {
                // string CleanedValue = CalibrationValue;
                string CleanedValue = WordToNumber(CalibrationValue); // Convert "one" to "1", "two" to "2", and so on
                CleanedValue = Regex.Replace(CleanedValue, "[^0-9]+", string.Empty); // Strip everything left but numbers

                // Get the first and last numbers and combine them
                string FirstValue = CleanedValue.First().ToString();
                string LastValue = CleanedValue.Last().ToString();
                int Calibration = int.Parse(FirstValue + LastValue);

                // Give output and add to the total
                Console.WriteLine($"Input {CalibrationValue} gives output {Calibration}!");
                File.AppendAllText(@".\log.txt", $"Input {CalibrationValue} cleans into {CleanedValue} which gives output {Calibration}!" + Environment.NewLine);
                CalibrationOutput += Calibration;
            }
            return CalibrationOutput;
        }


        private static string WordToNumber(string Words)
        {
            Dictionary<string, string> WordNumbers = new Dictionary<string, string>
            {
                { "eightwo", "82" },  // Merged number edge case
                { "twone", "21" },    // Merged number edge case
                { "oneight", "18" },   // Merged number edge case
                { "zero", "0" },
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" },
                { "ten", "10" },
                { "eleven", "11" },
                { "twelve", "12" },
                { "thirteen", "13" },
                { "fourteen", "14" },
                { "fifteen", "15" },
                { "sixteen", "16" }
            };

            // Create a regular expression pattern from the dictionary keys
            string WordSearchPattern = string.Join("|", WordNumbers.Keys.Select(Regex.Escape));
            Regex WordSearch = new Regex(WordSearchPattern);

            // Convert word numbers to actual numbers
            string Output = WordSearch.Replace(Words, match => WordNumbers[match.Value]);
            return Output;
        }
    }
}