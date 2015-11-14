using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace P96.SuDoku
{
    public class Program
    {
        public static int TheSum;

        public static void Main(string[] args)
        {
            var boards = ReadBoards(args[0]);

            var time = Stopwatch.StartNew();
            TheSum = 0;
            foreach (var board in boards)
            {
                var solution = P96SudokuSolver.SolveSudoku(board);
                TheSum += solution[0, 0] * 100 + solution[0, 1] * 10 + solution[0, 2];
            }

            Console.WriteLine(time.ElapsedMilliseconds);
            Console.WriteLine(TheSum);
            Console.ReadLine();


            //var board = new[,] {
            //    { 9,1,0, 0,0,2, 0,0,7 },
            //    { 0,0,2, 0,0,0, 0,0,3 },
            //    { 0,0,0, 7,0,0, 0,8,0 },

            //    { 6,0,0, 0,4,0, 3,0,0 },
            //    { 0,0,0, 6,0,1, 0,0,0 },
            //    { 0,0,5, 0,7,0, 0,0,2 },

            //    { 0,4,0, 0,0,3, 0,0,0 },
            //    { 7,0,0, 0,0,0, 1,0,0 },
            //    { 8,0,0, 5,0,0, 0,2,9 },
            //};

            //for (int i = 0; i < 1000; i++)
            //{
            //    var solution = P96SudokuSolver.SolveSudoku(board);
            //}
        }
        public static List<int[,]> ReadBoards(string fileName)
        {
            var boards = new List<int[,]>();
            using (var boardFile = new StreamReader(File.OpenRead(fileName)))
            {
                for (int n = 0; n < 50; n++)
                {
                    boardFile.ReadLine();
                    var board = new int[9, 9];
                    for (int i = 0; i < 9; i++)
                    {
                        var line = boardFile.ReadLine();
                        for (int j = 0; j < 9; j++)
                        {
                            board[i, j] = (int)char.GetNumericValue(line[j]);
                        }
                    }
                    boards.Add(board);
                }
            }
            return boards;
        }
        public static string Print(int[,] board)
        {
            var sb = new StringBuilder();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (col % 3 == 0)
                        sb.Append(" ");
                    sb.Append(board[row, col]);
                }
                if ((row + 1) % 3 == 0)
                    sb.AppendLine();
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
