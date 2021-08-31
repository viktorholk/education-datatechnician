using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Array_of_Strings.classes
{
    public class HomePage
    {
        public string URL { get; set; }
        public string Text { get; set; }

        public Button Button { get; private set; }

        public HomePage(string url, string text, string color = "")
        {
            this.URL = url;
            this.Text = text;
            
            // Create the button
            this.Button = new Button
            {
                Tag     = url,
                Content = text
            };
            // Style and alignment for button
            this.Button.Width = 125;
            Thickness _margin = this.Button.Margin;
            _margin.Top += 5;
            this.Button.Margin = _margin;
            // Set the color
            if (color != "")
            {
                switch (color)
                {
                    case "red":
                        this.Button.Background = Brushes.OrangeRed;
                        break;
                    case "violet":
                        this.Button.Background = Brushes.BlueViolet;
                        break;
                    case "blue":
                        this.Button.Background = Brushes.CadetBlue;
                        break;
                }
            }
        }

        public void ButtonClick(object sender, RoutedEventArgs args, ref TextBox textBox)
        {
            // Update the text box
            string url = Button.Tag.ToString();
            textBox.Text = url;

            // Create a message box if the user wants to open the webpage
            MessageBoxResult result = MessageBox.Show($"Would you like to open this webpage?\n{url}", "Array of Strings", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Create the process of opening the webpage
                ProcessStartInfo process = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                // Start the process
                Process.Start(process);
            }
        }
    }
}
