using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day3
    {
        public static int Puzzle(string[] InputData)
        {
            // int Output = CheckAdjacency(inputData);
            int Output = 0;
            Coordinate CurrentValue = new Coordinate();
            CurrentValue.Grid = InputData;

            for (int XCoord = 0;  XCoord < InputData.Length; XCoord++)
            {
                for (int YCoord = 0; YCoord < InputData[XCoord].Length; YCoord++)
                {
                    CurrentValue.XCoord = XCoord;
                    CurrentValue.YCoord = YCoord;
                    CurrentValue.Char = InputData[CurrentValue.XCoord][CurrentValue.YCoord];

                    if (CurrentValue.IsDigit())
                    {
                        Console.WriteLine($"Adding value {CurrentValue.Char}");
                        CurrentValue.Value.Add(CurrentValue.Char);
                        // List<char> CharList = CurrentValue.Value;

                        Coordinate NextValue = CurrentValue.Look();
                        while (NextValue != null && NextValue.IsDigit()) { CurrentValue.Value.Append(NextValue.Char); CurrentValue.XCoord = NextValue.XCoord; CurrentValue.YCoord = NextValue.YCoord; NextValue = CurrentValue.Look(); }

                        string charString = new string(CurrentValue.Value.ToArray());
                        Output += int.Parse(charString);
                        CurrentValue.Value.Clear();
                    }
                }
            }

            return Output;
        }

        // private static int CheckAdjacency(string[] input)
        // {
        //     int Output = 0;
        // 
        //     for (int XCoord = 0; XCoord < input.Length; XCoord++)
        //     {
        //         for (int YCoord = 0; YCoord < input[XCoord].Length; YCoord++) // Travels from left to right, top to bottom
        //         {
        //             char CurrentChar = input[XCoord][YCoord];
        //     
        //             // Check if it's a number
        //             if (char.IsDigit(CurrentChar))
        //             {
        //                 List<char> CurrentNumber = new List<char>();
        //                 CurrentNumber.Add(CurrentChar);
        //     
        //                 if (YCoord > 0) // Look behind to find negative numbers
        //                 {
        //                     char PreviousChar = input[XCoord][YCoord - 1];
        //                 
        //                     if (PreviousChar == '-') { CurrentNumber.Insert(0, PreviousChar); }
        //                 }
        //     
        //                 Check adjacency and handle accordingly
        //                 AdjacentSymbol = CheckAdjacentElements(input, XCoord, YCoord);
        //     
        //     
        //     
        //                 int NumberInteger = int.Parse(CurrentNumber.ToArray());
        //                 Console.WriteLine($"Number found: {NumberInteger}");
        //                 if (AdjacentSymbol)
        //                 {
        //                     Console.WriteLine("...And it has an adjacent symbol!");
        //                     Output += NumberInteger;
        //                 }
        //             }
        //         }
        //     }
        // 
        //     return Output;
        // }

        // private static List<char> LookAhead(string[] Grid, int Row, int Col)
        // private static void LookAhead(string[] Grid, int Row, int Col)
        // {
        //     bool AdjacentSymbol = false;
        //     int LookAheadCoord = Col + 1; // Look ahead to find multi-digit numbers
        //     if (LookAheadCoord < Grid.Length)
        //     {
        //         char NextChar = Grid[Row][LookAheadCoord];
        // 
        //         while (char.IsDigit(NextChar))
        //         {
        //             if (!AdjacentSymbol) { AdjacentSymbol = CheckAdjacentElements(Grid, Row, LookAheadCoord); }
        // 
        //             CurrentNumber.Add(input[XCoord][LookAheadCoord]);
        //             LookAheadCoord++;
        //             if (LookAheadCoord < input.Length) { NextChar = input[XCoord][LookAheadCoord]; }
        //             else { NextChar = input[XCoord + 1][YCoord]; }
        //         }
        // 
        //         YCoord = LookAheadCoord; // Skip to the end of the number
        //     }
        // }

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

        class Coordinate
        {
            public string[] Grid;
            public int XCoord;
            public int YCoord;
            public char Char;
            public List<char> Value = new List<char>();

            public bool IsDigit()
            {
                return char.IsDigit(this.Char);
            }

            public bool SymbolAdjacent()
            {
                // Define the eight possible directions (up, down, left, right, and diagonals)
                int[] RowDirs = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] ColDirs = { -1, 0, 1, -1, 1, -1, 0, 1 };

                for (int i = 0; i < 8; i++)
                {
                    int NewRow = this.XCoord + RowDirs[i];
                    int NewCol = this.YCoord + ColDirs[i];

                    // Check if the new position is within the boundaries of the grid
                    if (NewRow >= 0 && NewRow < this.Grid.Length && NewCol >= 0 && NewCol < this.Grid[NewRow].Length)
                    {
                        char AdjacentChar = this.Grid[NewRow][NewCol];

                        // Compare the current character with the adjacent character
                        if (!char.IsDigit(AdjacentChar) && AdjacentChar != '.')
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public Coordinate Look(bool Behind = false)
            {
                Coordinate NextCharacter = new Coordinate();
                NextCharacter.Grid = this.Grid;
                if (!Behind) { NextCharacter.YCoord = this.YCoord + 1; }
                else { NextCharacter.YCoord = this.YCoord - 1; }
                NextCharacter.XCoord = this.XCoord;

                if (NextCharacter.YCoord > 0 && NextCharacter.YCoord < NextCharacter.Grid.Length)
                {
                    NextCharacter.Char = NextCharacter.Grid[NextCharacter.XCoord][NextCharacter.YCoord];

                    return NextCharacter;
                }
                else { return null; }
            }
        }
    }
}