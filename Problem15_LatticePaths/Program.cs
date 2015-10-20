using System;
using System.Collections.Generic;

namespace Problem15_LatticePaths
{
    class Program
    {
        struct Cell
        {
            public Cell(int row, int col)
            {
                this.row = row;
                this.col = col;
            }

            public int col;
            public int row;
        }

        static void Main(string[] args)
        {
            // Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, 
            // there are exactly 6 routes to the bottom right corner.
            //How many such routes are there through a 20×20 grid ?

            Console.WriteLine(Grid(20, new Cell { row = 0, col = 0 }));
            Console.Read();
        }

        static Dictionary<Cell, int> chache = new Dictionary<Cell, int>();

        static int Grid(int gridSize, Cell cell)
        {
            if (chache.ContainsKey(cell))
                return chache[cell];

            if (cell.row == gridSize && cell.col == gridSize) return 0;

            var rightValue = 0;
            var downValue = 0;
            if (cell.row < gridSize)
            {
                downValue = 1 + Grid(gridSize, new Cell(cell.row + 1, cell.col));
            }
            if (cell.col < gridSize)
            {
                rightValue = 1 + Grid(gridSize, new Cell(cell.row, cell.col + 1));
            }

            return chache[cell] = rightValue + downValue;
        }
    }
}
