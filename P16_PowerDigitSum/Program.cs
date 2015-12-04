namespace P16_PowerDigitSum
{
    using System;
    using System.Numerics;

    internal class Program
    {
        private static void Main(string[] args)
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
