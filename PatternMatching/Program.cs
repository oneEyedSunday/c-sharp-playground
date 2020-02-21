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
                Rectangle(10, 10) => "The value of length and breadth is same – this represents a square.",
                _ => "Meeeh."
            };

            Console.WriteLine(result);

            Voyager voyager = new Voyager()
            {
                CountryOrigin = "Nigeria",
                Mileage = 45,
                Oriki = "Ologo 5G",
                Destination = "Canada"
            
            };

            bool grantEntry = ShouldAllowEntry(voyager);
            Console.WriteLine("{0}, you have been {1} visa to {2}", voyager.Oriki, grantEntry ? "granted" : "denied", voyager.Destination);
        }

        public static bool ShouldAllowEntry(Voyager hobo) =>
        hobo switch
        {
            { CountryOrigin: "Nigeria", Destination: "Nigeria" } => true,
            { CountryOrigin: "Nigeria" } => false,
            _ => false
        };
    }


    public class Rectangle
    {
        public int Length { get; set; }
        public int Breadth { get; set; }
        public Rectangle(int x, int y) => (Length, Breadth) = (x, y);
        public void Deconstruct(out int x, out int y) => (x, y) = (Length, Breadth);
    }

    public class Voyager
    {
        public string Oriki { get; set; }
        public decimal Mileage { get; set; }
        public string CountryOrigin { get; set; }
        public string Destination { get; set; }
    }
}
