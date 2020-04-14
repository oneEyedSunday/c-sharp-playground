using System;

namespace _101
{
    class Program
    {
        static void Main(string[] args)
        {
            Friend[] friends =
            {
                new Friend("Scott", "Hanselmann"),
                new Friend("Kendra", "Havens")
            };

            Console.WriteLine("C# 101 With: ");

            foreach (var friend in friends)
            {
                Console.WriteLine($"{friend.firstName} {friend.lastName}");
            }

            Console.WriteLine("-----Entering Trims---");

            Program.TrimStringsAndDisplay("    Irreconcilable differences   ");

            Program.ReplaceAndDisplay("Terrier is King", "king", "Queen");

            Program.PartsTest("Ignatius Agu", "gu");

            Console.WriteLine("-------Integer Math-------");

            Integers.Go();
        }


        static void TrimStringsAndDisplay(string value)
        {
            Console.WriteLine($"String as is: {value}");
            Console.WriteLine($"String trimmed to the left: {value.TrimStart()}");
            Console.WriteLine($"String trimmed to the right: {value.TrimEnd()}");
            Console.WriteLine($"String trimmed: {value.Trim()}");
        }

        static void ReplaceAndDisplay(string original, string candidate, string replacement)
        {
            Console.WriteLine($"Initia String is [{original}], replaced string is [{original.ToLower().Replace(candidate, replacement)}]");
        }

        static void PartsTest(string value, string candidate)
        {
            Console.WriteLine($"Does [{value}] contain [{candidate}]: {value.Contains(candidate)}");
        }
    }
}
