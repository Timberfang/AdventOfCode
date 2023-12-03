using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day3
    {
        public static int Puzzle(string[] inputData)
        {
            int Output = CheckAdjacency(inputData);

            return Output;
        }

        private static int CheckAdjacency(string[] input)
        {
            bool AdjacentSymbol = false;
            int Output = 0;

            for (int XCoord = 0; XCoord < input.Length; XCoord++)
            {
                for (int YCoord = 0; YCoord < input[XCoord].Length; YCoord++) // Travels from left to right, top to bottom
                {
                    char CurrentChar = input[XCoord][YCoord];

                    // Check if it's a number
                    if (char.IsDigit(CurrentChar))
                    {
                        List<char> CurrentNumber = new List<char>();
                        CurrentNumber.Add(CurrentChar);

                        // if (YCoord > 0) // Look behind to find negative numbers
                        // {
                        //     char PreviousChar = input[XCoord][YCoord - 1];
                        // 
                        //     if (PreviousChar == '-') { CurrentNumber.Insert(0, PreviousChar); }
                        // }

                        // Check adjacency and handle accordingly
                        AdjacentSymbol = CheckAdjacentElements(input, XCoord, YCoord);

                        int LookAheadCoord = YCoord + 1; // Look ahead to find multi-digit numbers
                        if (LookAheadCoord < input.Length)
                        {
                            char NextChar = input[XCoord][LookAheadCoord];
                        
                            while (char.IsDigit(NextChar))
                            {
                                if(!AdjacentSymbol) { AdjacentSymbol = CheckAdjacentElements(input, XCoord, LookAheadCoord); }
                        
                                CurrentNumber.Add(input[XCoord][LookAheadCoord]);
                                LookAheadCoord++;
                                if (LookAheadCoord < input.Length) { NextChar = input[XCoord][LookAheadCoord]; }
                                else { NextChar = input[XCoord+1][YCoord]; }
                            }
                        
                            YCoord = LookAheadCoord; // Skip to the end of the number
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

        private static bool CheckAdjacentElements(string[] Grid, int Row, int Col)
        {
            // Define the eight possible directions (up, down, left, right, and diagonals)
            int[] RowDirs = { -1, -1, -1,  0, 0,  1, 1, 1 };
            int[] ColDirs = { -1,  0,  1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int NewRow = Row + RowDirs[i];
                int NewCol = Col + ColDirs[i];

                // Check if the new position is within the boundaries of the grid
                if (NewRow >= 0 && NewRow < Grid.Length && NewCol >=0 && NewCol < Grid[NewRow].Length)
                {
                    char AdjacentChar = Grid[NewRow][NewCol];

                    // Compare the current character with the adjacent character
                    if (!char.IsDigit(AdjacentChar) && AdjacentChar != '.')
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}