using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace P16_PowerDigitSum
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger num = 2;
            for (int i = 1; i < 1000; i++)
            {
                num *= 2;
            }
            var str = num.ToString();
            BigInteger sum = 0;
            foreach (var c in str)
            {
                sum += (int)char.GetNumericValue(c);
            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
