using System;

namespace Remote
{
    class Button {
        public Action Push;

        public Button(Action del){
            this.Push = del;
        }
    }
    class Remote{
        public Button[] Buttons;

        public bool TvStarted = false;

        public Remote(){
            this.Buttons = new Button[]{
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 1");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 2");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 3");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 4");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 5");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 6");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 7");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 8");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching to channel 9");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Switching input");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Opening NetFlix");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Opening Settings");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Increase volume");
                }),
                new Button(delegate () {
                    System.Console.WriteLine("Decrease volume");
                }),
                new Button(delegate () {
                    TvStarted = !TvStarted;
                    System.Console.WriteLine( TvStarted ? "Turning TV off" : "Turning TV on" );
                }),
            };
        }

        public void PressAllButtons(){
            for (int i = 0; i < this.Buttons.Length; i++)
            {
                System.Console.Write($" #{i} ");
                this.Buttons[i].Push();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Remote remote = new Remote();

            remote.PressAllButtons();

            while (true){
                System.Console.WriteLine();
                System.Console.WriteLine($"Press a button between 0 - {remote.Buttons.Length - 1}");
                System.Console.Write("  $ ");
                int value;
                int selection = int.TryParse(Console.ReadLine(), out value) ? value : -1;
                if (selection <= remote.Buttons.Length - 1 && selection >= 0)
                    remote.Buttons[selection].Push();
                else System.Console.WriteLine("Not a valid button!");
            }
        }
    }
}
