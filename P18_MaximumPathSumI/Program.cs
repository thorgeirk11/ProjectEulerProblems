using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;

namespace P18_MaximumPathSumI
{
    class P18_MaximumPathSum
    {
        public static int GetMax(int[][] tree)
        {
            for (int row = tree.Length - 2; row >= 0; row--)
            {
                for (int i = 0; i < tree[row].Length; i++)
                {
                    tree[row][i] += Math.Max(tree[row + 1][i], tree[row + 1][i + 1]);
                }
            }
            return tree[0][0];
        }
        static void Main(string[] args)
        {
            var max = GetMax(P67_Data.Tree);
            Console.WriteLine(max);
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class MaxPathTests
    {
        [Test]
        public void MaxPathTest()
        {
            var sp = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                var max = P18_MaximumPathSum.GetMax(P67_Data.Tree);
            }
            Console.WriteLine(sp.ElapsedMilliseconds);
        }
    }
}
