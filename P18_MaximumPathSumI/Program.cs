using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P18_MaximumPathSumI
{
    class Program
    {
        static int[] tree = new[]
        {
                    75,
                95,      64,
            17,     47,     82,
        18,     35,     87,     10,
     20,    04,     82,     47,     65,

            19, 01, 23, 75, 03, 34,
            88, 02, 77, 73, 07, 63, 67, 99, 65, 04, 28, 06, 16, 70, 92, 41, 41, 26, 56, 83, 40,
            80, 70, 33, 41, 48, 72, 33, 47, 32, 37, 16, 94, 29, 53, 71, 44, 65, 25, 43, 91, 52,
            97, 51, 14, 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57, 91, 71, 52, 38, 17, 14,
            91, 43, 58, 50, 27, 29, 48, 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31,
            04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23
        };


        static void Main(string[] args)
        {
            var max = 0;
            for (int i = 1; i < tree.Length; i++)
            {
                var depth = GetDepth(i);
                var Left = GetValue(i, depth);
                var Right = GetValue(i + 1, depth);
                tree[i] += Math.Max(Left, Right);
                max = Math.Max(max, tree[i]);
            }

            Console.ReadLine();
        }


        static int GetValue(int index, int depth)
        {
            var nextValueDepth = GetDepth(index - depth);
            if (depth == nextValueDepth)
                return 0;
            if (depth - nextValueDepth == 2)
                return 0;
            if (index - depth < 0)
                return 0;
            return tree[index - depth];
        }

        static int GetDepth(int index)
        {
            for (int i = 0; true; i++)
            {
                index -= i;
                if (index < 0)
                    return i;
            }
        }

    }
}
