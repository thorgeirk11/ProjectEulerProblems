using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P244
{
    class P244
    {
        static void Main(string[] args)
        {
            var frontier = new List<Path>();
            var shortest = new List<Path>();

            var shortestSol = int.MaxValue;

            var cur = new Path(Root)
            {
                Heuristics = Heuristics(Root)
            };
            do
            {
                if (cur.Heuristics + cur.Depth <= shortestSol)
                {
                    foreach (var board in AllPaths(cur.Board))
                    {
                        if (cur.Boards.Contains(board))
                            continue;
                        var nextPath = cur.Next(board);
                        nextPath.Heuristics = Heuristics(board);
                        frontier.Add(nextPath);
                    }
                }
                else
                {

                }
                frontier.Sort((a, b) => a.Heuristics.CompareTo(b.Heuristics));

                cur = frontier.First();
                frontier.RemoveAt(0);
                if (cur.Heuristics == 0)
                {
                    if (shortestSol > cur.Depth)
                    {
                        shortestSol = cur.Depth;
                        shortest.RemoveAll(i => i.Depth > shortestSol);
                    }
                    shortest.Add(cur);
                }
            } while (frontier.Count > 0);

            Console.WriteLine(cur);
            Console.WriteLine(Heuristics(cur.Board));

            Console.WriteLine();
        }

        static int Heuristics(Board board)
        {
            var distance = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (row == board.X && col == board.Y) continue;
                    if (row == Goal.X && col == Goal.Y) continue;
                    if (board[row, col] != Goal[row, col])
                    {
                        distance += Math.Abs(board.X - row) + Math.Abs(board.Y - col);
                    }
                }
            }
            distance += Math.Abs(board.X - Goal.X) + Math.Abs(board.Y - Goal.Y);
            return distance;
        }

        static IEnumerable<Board> AllPaths(Board state)
        {
            if (state.CanDown) yield return state.Down;
            if (state.CanUp) yield return state.Up;
            if (state.CanRight) yield return state.Right;
            if (state.CanLeft) yield return state.Left;
        }

        #region Root & Goals
        static Board Root = new Board(
            tiles: new bool[,]
            {
                { true, true, false, false },
                { true, true, false, false },
                { true, true, false, false },
                { true, true, false, false },
            },
            x: 0,
            y: 0
        );

        static Board Goal = new Board(
            tiles: new bool[,]
            {
                { true, false, true, false },
                { false, true, false, true },
                { true, false, true, false },
                { false, true, false, true },
            },
            x: 0,
            y: 0
        );
        #endregion
    }

    public class Path : IEquatable<Path>
    {
        public Board Board { get; }
        public Path Previus { get; }
        public int Heuristics { get; set; }

        public int Depth => (Previus?.Depth ?? 0) + 1;

        public IEnumerable<Board> Boards
        {
            get
            {
                yield return Board;

                if (Previus != null)
                {
                    foreach (var prevBoards in Previus.Boards)
                    {
                        yield return prevBoards;
                    }
                }
            }
        }

        public Path(Board cur) { Board = cur; }
        private Path(Path prev, Board cur)
        {
            Previus = prev;
            Board = cur;
        }
        public Path Next(Board board) => new Path(this, board);

        public bool Equals(Path other) =>
            Board.Equals(other.Board) && (Previus?.Equals(other.Previus) ?? Previus == null && other.Previus == null);
        public override bool Equals(object other) => Equals(other as Path);
        public override int GetHashCode() => Board.GetHashCode() ^ (Previus?.GetHashCode() ?? 0);
        public override string ToString() => Board.ToString();
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public struct Board
    {
        private readonly int _tiles;
        public int X { get; }
        public int Y { get; }
        //public Direction ReachDir { get; }

        public Board(bool[,] tiles, int x, int y)
        {
            _tiles = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (tiles[row, col])
                    {
                        _tiles |= 1 << row * 4 + col;
                    }
                }
            }
            X = x;
            Y = y;
        }
        private Board(int tiles, int x, int y)
        {
            _tiles = tiles;
            X = x;
            Y = y;
        }

        public bool CanDown => X > 0;
        public bool CanLeft => Y > 0;
        public bool CanRight => Y < 3;
        public bool CanUp => X < 3;

        public Board Down => NewState(X - 1, Y);
        public Board Left => NewState(X, Y - 1);
        public Board Right => NewState(X, Y + 1);
        public Board Up => NewState(X + 1, Y);

        public bool this[int row, int col] => ((_tiles >> row * 4 + col) & 1) == 1;

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (row == X && col == Y) sb.Append("  ");
                    else sb.Append(this[row, col] ? "t " : "f ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private Board NewState(int x, int y) => ((_tiles >> x * 4 + y) & 1) == 1 ?
            new Board(_tiles | (1 << X * 4 + Y), x, y) :
            new Board(_tiles & ~(1 << X * 4 + Y), x, y);
    }
}
