using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.src.tools
{
    public class Grid
    {
        private char[,] grid;

        public Grid(string[] input)
        {
            // Initialize grid from input strings
            int rows = input.Length;
            int cols = input[0].Length;
            grid = new char[rows, cols];

            for (int rowNum = 0; rowNum < rows; rowNum++)
            {
                for (int colNum = 0; colNum < cols; colNum++)
                {
                    grid[rowNum, colNum] = input[rowNum][colNum];
                }
            }
        }

        public char GetElement(int row, int col)
        {
            // Return element at specified position
            return grid[row, col];
        }

        public bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);
        }

        public char[] GetNeighbors(int row, int col)
        {
            List<char> neighbors = new List<char>();

            // Check vertically, horizontally, and diagonally for characters
            for (int rowNum = row - 1; rowNum <= row + 1; rowNum++)
            {
                for (int colNum = col - 1; colNum <= col + 1; colNum++)
                {
                    if (rowNum == row && colNum == col)
                    {
                        // Skip current position
                        continue;
                    }
                    else if (IsValidPosition(rowNum, colNum));
                    {
                        neighbors.Add(GetElement(row, col));
                    }
                }
            }

            return neighbors.ToArray();
        }
    }
}
