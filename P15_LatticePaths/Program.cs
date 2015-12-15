namespace Problem15_LatticePaths
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class Program
    {
        private struct Cell
        {
            public int Col { get; }
            public int Row { get; }

            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public Cell Down => new Cell(Row + 1, Col);
            public Cell Right => new Cell(Row, Col + 1);

            public override string ToString() => $"({Row},{Col})";
        }

        const int GridSize = 20;

        static void Main(string[] args)
        {
            // Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down,
            // there are exactly 6 routes to the bottom right corner.
            //How many such routes are there through a 20×20 grid ?
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000; i++)
            {
                CachedCells.Clear();
                PathCount(new Cell(0, 0));
            }
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.Read();
        }

        static Dictionary<Cell, long> CachedCells = new Dictionary<Cell, long>();

        static long PathCount(Cell cell)
        {
            if (CachedCells.ContainsKey(cell)) return CachedCells[cell];
            if (cell.Row == GridSize && cell.Col == GridSize) return 1;
            if (cell.Row > GridSize || cell.Col > GridSize) return 0;
            return CachedCells[cell] = PathCount(cell.Down) + PathCount(cell.Right);
        }
    }
}
