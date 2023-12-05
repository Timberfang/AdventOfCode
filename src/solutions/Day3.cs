using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.src.tools;
using static AdventOfCode.src.tools.Grid;

namespace AdventOfCode.src.solutions
{
    public class Day3
    {
        public static int Puzzle(string[] input, bool part2)
        {
            int Output;

            if (part2) { Output = Part2(input); }
            else { Output = Part1(input); }

            return Output;
        }
        
        public static int Part1(string[] input)
        {
            int Output = 0;
            Grid InputGrid = new Grid(input);

            for (int currentRow = 0; currentRow < input.Length; currentRow++)
            {
                for (int currentCol = 0; currentCol < input.Length; currentCol++)
                {
                    InputGrid.SetPosition(currentRow, currentCol);

                    if (Char.IsDigit(InputGrid.currentElement))
                    {

                        List<char> CurrentNumber = new List<char>(); // Initialize CurrentNumber for each digit
                        // List<char> ValidNumber = new List<char>(); // Initialize ValidNumber for each digit
                        string ValidNumber = "0";
                        bool AdjacentSymbol = false;

                        while (Char.IsDigit(InputGrid.currentElement))
                        {
                            if (!AdjacentSymbol)
                            {
                                NeighborInfo NeighborSymbol = CheckNeighbors(InputGrid, InputGrid.currentPosition.Row, InputGrid.currentPosition.Col);
                                AdjacentSymbol = NeighborSymbol.Character != '\0';
                            }
                            CurrentNumber.Add(InputGrid.currentElement);
                            InputGrid.MovePosition(Grid.Direction.Right);
                            currentCol = InputGrid.currentPosition.Col; // Move position forwards for loop
                        }

                        if (AdjacentSymbol) { ValidNumber = new string(CurrentNumber.ToArray()); }
                        if (ValidNumber != "0") { Console.WriteLine($"Valid Number: {ValidNumber}"); } // Exclude default values
                        Output += int.Parse(ValidNumber);
                    }
                }
            }

            return Output;
        }

        public static int Part2(string[] input)
        {
            int Output = 0;
            Grid InputGrid = new Grid(input);
            Dictionary<(int Row, int Col), List<int>> ValidNumbers = new Dictionary<(int, int), List<int>>(); // Record numbers adjacent to asterisk characters

            // Process numbers
            NeighborInfo NeighborSymbol = new NeighborInfo();
            for (int currentRow = 0; currentRow < input.Length; currentRow++)
            {
                for (int currentCol = 0; currentCol < input.Length; currentCol++)
                {
                    InputGrid.SetPosition(currentRow, currentCol);
        
                    if (Char.IsDigit(InputGrid.currentElement) && InputGrid.currentElement != '*')
                    {
                        List<char> CurrentNumber = new List<char>(); // Store current number as a list of characters to be assembled later into an integer
                        bool AdjacentSymbol = false; // Records whether at least one digit has an adjacent asterisk - if it doesn't no checking is necessary

                        while (Char.IsDigit(InputGrid.currentElement))
                        {
                            if (!AdjacentSymbol)
                            {
                                NeighborSymbol = CheckNeighbors(InputGrid, InputGrid.currentPosition.Row, InputGrid.currentPosition.Col);
                                AdjacentSymbol = NeighborSymbol.Character == '*';
                            }
                            CurrentNumber.Add(InputGrid.currentElement);
                            InputGrid.MovePosition(Grid.Direction.Right);
                            currentCol = InputGrid.currentPosition.Col;
                        }
        
                        if (AdjacentSymbol)
                        {
                            if (!ValidNumbers.ContainsKey((NeighborSymbol.Row,NeighborSymbol.Col)))
                            {
                                ValidNumbers[(NeighborSymbol.Row, NeighborSymbol.Col)] = new List<int>();
                            }

                            ValidNumbers[(NeighborSymbol.Row, NeighborSymbol.Col)].Add(int.Parse(new string(CurrentNumber.ToArray())));
                        }
                    }
                }
            }

            // Process the numbers associated with each asterisk
            foreach (var Number in ValidNumbers)
            {
                if (Number.Value.Count > 1)
                {
                    // If there are multiple numbers associated with the same asterisk, process them, else skip the number as it is invalid
                    int product = Number.Value[0] * Number.Value[1];
                    Console.WriteLine($"Match detected at ({Number.Key.Row},{Number.Key.Col}): {Number.Value[0]} * {Number.Value[1]} = {product}");
                    Output += product;
                }
            }

            return Output;
        }

        public static NeighborInfo CheckNeighbors(Grid input, int row, int col) // Check numbers for neighboring symbols - return all neighboring symbols along with their coordinates
        {
            NeighborInfo result = new NeighborInfo();

            NeighborInfo[] adjacentChars = input.GetNeighbors(row, col);

            foreach (NeighborInfo adjacentChar in adjacentChars)
            {
                if (!char.IsDigit(adjacentChar.Character) && adjacentChar.Character != '.')
                {
                    result = new NeighborInfo { Row = adjacentChar.Row, Col = adjacentChar.Col, Character = adjacentChar.Character };
                }
            }

            return result;
        }
    }
}