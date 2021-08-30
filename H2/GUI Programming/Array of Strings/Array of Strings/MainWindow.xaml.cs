using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Array_of_Strings.helpers;

namespace Array_of_Strings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox WebpageTextBox;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the buttons
            Jsonhandler.Item[] data = Jsonhandler.Load("data.json");

            foreach (Jsonhandler.Item item in data)
            {
                Button button = new Button
                {
                    Content = item.Text,
                    Name = $"Button{item.Text.Replace(" ", "")}",
                    // Store the URL in the tag of the button so we can refer to it later
                    Tag  = item.URL
                };
                // Create the event as a lambda function
                button.Click += (sender, RoutedEventArgs) => {
                    string url = button.Tag.ToString();
                    // Update the text box
                    WebpageTextBox.Text = url;

                    // Create a message box if the user wants to open the webpage
                    MessageBoxResult result = MessageBox.Show($"Would you like to open this webpage?\n{url}", "Array of Strings", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Create the process of opening the webpage
                        ProcessStartInfo process = new ProcessStartInfo();
                        process.FileName = url;
                        process.UseShellExecute = true;
                        // Start the process
                        Process.Start(process);
                    }
                };

                // Assign a color to the button dependent on the webpage country
                if (item.CountryCode == "DK")
                {
                    button.Background = Brushes.OrangeRed;
                } else if (item.CountryCode == "US")
                {
                    button.Background = Brushes.BlueViolet;
                }
                // Position, style and alignment.
                button.Width = 125;
                Thickness btnMargin = button.Margin;
                btnMargin.Top = 5;
                button.Margin = btnMargin;
                StackMain.Children.Add(button);
            }
            // Create the text box
            WebpageTextBox = new TextBox();
            WebpageTextBox.Name = "textBox";
            WebpageTextBox.TextAlignment = TextAlignment.Center;
            WebpageTextBox.HorizontalAlignment = HorizontalAlignment.Center;
            WebpageTextBox.Width = 125;
            // Set the margin
            Thickness margin = WebpageTextBox.Margin;
            margin.Top = 10;
            WebpageTextBox.Margin = margin;
            // Add to the stack panel
            StackMain.Children.Add(WebpageTextBox);
        }
    }
}
