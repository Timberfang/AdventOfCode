using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day6
    {
        // Puzzle entry point
        public static long Puzzle(string[] input, bool part2 = false)
        {
            // Splitting input string into its component lines
            long[] RaceDuration = ConvertInput(input[0], part2);
            long[] RaceRecord = ConvertInput(input[1], part2);

            long Output = CalculateSolution(RaceDuration, RaceRecord);
            return Output;
        }

        // Calculate puzzle solution
        public static long CalculateSolution(long[] RaceDuration, long[] RaceRecord)
        {
            long Output = 1; // Initialize output - we need to multiply the winning values for each race together

            // RaceDuration & RaceRecord should always have the same length, so either can be used here to represent the number of races
            for (int RaceIndex = 0; RaceIndex < RaceDuration.Length; RaceIndex++)
            {
                long WinningCombinations = 0; // Initialize for each race, counter for number of combinations
                for (long ButtonDuration = 1; ButtonDuration < RaceDuration[RaceIndex]; ButtonDuration++)
                {
                    // Distance traveled = speed per unit of time * remaining units of time; speed = button duration; remaining units of time = RaceDuration - ButtonDuration
                    long DistanceTraveled = ButtonDuration * (RaceDuration[RaceIndex] - ButtonDuration);
                    if (DistanceTraveled > RaceRecord[RaceIndex])
                    {
                        WinningCombinations++;
                    }
                }

                Output *= WinningCombinations;
            }

            return Output;
        }

        // Parse puzzle input
        public static long[] ConvertInput(string input, bool part2)
        {
            long[] Output;

            if (!part2)
            {
                // Extract numbers and convert to integers, splitting by spaces
                string[] InputNumbers = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Output = InputNumbers.Where(s => long.TryParse(s, out _)).Select(s => long.Parse(s)).ToArray();
            }
            else
            {
                // Extract numbers and convert to integers, concatenating
                string InputNumbers = string.Concat(input.Where(char.IsDigit));
                Output = new long[] { long.Parse(InputNumbers) };
            }

            return Output;
        }
    }
}
