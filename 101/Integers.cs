using System;
namespace _101
{
    public class Integers
    {
        public Integers()
        {
        }

        public static void Go()
        {
            Console.WriteLine(3.0 * 5 + 300);
            Console.WriteLine($"Man int value: , Should be 2^32-1 - 1 [{int.MaxValue}]");
            Console.WriteLine($"Min int value: , Should be -2^32-1 [{int.MinValue}]");
        }
    }
}
