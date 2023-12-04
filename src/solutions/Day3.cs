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
                            if (!AdjacentSymbol) { AdjacentSymbol = (CheckNeighbors(InputGrid, InputGrid.currentPosition.Row, InputGrid.currentPosition.Col, false)).Item1; } // Item1 = ValidNeighbor
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

            for (int currentRow = 0; currentRow < input.Length; currentRow++)
            {
                for (int currentCol = 0; currentCol < input.Length; currentCol++)
                {
                    InputGrid.SetPosition(currentRow, currentCol);
                    int UsedPosition = 0;

                    if (InputGrid.currentElement == '*')
                    {
                        (bool ValidNeighbor, int Position) Target = CheckNeighbors(InputGrid, InputGrid.currentPosition.Row, InputGrid.currentPosition.Col, true, UsedPosition);
                        if (Target.ValidNeighbor)
                        {
                            string ValidNumber = "0"; // Initialize ValidNumber for each digit
                            UsedPosition = Target.Position; // Skip value for searching

                            // Find number
                            Direction NewPosition = Direction.DownLeft; // Initialized default
                            switch (Target.Position) // Find direction from CheckNeighbors
                            {
                                case 0: { NewPosition = Direction.DownLeft; break; }
                                case 1: { NewPosition = Direction.Down; break; }
                                case 2: { NewPosition = Direction.DownRight; break; }
                                case 3: { NewPosition = Direction.Left; break; }
                                case 4: { NewPosition = Direction.Right; break; }
                                case 6: { NewPosition = Direction.UpLeft; break; }
                                case 7: { NewPosition = Direction.Up; break; }
                                case 8: { NewPosition = Direction.UpRight; break; }
                                default: { break; } // Invalid number check
                            }

                            InputGrid.MovePosition(NewPosition);
                            (int Row, int Col) StoredPosition = InputGrid.currentPosition;

                            // InputGrid.MovePosition(Grid.Direction.Left);
                            if (Char.IsDigit(InputGrid.currentElement)) // Don't need to check if symbol since we already know that it's near a '*'
                            {
                                List<char> CurrentNumber = new List<char>(); // Initialize CurrentNumber for each digit

                                while (Char.IsDigit(InputGrid.currentElement))
                                {
                                    CurrentNumber.Insert(0, InputGrid.currentElement); // Add to beginning since we are working backwards
                                    Console.WriteLine($"Found number:{InputGrid.currentElement}");
                                    if (InputGrid.IsValidPosition(InputGrid.currentPosition.Row, InputGrid.currentPosition.Col - 1)) {
                                        InputGrid.MovePosition(Grid.Direction.Left); // Look back
                                        currentCol = InputGrid.currentPosition.Col; // Move position backwards for loop
                                    }
                                    else { break; }
                                }

                                InputGrid.SetPosition(StoredPosition.Row,StoredPosition.Col); // Restore stored position
                                currentCol = InputGrid.currentPosition.Col + 1; // Update position to next value

                                InputGrid.MovePosition(Grid.Direction.Right);
                                while (Char.IsDigit(InputGrid.currentElement))
                                {
                                    CurrentNumber.Add(InputGrid.currentElement);
                                    Console.WriteLine($"Found number:{InputGrid.currentElement}");
                                    if (InputGrid.IsValidPosition(InputGrid.currentPosition.Row, InputGrid.currentPosition.Col + 1))
                                    {
                                        InputGrid.MovePosition(Grid.Direction.Right); // Look forwards
                                        currentCol = InputGrid.currentPosition.Col; // Move position forwards for loop
                                    }
                                    else { break; }
                                }

                                ValidNumber = new string(CurrentNumber.ToArray()); }

                                if (ValidNumber != "0") { Console.WriteLine($"Valid Number: {ValidNumber}"); } // Exclude default values
                                Output += int.Parse(ValidNumber);
                            }
                        }
                    }
                }
            return Output;
        }

        // Part 2
        // DONE: CheckNeighbors for '*'
        // DONE: If neighbors of '*' include a number, save position, then move back till numbers end, return to position, then move forward till numbers end
        // DONE: Add three sets of numbers together to create number
        // If that is the only number found, then stop
        // If another number is found, do the same process and multiply the two numbers together
        // Return output of multiplied numbers

        public static (bool, int) CheckNeighbors(Grid input, int row, int col, bool part2, int skip = 0)
        {
            (bool ValidNeighbor, int Position) Symbol = (false, 0); // Search order: Bottom Left => Bottom Middle => Bottom Right => Left => Right => Top Left => Top Middle => Top Right

            char[] AdjacentChars = input.GetNeighbors(row, col);
            if (!part2) // Part 1 needs the number to be adjacent to a symbol
            {
                foreach (char AdjacentChar in AdjacentChars)
                {
                    if (!char.IsDigit(AdjacentChar) && AdjacentChar != '.')
                    {
                        Symbol.ValidNeighbor = true;
                    }
                }
            }
            else // Part2 needs two numbers to be adjacent to a '*'
            {
                foreach (char AdjacentChar in AdjacentChars)
                {
                    if (char.IsDigit(AdjacentChar) && Symbol.Position >= skip)
                    {
                        Symbol.ValidNeighbor = true;
                    }
                    else
                    {
                        Symbol.Position++;
                    }
                }
            } 

            return (Symbol);
        }
    }
}