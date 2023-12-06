using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day4
    {
        public static int Puzzle(string[] input, bool part2)
        {
            int Output;

            // Remove the prefix from each line
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], @"^Card\s+" + (i + 1) + ":\\s+", "");
            }

            if (part2) { Output = Part2(input); }
            else { Output = Part1(input); }

            return Output;
        }

        public static int Part1(string[] input)
        {
            int Output = 0;

            // Remove the prefix from each line
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], @"^Card\s+" + (i + 1) + ":\\s+", "");
            }

            foreach (string line in input)
            {
                string cleanLine = line;

                // Split the string based on '|'
                string[] SplitString = cleanLine.Split('|');

                // Parse the substrings into arrays of integers
                int[] WinningArray = SplitString[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int[] TestArray = SplitString[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                // Compute number of winning values - that is, the count of the intersection of the two arrays, or the number of values that are present in both the first array and the second
                int Matches = WinningArray.Intersect(TestArray).Count();

                // Compute point values
                /// Points = 2^(matches-1)
                /// If matches = 1, points = 2^0 = 1
                /// If matches = 2, points = 2^1 = 2
                /// If matches = 3, points = 2^2 = 4
                /// ...
                int Points = (int)Math.Pow(2, Matches - 1); // This should never go beyond the range of an int based off of the dataset
                Output += Points;
            }

            return Output;
        }

        public static int Part2(string[] input)
        {
            int Output = 0;

            // Remove the prefix from each line
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], @"^Card\s+" + (i + 1) + ":\\s+", "");
            }

            // Create a list to store the 204 cards
            var Cards = new List<Card>();

            int CardNumber = 1; // Increased each loop, so first loop will use card 1

            foreach (string line in input)
            {
                string cleanLine = line;

                // Split the string based on '|'
                string[] SplitString = cleanLine.Split('|');

                // Parse the substrings into arrays of integers
                int[] WinningArray = SplitString[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int[] TestArray = SplitString[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                // Compute number of winning values - that is, the count of the intersection of the two arrays, or the number of values that are present in both the first array and the second
                int Matches = WinningArray.Intersect(TestArray).Count();

                var card = new Card
                {
                    WinningValues = Matches,
                    CardCount = 1
                };

                Cards.Add(card); // Add the card instance to the list
            }

            int CurrentCard = 0;
            while (CurrentCard < Cards.Count - 1) // Iterate through each card, stopping to avoid hitting the list end
            {
                int Max = Cards[CurrentCard].WinningValues;
                int incrementCard = 1; // Work in reverse order from max value
                while (incrementCard <= Max)
                {
                    Cards[CurrentCard + incrementCard].CardCount += Cards[CurrentCard].CardCount;
                    incrementCard++;
                }
                CurrentCard++;

                Console.WriteLine($"Card count for {CurrentCard + 1}: {Cards[CurrentCard].CardCount}");
                Output += Cards[CurrentCard].CardCount;
            }
            Output += 1; // Add first card which is skipped in the loop

            return Output;
        }

        public class Card
        {
            public int WinningValues { get; set; }
            public int CardCount {  get; set; }
        }
    }
}
