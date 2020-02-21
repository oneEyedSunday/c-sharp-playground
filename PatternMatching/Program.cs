using System;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Rectangle R = new Rectangle(10, 10);

            var result = R switch
            {
                Rectangle(0, 0) => "The value of length and breadth is zero.",
                Rectangle(10, 5) => "The value of length is 10, breadth is 5.",
                Rectangle(10, 10) => "The value of length and breadth is same –" +
        "this represents a square.",
                _ => "Meeeh."
            };

            Console.WriteLine(result);
        }
    }


    public class Rectangle
    {
        public int Length { get; set; }
        public int Breadth { get; set; }
        public Rectangle(int x, int y) => (Length, Breadth) = (x, y);
        public void Deconstruct(out int x, out int y) => (x, y) = (Length, Breadth);
    }
}
