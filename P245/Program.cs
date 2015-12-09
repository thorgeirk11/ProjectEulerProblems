using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace P245
{
    class Program
    {
        static bool IsResiliant(int x, int y)
        {
            while (true)
            {
                var t = x % y;
                if (t == 0) return y == 1;
                x = y;
                y = t;
            }
        }

        static double ResilianceRatio(int d)
        {
            var _3 = d % 3 == 0;
            var _5 = d % 5 == 0;
            var _7 = d % 7 == 0;

            var resiliant = 0;
            Parallel.For(1, d, i =>
            {
                if (i % 2 == 0) return;
                if (_3 && i % 3 == 0) return;
                if (_5 && i % 5 == 0) return;
                if (_7 && i % 7 == 0) return;

                if (IsResiliant(d, i))
                {
                    Interlocked.Increment(ref resiliant);
                }
            });
            return resiliant / (double)(d - 1);
        }

        static void Main(string[] args)
        {
            //var sw = Stopwatch.StartNew();
            //ResilianceRatio(892371480);
            //var sec = sw.ElapsedMilliseconds;
            //Console.WriteLine(sec);
            //Console.ReadLine();
            var min = 1.0;
            //for (int i = 10; true; i += 10)
            for (int i = 9699690; true; i += 9699690)
            {
                var ratio = ResilianceRatio(i);
                //if (ratio < 0.2)
                if (ratio < min)
                {
                    //Console.WriteLine(i);
                    //break;
                    min = ratio;
                    Console.WriteLine(i + ": " + ratio);
                }
            }
        }
    }
}
