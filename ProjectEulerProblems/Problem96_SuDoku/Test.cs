﻿//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using static Problem96_SuDoku.Possible;

//namespace Problem96_SuDoku
//{
//    [Flags]
//    public enum Possible
//    {
//        One = 1 << 1,
//        Two = 1 << 2,
//        Three = 1 << 3,
//        Four = 1 << 4,
//        Five = 1 << 5,
//        Six = 1 << 6,
//        Seven = 1 << 7,
//        Eight = 1 << 8,
//        Nine = 1 << 9,
//        All = One | Two | Three | Four | Five | Six | Seven | Eight | Nine
//    }
//    public class Program
//    {
//        public static List<Possible> posIndexer = new List<Possible>
//        {
//            One,
//            Two ,
//            Three ,
//            Four ,
//            Five ,
//            Six ,
//            Seven ,
//            Eight ,
//            Nine
//        };

//        public static int TheSum = 0;

//        public static Possible[,] TheSolution { get; private set; }

//        public static void Main(string[] args)
//        {
//            var boards = ReadBoards(args[0]);

//            var time = Stopwatch.StartNew();
//            int counter = 1;
//            foreach (var board in boards)
//            {
//                var root = InizialzeRoot();
//                RunBothSimpleAndAdvance(board, root);

//                if (IsCorrect(root))
//                {
//                    TheSum += board[0, 0] * 100 + board[0, 1] * 10 + board[0, 2];
//                    Console.WriteLine("Board " + counter + " " + TheSum);
//                    Console.WriteLine(Print(board));
//                    Console.WriteLine();
//                    counter++;
//                    continue;
//                }

//                DFS(board, root);


//                Console.WriteLine("Board " + counter + " " + TheSum);
//                Console.WriteLine(Print(board));
//                Console.WriteLine();
//                counter++;

//            }


//            Console.WriteLine(time.ElapsedMilliseconds);
//            Console.WriteLine(TheSum);
//            Console.Read();
//        }

//        private static void DFS(int[,] board, Possible[,] root)
//        {
//            int row, col;
//            FindIndexOfFirstFlaged(root, out row, out col);
//            foreach (var number in GetPosabilities(root[row, col]).ToList())
//            {
//                board[row, col] = GetNumber(number);

//                var newBoard = new int[9, 9];
//                var newRoot = new Possible[9, 9];
//                Array.Copy(board, newBoard, 9 * 9);
//                Array.Copy(root, newRoot, 9 * 9);

//                if (DFS_Checker(newBoard, newRoot))
//                {
//                    TheSum += newBoard[0, 0] * 100 + newBoard[0, 1] * 10 + newBoard[0, 2];
//                    return;
//                }
//                root[row, col] &= ~number;
//            }
//        }

//        public static Possible[,] InizialzeRoot()
//        {
//            var root = new Possible[9, 9];
//            for (int row = 0; row < 9; row++)
//            {
//                for (int col = 0; col < 9; col++)
//                {
//                    root[row, col] = All;
//                }
//            }

//            return root;
//        }

//        public static string Print(Possible[,] root)
//        {
//            var sb = new StringBuilder();
//            for (int row = 0; row < 9; row++)
//            {
//                for (int col = 0; col < 9; col++)
//                {
//                    if (col % 3 == 0)
//                        sb.Append(" ");
//                    sb.Append(GetNumber(root[row, col]));
//                }
//                if ((row + 1) % 3 == 0)
//                    sb.AppendLine();
//                sb.AppendLine();
//            }
//            return sb.ToString();
//        }

//        private static int GetNumber(Possible pos)
//        {
//            return posIndexer.IndexOf(pos) + 1;
//        }

//        public static string Print(int[,] board)
//        {
//            var sb = new StringBuilder();
//            for (int row = 0; row < 9; row++)
//            {
//                for (int col = 0; col < 9; col++)
//                {
//                    if (col % 3 == 0)
//                        sb.Append(" ");
//                    sb.Append(board[row, col]);
//                }
//                if ((row + 1) % 3 == 0)
//                    sb.AppendLine();
//                sb.AppendLine();
//            }
//            return sb.ToString();
//        }


//        public static void FindIndexOfFirstFlaged(Possible[,] root, out int row, out int col)
//        {
//            row = -1;
//            col = -1;

//            var lowestNumber = int.MaxValue;

//            for (int iRow = 0; iRow < 9; iRow++)
//            {
//                for (int iCol = 0; iCol < 9; iCol++)
//                {
//                    if (!OnlyOnePosiblity(root[iRow, iCol]))
//                    {
//                        var count = GetPosabilities(root[iRow, iCol]).Count();

//                        if (lowestNumber > count)
//                        {
//                            row = iRow;
//                            col = iCol;
//                            lowestNumber = count;
//                        }
//                    }
//                }
//            }
//        }

//        private static bool DFS_Search(int[,] board, Possible[,] root, int row, int col)
//        {
//            foreach (var number in GetPosabilities(root[row, col]).ToList())
//            {
//                board[row, col] = GetNumber(number);

//                var isCorrect = DFS_Checker(board, root);
//                if (isCorrect) return true;

//                board[row, col] = 0;
//                root[row, col] &= ~number;
//            }
//            return false; // None of the guesses were correct.
//        }

//        static bool DFS_Checker(int[,] board, Possible[,] root)
//        {
//            var tempBoard = new int[9, 9];
//            var tempRoot = new Possible[9, 9];
//            Array.Copy(board, tempBoard, 9 * 9);
//            Array.Copy(root, tempRoot, 9 * 9);

//            RunBothSimpleAndAdvance(tempBoard, tempRoot);

//            var correct = false;
//            var error = HasError(tempRoot, out correct);
//            if (error)
//            {
//                return false;
//            }
//            if (correct)
//            {
//                return true;
//            }
//            int row, col;
//            FindIndexOfFirstFlaged(tempRoot, out row, out col);

//            return DFS_Search(tempBoard, tempRoot, row, col);
//        }

//        public static bool HasError(Possible[,] arr, out bool IsCorrect)
//        {
//            IsCorrect = true;
//            for (int row = 0; row < 9; row++)
//            {
//                for (int col = 0; col < 9; col++)
//                {
//                    if (arr[row, col] == 0) return true;
//                    if (!OnlyOnePosiblity(arr[row, col]))
//                        IsCorrect = false;
//                }
//            }
//            return false;
//        }

//        public static void RunBothSimpleAndAdvance(int[,] board, Possible[,] posArray)
//        {
//            var changed = false;
//            do
//            {
//                changed = SimpleMethod(board, posArray);
//                if (!changed) return;
//                changed = AdvancedMethod(board, posArray);
//            } while (changed);
//        }

//        private static bool AdvancedMethod(int[,] board, Possible[,] posArray)
//        {
//            var anyChangeAtAll = false;
//            var changed = false;
//            do
//            {
//                changed = false;
//                for (int row = 0; row < 9; row++)
//                {
//                    for (int col = 0; col < 9; col++)
//                    {
//                        var possible = CheckCellWithPos(posArray, row, col);
//                        posArray[row, col] = possible;

//                        if (possible == 0)
//                            return false;

//                        if (board[row, col] == 0 && OnlyOnePosiblity(possible))
//                        {
//                            board[row, col] = GetNumber(possible);
//                            changed = true;
//                            anyChangeAtAll = true;
//                        }
//                    }
//                }
//            } while (changed);
//            return anyChangeAtAll;
//        }

//        private static bool SimpleMethod(int[,] board, Possible[,] posArray)
//        {
//            var anyChangeAtAll = false;
//            var changed = false;
//            do
//            {
//                changed = false;
//                for (int row = 0; row < 9; row++)
//                {
//                    for (int col = 0; col < 9; col++)
//                    {
//                        var possible = CheckCell(board, row, col);
//                        posArray[row, col] = possible;

//                        if (possible == 0)
//                            return false;

//                        if (board[row, col] == 0 && OnlyOnePosiblity(possible))
//                        {
//                            board[row, col] = GetNumber(possible);
//                            changed = true;
//                            anyChangeAtAll = true;
//                        }
//                    }
//                }
//            } while (changed);
//            return anyChangeAtAll;
//        }

//        public static bool IsCorrect(Possible[,] posArray)
//        {
//            var isCorrect = false;
//            return !HasError(posArray, out isCorrect) && isCorrect;
//        }

//        public static Possible CheckCellWithPos(Possible[,] root, int row, int col)
//        {
//            var possible = root[row, col];
//            if (OnlyOnePosiblity(possible)) return possible;
//            foreach (var number in GetPosabilities(possible))
//            {
//                var onlyInRow = true;
//                var onlyInCol = true;
//                for (int i = 0; i < 9; i++)
//                {
//                    if (i != col && root[row, i].HasFlag(number))
//                    {
//                        onlyInRow = false;
//                    }
//                    if (i != row && root[i, col].HasFlag(number))
//                    {
//                        onlyInCol = false;
//                    }
//                    if (!onlyInCol && !onlyInRow) break;
//                }
//                if (onlyInCol || onlyInRow)
//                {
//                    return number;
//                }

//                if (OnlyInBox(root, row, col, number))
//                {
//                    return number;
//                }
//            }
//            return possible;
//        }

//        private static bool OnlyInBox(Possible[,] root, int row, int col, Possible number)
//        {
//            var boxRow = row / 3;
//            var boxCol = col / 3;
//            for (int iCol = boxCol * 3; iCol < boxCol * 3 + 3; iCol++)
//            {
//                for (int iRow = boxRow * 3; iRow < boxRow * 3 + 3; iRow++)
//                {
//                    if (iCol == col && iRow == row) continue;

//                    var value = root[iRow, iCol];
//                    if (value.HasFlag(number))
//                    {
//                        return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public static bool OnlyOnePosiblity(Possible possible)
//        {
//            return (possible & (possible - 1)) == 0;
//        }

//        public static IEnumerable<Possible> GetPosabilities(Possible input)
//        {
//            foreach (Possible value in posIndexer)
//                if (input.HasFlag(value))
//                    yield return value;
//        }

//        public static Possible CheckCell(int[,] board, int row, int col)
//        {
//            if (board[row, col] != 0) return (Possible)(1 << board[row, col]);

//            var possible = All;

//            for (int i = 0; i < 9; i++)
//            {
//                if (board[row, i] != 0)
//                {
//                    var mask = ~(1 << board[row, i]);
//                    possible &= (Possible)mask;
//                }
//                if (board[i, col] != 0)
//                {
//                    var val = board[i, col];
//                    var mask = ~(1 << val);
//                    possible &= (Possible)mask;
//                }
//            }
//            var boxRow = row / 3;
//            var boxCol = col / 3;
//            for (int iCol = boxCol * 3; iCol < boxCol * 3 + 3; iCol++)
//            {
//                for (int iRow = boxRow * 3; iRow < boxRow * 3 + 3; iRow++)
//                {
//                    if (board[iRow, iCol] != 0)
//                    {
//                        var value = board[iRow, iCol];
//                        var mask = ~(1 << value);
//                        possible &= (Possible)mask;
//                    }
//                }
//            }
//            return possible;
//        }

//        public static List<int[,]> ReadBoards(string fileName)
//        {
//            var boards = new List<int[,]>();
//            using (var boardFile = new StreamReader(File.OpenRead(fileName)))
//            {
//                for (int n = 0; n < 50; n++)
//                {
//                    boardFile.ReadLine();
//                    var board = new int[9, 9];
//                    for (int i = 0; i < 9; i++)
//                    {
//                        var line = boardFile.ReadLine();
//                        for (int j = 0; j < 9; j++)
//                        {
//                            board[i, j] = (int)char.GetNumericValue(line[j]);
//                        }
//                    }
//                    boards.Add(board);
//                }
//            }
//            return boards;
//        }
//    }
//}
