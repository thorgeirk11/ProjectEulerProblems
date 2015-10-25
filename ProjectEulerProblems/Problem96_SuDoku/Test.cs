namespace NUnit.Tests
{
    using System;
    using NUnit.Framework;
    using Problem96_SuDoku;

    [TestFixture]
    public class SudokuTests
    {
        [Test]
        public void SimplePerfTest()
        {
            var board = new[,] {
                { 0,0,3, 0,2,0, 6,0,0 },
                { 9,0,0, 3,0,5, 0,0,1 },
                { 0,0,1, 8,0,6, 4,0,0 },

                { 0,0,8, 1,0,2, 9,0,0 },
                { 7,0,0, 0,0,0, 0,0,8 },
                { 0,0,6, 7,0,8, 2,0,0 },

                { 0,0,2, 6,0,9, 5,0,0 },
                { 8,0,0, 2,0,3, 0,0,9 },
                { 0,0,5, 0,1,0, 3,0,0 },
            };

            for (int i = 0; i < 1000; i++)
            {
                var x = (int[,])board.Clone();
                SudokuSolver.RunBothSimpleAndAdvance(x, SudokuSolver.InizialzeRoot());
            }
        }
        [Test]
        public void GetPosabilitiesTest()
        {
            for (int i = 0; i < 10000000; i++)
            {
                SudokuSolver.GetPosabilities(Possible.All);
            }
        }
    }
}