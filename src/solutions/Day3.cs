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
            int Output = 0;
            Coordinate CurrentValue = new Coordinate(InputData, 0, 0);
            CurrentValue.Grid = InputData;

            for (int XCoord = 0; XCoord < InputData.Length; XCoord++)
            {
                for (int YCoord = 0; YCoord < InputData[XCoord].Length; YCoord++)
                {
                    CurrentValue.XCoord = XCoord;
                    CurrentValue.YCoord = YCoord;

                    if (CurrentValue.IsDigit())
                    {
                        List<char> CurrentNumber = new List<char>(); // Initialize CurrentNumber for each digit
                        int lookaheadYCoord = YCoord;
                        bool AdjacentSymbol = false; // Initialize as false so it will only be true if a symbol is found in the current loop

                        while (CurrentValue.Look() != null && CurrentValue.IsDigit()) // Lookahead for numbers
                        {
                            if (!AdjacentSymbol) { AdjacentSymbol = CurrentValue.SymbolAdjacent(); }
                            CurrentNumber.Add(CurrentValue.Char);
                            lookaheadYCoord = CurrentValue.YCoord + 1; // Update the lookahead coordinate
                            CurrentValue = CurrentValue.Look();
                        }

                        YCoord = lookaheadYCoord; // Skip to the coordinate the lookahead stopped on
                    }
                }
            }

            return Output;
        }

        class Coordinate
        {
            public string[] Grid;
            public int XCoord;
            public int YCoord;
            public List<char> Value = new List<char>();
            public char Char
            {
                get
                {
                    // Ensure that the coordinates are within bounds
                    if (XCoord >= 0 && XCoord < Grid.Length && YCoord >= 0 && YCoord < Grid[XCoord].Length)
                    {
                        return Grid[XCoord][YCoord];
                    }
                    else
                    {
                        // Handle out-of-bounds coordinates gracefully
                        return '\0';
                    }
                }
            }

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
                Coordinate NextCharacter = new Coordinate(this.Grid, this.XCoord, this.YCoord);
                if (!Behind) { NextCharacter.YCoord++; }
                else { NextCharacter.YCoord--; }
                NextCharacter.XCoord = this.XCoord;

                if (NextCharacter.YCoord >= 0 && NextCharacter.YCoord <= NextCharacter.Grid.Length)
                {
                    return NextCharacter;
                }
                else { return null; }
            }

            public Coordinate(string[] grid, int xCoord, int yCoord)
            {
                Grid = grid;
                XCoord = xCoord;
                YCoord = yCoord;
            }
        }
    }
}