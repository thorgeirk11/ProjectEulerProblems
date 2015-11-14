using System;
using System.Collections.Generic;
using System.Diagnostics;

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

            public override string ToString()
            {
                return "(" + row + "," + col + ")";
            }
        }

        static void Main(string[] args)
        {
            // Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, 
            // there are exactly 6 routes to the bottom right corner.
            //How many such routes are there through a 20×20 grid ?

            var st = Stopwatch.StartNew();
            Console.WriteLine(Grid(20, new Cell { row = 0, col = 0 }));
            Console.WriteLine(st.ElapsedMilliseconds);
            Console.Read();
        }

        static Dictionary<Cell, long> CachedCells = new Dictionary<Cell, long>();

        static long Grid(int gridSize, Cell cell)
        {
            if (CachedCells.ContainsKey(cell))
                return CachedCells[cell];

            if (cell.row == gridSize && cell.col == gridSize) return 1;

            long rightValue = 0;
            long downValue = 0;
            if (cell.row < gridSize)
            {
                downValue = Grid(gridSize, new Cell(cell.row + 1, cell.col));
            }
            if (cell.col < gridSize)
            {
                rightValue = Grid(gridSize, new Cell(cell.row, cell.col + 1));
            }

            return CachedCells[cell] = rightValue + downValue;
        }
    }
}
