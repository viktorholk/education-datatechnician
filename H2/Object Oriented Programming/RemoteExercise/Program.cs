using System;

namespace RemoteExercise
{
    class Button {
        public Action Push;

        public Button(Action del){
            this.Push = del;
        }
    }
    class Remote{
        public Button[] Buttons;

        public Remote(){
            this.Buttons = new Button[]{
                new Button(Button1_press),
                new Button(Button2_press),
                new Button(Button3_press),
                new Button(Button4_press),
                new Button(Button5_press),
                new Button(Button6_press),
                new Button(Button7_press),
                new Button(Button8_press),
                new Button(Button9_press),
                new Button(Button10_press),
                new Button(Button11_press),
                new Button(Button12_press),
                new Button(Button13_press),
                new Button(Button14_press),
                new Button(Button15_press),
            };
        }

        public void PressAllButtons(){
            for (int i = 0; i < this.Buttons.Length; i++)
            {
                System.Console.Write($" #{i} ");
                this.Buttons[i].Push();
            }
        }
        private void Button1_press(){
            System.Console.WriteLine("Switching to channel 1");
        }
        private void Button2_press(){
            System.Console.WriteLine("Switching to channel 2");
        }
        private void Button3_press(){
            System.Console.WriteLine("Switching to channel 3");
        }
        private void Button4_press(){
            System.Console.WriteLine("Switching to channel 4");
        }
        private void Button5_press(){
            System.Console.WriteLine("Switching to channel 5");
        }
        private void Button6_press(){
            System.Console.WriteLine("Switching to channel 6");
        }
        private void Button7_press(){
            System.Console.WriteLine("Switching to channel 7");
        }
        private void Button8_press(){
            System.Console.WriteLine("Switching to channel 8");
        }
        private void Button9_press(){
            System.Console.WriteLine("Switching to channel 9");
        }
        private void Button10_press(){
            System.Console.WriteLine("Switching to HDMI");
        }
        private void Button11_press(){
            System.Console.WriteLine("Opening NetFlix");
        }
        private void Button12_press(){
            System.Console.WriteLine("Opening HBO");
        }
        private void Button13_press(){
            System.Console.WriteLine("Turning volume up");
        }
        private void Button14_press(){
            System.Console.WriteLine("Turning volume down");
        }
        private void Button15_press(){
            System.Console.WriteLine("Turning off TV");
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
            }
        }
    }
}
