using NUnit.Framework;
using P96.SuDoku;

[TestFixture]
public class SudokuTests
{
    [Test]
    public void SimplePerfTest()
    {
        var board = new[,] {
            { 9,1,0, 0,0,2, 0,0,7 },
            { 0,0,2, 0,0,0, 0,0,3 },
            { 0,0,0, 7,0,0, 0,8,0 },

            { 6,0,0, 0,4,0, 3,0,0 },
            { 0,0,0, 6,0,1, 0,0,0 },
            { 0,0,5, 0,7,0, 0,0,2 },

            { 0,4,0, 0,0,3, 0,0,0 },
            { 7,0,0, 0,0,0, 1,0,0 },
            { 8,0,0, 5,0,0, 0,2,9 },
        };

        for (int i = 0; i < 1000; i++)
        {
            var solution = P96SudokuSolver.SolveSudoku(board);
            //Assert.IsTrue(SudokuSolver.IsCorrect(solution))
        }
    }
}