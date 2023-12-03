using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day3
    {
        public static int Puzzle(string[] inputData)
        {
            // Input Data
            // string[] inputData = Input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            int Output = CheckAdjacency(inputData);

            return Output;
        }

        private static int CheckAdjacency(string[] input)
        {
            bool AdjacentSymbol = false;
            int Output = 0;

            for (int x = 0; x < input.Length; x++)
            {
                for (int y = 0; y < input[x].Length; y++) // Travels from left to right, top to bottom
                {
                    char CurrentChar = input[x][y];

                    // Check if it's a number
                    if (char.IsDigit(CurrentChar))
                    {
                        List<char> CurrentNumber = new List<char>();
                        CurrentNumber.Add(CurrentChar);

                        // if (y > 0) // Look behind to find negative numbers
                        // {
                        //     char PreviousChar = input[x][y - 1];
                        // 
                        //     if (PreviousChar == '-') { CurrentNumber.Insert(0, PreviousChar); }
                        // }

                        // Check adjacency and handle accordingly
                        AdjacentSymbol = CheckAdjacentElements(input, x, y);

                        int z = y + 1; // Look ahead to find multi-digit numbers
                        if (z < input.Length)
                        {
                            char NextChar = input[x][z];
                        
                            while (char.IsDigit(NextChar))
                            {
                                if(!AdjacentSymbol) { AdjacentSymbol = CheckAdjacentElements(input, x, z); }
                        
                                CurrentNumber.Add(input[x][z]);
                                z++;
                                if (z < input.Length) { NextChar = input[x][z]; }
                                else { NextChar = input[x+1][y]; }
                            }
                        
                            y = z; // Skip to the end of the number
                        }

                        int NumberInteger = int.Parse(CurrentNumber.ToArray());
                        Console.WriteLine($"Number found: {NumberInteger}");
                        if (AdjacentSymbol)
                        {
                            Console.WriteLine("...And it has an adjacent symbol!");
                            Output += NumberInteger;
                        }
                    }
                }
            }

            return Output;
        }

        private static bool CheckAdjacentElements(string[] grid, int row, int col)
        {
            // Define the eight possible directions (up, down, left, right, and diagonals)
            int[] rowDirs = { -1, -1, -1,  0, 0,  1, 1, 1 };
            int[] colDirs = { -1,  0,  1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int newRow = row + rowDirs[i];
                int newCol = col + colDirs[i];

                // Check if the new position is within the boundaries of the grid
                if (newRow >= 0 && newRow < grid.Length && newCol >=0 && newCol < grid[newRow].Length)
                {
                    char adjacentChar = grid[newRow][newCol];

                    // Compare the current character with the adjacent character
                    if (!char.IsDigit(adjacentChar) && adjacentChar != '.')
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}