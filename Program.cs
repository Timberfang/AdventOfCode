// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

namespace Advent.Solutions
{
    class Program
    {
        static void Main()
        {
            var Output = Day1.Solution(File.ReadAllLines(".\\input.txt"));
            Console.WriteLine($"Output: {Output}");
        }
    }

    public class Day1
    {
        public static int Solution(string[] Input)
        {
            int CalibrationOutput = 0;
            foreach (string CalibrationValue in Input)
            {
                string CleanedValue = CalibrationValue;

                // Edge cases - This is dumb, but it works
                CleanedValue = CleanedValue.Replace("eightwo", "82");
                CleanedValue = CleanedValue.Replace("sixteen", "16");
                CleanedValue = CleanedValue.Replace("twone", "21");
                CleanedValue = CleanedValue.Replace("oneight", "18");

                // Convert word numbers to integers - I'm sure there's a better way to do this that I don't know about, but I'm new to C#
                CleanedValue = CleanedValue.Replace("zero", "0");
                CleanedValue = CleanedValue.Replace("one", "1");
                CleanedValue = CleanedValue.Replace("two", "2");
                CleanedValue = CleanedValue.Replace("three", "3");
                CleanedValue = CleanedValue.Replace("four", "4");
                CleanedValue = CleanedValue.Replace("five", "5");
                CleanedValue = CleanedValue.Replace("six", "6");
                CleanedValue = CleanedValue.Replace("seven", "7");
                CleanedValue = CleanedValue.Replace("eight", "8");
                CleanedValue = CleanedValue.Replace("nine", "9");
                CleanedValue = Regex.Replace(CleanedValue, "[^0-9]+", string.Empty); // Strip everything left but numbers

                string FirstValue = CleanedValue.First().ToString();
                string LastValue = CleanedValue.Last().ToString();

                int Calibration = int.Parse(FirstValue + LastValue);

                Console.WriteLine($"Input {CleanedValue}... Gives output {Calibration}!");
                CalibrationOutput += Calibration;
            }
            return CalibrationOutput;
        }
    }
}