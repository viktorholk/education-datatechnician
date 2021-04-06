using System;

namespace Calculator_Documentation
{

    /// <summary>
    /// The Calculator class that is the pinpoint of the project
    /// In here we have the mathematically logical methods such as Add, Subtract, Multiply and division
    /// There is also a method called GetNumbers that returns the (int)numbers that are used in the logical methods
    /// </summary>
    public class Calculator
    {
        /// <summary>
        ///     Takes two arguments and takes the sum of it by adding them together
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>Arguments added together</returns>
        public float Add(float num1, float num2)
        {
            return num1 + num2;
        }

        /// <summary>
        /// Takes two arguments and subtracts them
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>Arguments subtracted</returns>
        public float Subtract(float num1, float num2)
        {
            return num1 - num2;
        }
        /// <summary>
        /// Takes two arguments and multiplies them together
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>Arguments multiplied</returns>
        public float Multiply(float num1, float num2)
        {
            return num1 * num2;
        }
        /// <summary>
        /// Takes two arguments and divides them
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>Argumentes divided</returns>
        public float Division(float num1, float num2)
        {
            return num1 / num2;
        }

        /// <summary>
        /// This method returns a tuple with the two valid numbers that the user has inputtet
        /// First we read it as a string for the two numbers and then make a try catch were we will check if it can be converted to integers
        /// and if it can, we will return the two numbers in a tuple
        /// </summary>
        /// <returns></returns>
        public Tuple<int,int> GetNumbers()
        {
            int num1;
            int num2;

            while (true)
            {
                Console.Write("Number 1: ");
                string _num1 = Console.ReadLine();

                Console.Write("Number 2: ");
                string _num2 = Console.ReadLine();
                // Check if the numbers can be converted to integers
                try
                {
                    num1 = Convert.ToInt32(_num1);
                    num2 = Convert.ToInt32(_num2);
                    // Break out of the loop so we can return the tuples
                    break;
                } catch
                {
                    Console.WriteLine("Not valid number(s), try again");
                }
            }
            // Return the tuples
            return Tuple.Create(num1, num2);

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Calculator object
            Calculator calculator = new Calculator();
            // Print the menu óptions
            Console.WriteLine("Console Calculator");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");
            while (true)
            {
                // Take user input for the selection of the menu
                // Only allow integers so we have a while loop to make sure it only breaks when it is a valid number
                // and if it isn't we write to the user its a invalid number
                int menuSelection;
                while (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("You entered an invalid number");
                }
                // Go through with case we are going to handle from the menuselection
                switch (menuSelection)
                {

                    case 1:
                        // Get the two numbers
                        Tuple<int, int> addNumbers = calculator.GetNumbers();
                        // Write the output of the Add method
                        Console.WriteLine(calculator.Add(addNumbers.Item1, addNumbers.Item2));
                        //Empty line for easier to read
                        Console.WriteLine();

                        break;
                    case 2:
                        // Get the two numbers
                        Tuple<int, int> subtractNumbers = calculator.GetNumbers();
                        // Write the output of the Subtract method
                        Console.WriteLine(calculator.Subtract(subtractNumbers.Item1, subtractNumbers.Item2));
                        //Empty line for easier to read
                        Console.WriteLine();

                        break;
                    case 3:
                        // Get the two numbers
                        Tuple<int, int> multiplyNumbers = calculator.GetNumbers();
                        // Write the output of the Multiply method
                        Console.WriteLine(calculator.Multiply(multiplyNumbers.Item1, multiplyNumbers.Item2));
                        //Empty line for easier to read
                        Console.WriteLine();

                        break;
                    case 4:
                        // Get the two numbers
                        Tuple<int, int> divideNumbers = calculator.GetNumbers();
                        // Write the output of the Division method
                        Console.WriteLine(calculator.Division(divideNumbers.Item1, divideNumbers.Item2));
                        //Empty line for easier to read
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
