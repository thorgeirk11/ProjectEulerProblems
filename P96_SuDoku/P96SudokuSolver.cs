using System;
using System.Collections.Generic;
using System.Linq;
using static P96.SuDoku.Possible;

namespace P96.SuDoku
{
    [Flags]
    public enum Possible
    {
        One = 1 << 1,
        Two = 1 << 2,
        Three = 1 << 3,
        Four = 1 << 4,
        Five = 1 << 5,
        Six = 1 << 6,
        Seven = 1 << 7,
        Eight = 1 << 8,
        Nine = 1 << 9,
        All = One | Two | Three | Four | Five | Six | Seven | Eight | Nine
    }
    public static class P96SudokuSolver
    {
        public static Possible[] posabilities = new[] { One, Two, Three, Four, Five, Six, Seven, Eight, Nine };
        public static int TheSum;

        public static int[,] SolveSudoku(int[,] board)
        {
            var root = InizialzeRoot();
            FillOut(board, root);

            int[,] solution;
            if (IsCorrect(root))
            {
                solution = board;
            }
            else
            {
                solution = DFS(board, root);
            }

            return solution;
        }

        public static Possible[,] InizialzeRoot()
        {
            var root = new Possible[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    root[row, col] = All;
                }
            }
            return root;
        }

        public static void CellIndexWithFewestFlags(Possible[,] root, out int row, out int col)
        {
            row = -1;
            col = -1;
            var lowestNumber = int.MaxValue;
            for (int iRow = 0; iRow < 9; iRow++)
            {
                for (int iCol = 0; iCol < 9; iCol++)
                {
                    var cell = root[iRow, iCol];
                    if (!OnlyOnePosiblity(cell))
                    {
                        var numberOfPosibiltiesInCell = GetFlags(cell).Count();

                        if (lowestNumber > numberOfPosibiltiesInCell)
                        {
                            row = iRow;
                            col = iCol;
                            lowestNumber = numberOfPosibiltiesInCell;
                        }
                    }
                }
            }
        }

        static int[,] DFS(int[,] board, Possible[,] root)
        {
            int row, col;
            CellIndexWithFewestFlags(root, out row, out col);
            foreach (var number in GetFlags(root[row, col]).ToList())
            {
                var tempBoard = new int[9, 9];
                var tempRoot = new Possible[9, 9];
                Array.Copy(board, tempBoard, 9 * 9);
                Array.Copy(root, tempRoot, 9 * 9);

                root[row, col] &= ~number;
                tempBoard[row, col] = CellValue(number);

                FillOut(tempBoard, tempRoot);

                var correct = false;
                if (HasError(tempRoot, out correct)) continue;
                if (correct) return tempBoard;

                var solution = DFS(tempBoard, tempRoot);
                if (solution != null) return solution;
            }
            return null; // None of the guesses were correct.
        }

        public static bool HasError(Possible[,] arr, out bool IsCorrect)
        {
            IsCorrect = true;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (arr[row, col] == 0) return true;
                    if (!OnlyOnePosiblity(arr[row, col]))
                        IsCorrect = false;
                }
            }
            return false;
        }
        public static void FillOut(int[,] board, Possible[,] root)
        {
            do
            {
                FillOut(board, root, true);
            }
            while (FillOut(board, root, false));
        }
        private static bool FillOut(int[,] board, Possible[,] posArray, bool useSimpleMethod)
        {
            var anyChangeAtAll = false;
            var changed = false;
            do
            {
                changed = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        Possible possible;
                        if (useSimpleMethod)
                        {
                            possible = posArray[row, col] = CheckCell(board, row, col);
                        }
                        else
                        {
                            possible = CheckCellWithPos(posArray, row, col);
                        }
                        if (possible == 0) return false;
                        if (board[row, col] == 0 && OnlyOnePosiblity(possible))
                        {
                            board[row, col] = CellValue(possible);
                            changed = true;
                            anyChangeAtAll = true;
                        }
                    }
                }
            } while (changed);
            return anyChangeAtAll;
        }
        public static bool IsCorrect(Possible[,] posArray)
        {
            var isCorrect = false;
            return !HasError(posArray, out isCorrect) && isCorrect;
        }
        public static Possible CheckCellWithPos(Possible[,] root, int row, int col)
        {
            var possible = root[row, col];
            if (OnlyOnePosiblity(possible)) return possible;
            foreach (var number in GetFlags(possible).ToList())
            {
                var onlyInRow = true;
                var onlyInCol = true;
                for (int i = 0; i < 9; i++)
                {
                    if (i != col && root[row, i].HasFlag(number))
                    {
                        onlyInRow = false;
                    }
                    if (i != row && root[i, col].HasFlag(number))
                    {
                        onlyInCol = false;
                    }
                    if (!onlyInCol && !onlyInRow) break;
                }
                if (onlyInCol || onlyInRow)
                {
                    return number;
                }

                if (OnlyInBox(root, row, col, number))
                {
                    return number;
                }
            }
            return possible;
        }

        private static bool OnlyInBox(Possible[,] root, int row, int col, Possible number)
        {
            var boxRow = (row / 3) * 3;
            var boxCol = (col / 3) * 3;
            for (int iCol = boxCol; iCol < boxCol + 3; iCol++)
            {
                for (int iRow = boxRow; iRow < boxRow + 3; iRow++)
                {
                    if (iCol == col && iRow == row) continue;
                    if (root[iRow, iCol].HasFlag(number)) return false;
                }
            }
            return true;
        }

        public static bool OnlyOnePosiblity(Possible possible) => (possible & (possible - 1)) == 0;

        public static Possible CheckCell(int[,] board, int row, int col)
        {
            if (board[row, col] != 0) return (Possible)(1 << board[row, col]);

            var possible = All;
            for (int i = 0; i < 9; i++)
            {
                possible &= (Possible)~(1 << board[row, i]);
                possible &= (Possible)~(1 << board[i, col]);
            }
            var boxRow = (row / 3) * 3;
            var boxCol = (col / 3) * 3;
            for (int iCol = boxCol; iCol < boxCol + 3; iCol++)
            {
                for (int iRow = boxRow; iRow < boxRow + 3; iRow++)
                {
                    possible &= (Possible)~(1 << board[iRow, iCol]);
                }
            }
            return possible;
        }

        public static IEnumerable<Possible> GetFlags(Possible input)
        {
            if (input.HasFlag(One)) yield return One;
            if (input.HasFlag(Two)) yield return Two;
            if (input.HasFlag(Three)) yield return Three;
            if (input.HasFlag(Four)) yield return Four;
            if (input.HasFlag(Five)) yield return Five;
            if (input.HasFlag(Six)) yield return Six;
            if (input.HasFlag(Seven)) yield return Seven;
            if (input.HasFlag(Eight)) yield return Eight;
            if (input.HasFlag(Nine)) yield return Nine;
        }

        private static int CellValue(Possible pos) => Array.IndexOf(posabilities, pos) + 1;
    }
}
