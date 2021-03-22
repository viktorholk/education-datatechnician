using System;
namespace ClassExercises
{

    class Point2D
    {
        double x, y;

        public Point2D(double _x, double _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public string GetPoints()
        {
            return $"( {this.x}, {this.y} )";
        }

        public double GetDistance()
        {
            return Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            Point2D point = new Point2D(5.0, 10.0);
            Console.Write($"The distance between {point.GetPoints()} = ");
            Console.Write(point.GetDistance());
            Console.WriteLine();
        }
    }
}
