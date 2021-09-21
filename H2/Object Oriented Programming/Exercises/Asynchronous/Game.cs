using System;
using System.Threading.Tasks;
namespace Asynchronous
{
    class Game
    {
        private int MinRange = 0;
        private int MaxRange = 10;
        private int CountDown  = 10;
        private bool GameOver = false;
        private int number = 0;

        public Game() { }

        public Game(int minRange, int maxRange){
            this.MinRange = minRange;
            this.MaxRange = maxRange;
        }

        public void Start(){
            Console.Clear();
            System.Console.WriteLine("WELCOME TO NUMBER GUESSER");
            System.Console.WriteLine("Press ENTER when ready.");
            Console.ReadLine();
            Console.Clear();

            // Generate the random number
            Random rand = new Random();
            this.number = rand.Next(this.MinRange, this.MaxRange);
            System.Threading.Thread.Sleep(1000);
            // Start the countdown task
            CountDownTask();
            Console.SetCursorPosition(3,0);
            System.Console.WriteLine($"Guess a number between {this.MinRange} - {this.MaxRange}");

            while (!GameOver){
                // Handle the input
                int guess = GetInput();

                Console.SetCursorPosition(3, 2);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(3, 2);
                Console.ForegroundColor = ConsoleColor.Red;
                if (number > guess){
                    System.Console.WriteLine($"NUMBER IS GREATHER THAN {guess}");
                } else if (number < guess) {
                    System.Console.WriteLine($"NUMBER IS LESS THAN {guess}");
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine($"YOU ARE CORRECT");
                    break;
                }
                Console.ResetColor();
            }
        }
        private async Task CountDownTask(){
            this.CountDown = 10;
            do {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;
                // Clear the line
                Console.SetCursorPosition(3, 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(3, 1);
                // Print the line and reset the cursor
                System.Console.WriteLine($"You have {this.CountDown} seconds remaining.");
                Console.SetCursorPosition(left,top);
                // Wait 1000 ms and decrement the countdown
                await Task.Delay(1000);
                this.CountDown -= 1;

            } while(this.CountDown > 0);

            GameOverScreen();
        }

        private void GameOverScreen(){
            Console.Clear();
            this.GameOver = true;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("GAMEOVER");
            Console.ResetColor();
            // Exit
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }
        private int GetInput(){
            string input;
            int value;

            while (true){
                Console.SetCursorPosition(3,4);
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write(" $ ");
                Console.ResetColor();

                input = Console.ReadLine();
                if (input.Length > 0){
                    if (int.TryParse(input, out value)){
                        // Clear the last input
                        Console.SetCursorPosition(3,4);
                        System.Console.Write(new string(' ', Console.WindowWidth));

                        return value;
                    }
                    else
                        System.Console.WriteLine("Please input a valid number");
                }
            }
        }
    }
}
