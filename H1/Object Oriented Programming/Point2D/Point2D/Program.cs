using System;

namespace Point2D
{

    class Point2D
    {
        double x, y;

        public Point2D(double _x, double _y)
        {
            this.x = _x;
            this.y = _y;
        }
        public double Distance(Point2D p2)
        {
            
            return Math.Sqrt(Math.Pow(p2.x - this.x, 2) + Math.Pow(p2.y - this.y,2));
        }

        public override string ToString()
        {
            return $"( {this.x}, {this.y} )";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Point2D point1 = new Point2D(5, 10);
            Point2D point2 = new Point2D(8, 12);
            Console.Write($"The distance between {point1.ToString()} and {point2.ToString()}  = ");
            Console.Write(point1.Distance(point2));
            Console.WriteLine();
        }
    }
}
