namespace AdventOfCode.src.tools
{
    public class Grid
    {
        private char[,] grid;
        private int currentRow;
        private int currentCol;
        public char currentElement
        {
            get
            {
                if (IsValidPosition(currentRow, currentCol)) { return GetElement(currentRow, currentCol); }
                else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); } // Invalid position handling
            }
        }
        public (int Row, int Col) currentPosition
        {
            get
            {
                if (IsValidPosition(currentRow, currentCol)) { return (currentRow, currentCol); }
                else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); } // Invalid position handling
            }
        }

        public Grid(string[] input)
        {
            // Initialize grid from input strings
            int rows = input.Length;
            int cols = input[0].Length;
            grid = new char[rows, cols];

            // Set grid data to input grid data
            for (int rowNum = 0; rowNum < rows; rowNum++)
            {
                for (int colNum = 0; colNum < cols; colNum++)
                {
                    grid[rowNum, colNum] = input[rowNum][colNum];
                }
            }

            // Set initial position
            currentRow = 0;
            currentCol = 0;
        }

        public char GetElement(int row, int col)
        {
            // Return element at specified position
            return grid[row, col];
        }

        public void SetElement (int row, int col, char input)
        {
            if (IsValidPosition (row, col))
            {
                // Set grid position to given character
                grid[row, col] = input;
            }
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
                    else if (IsValidPosition(rowNum, colNum))
                    {
                        neighbors.Add(GetElement(rowNum, colNum));
                    }
                }
            }

            return neighbors.ToArray();
        }

        public void MovePosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (IsValidPosition(currentRow - 1, currentCol)) { currentRow--; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.Down:
                    if (IsValidPosition(currentRow + 1, currentCol)) { currentRow++; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.Left:
                    if (IsValidPosition(currentRow, currentCol - 1)) { currentCol--; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.Right:
                    if (IsValidPosition(currentRow , currentCol + 1)) { currentCol++; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.UpLeft:
                    if (IsValidPosition(currentRow - 1, currentCol - 1)) { currentRow--; currentCol--; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.UpRight:
                    if (IsValidPosition(currentRow - 1, currentCol + 1)) { currentRow--; currentCol++; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.DownLeft:
                    if (IsValidPosition(currentRow + 1, currentCol - 1)) { currentRow++; currentCol--; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                case Direction.DownRight:
                    if (IsValidPosition(currentRow + 1, currentCol + 1)) { currentRow++; currentCol++; }
                    else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
                    break;
                default:
                    // Handle unexpected direction
                    break;
            }
        }
        public void SetPosition(int row, int col)
        {
            if (IsValidPosition(row, col)) { currentRow = row; currentCol = col; }
            else { throw new IndexOutOfRangeException("Position exceeds grid boundaries"); }
        }

        public enum Direction { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight }
    }
}
