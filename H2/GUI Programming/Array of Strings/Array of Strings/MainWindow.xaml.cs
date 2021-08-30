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
        TextBox webpageTextBox;

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
                    Name = $"Button{item.Text.Replace(" ", "")}"
                };
                button.Click += (sender, EventArgs) => { ButtonClick    (sender, EventArgs, item.URL); };

                // Assign a color to the button dependent on the webpage country
                if (item.CountryCode == "DK")
                {
                    button.Background = Brushes.OrangeRed;
                } else if (item.CountryCode == "US")
                {
                    button.Background = Brushes.BlueViolet;
                }
                // Position and alignment
                button.Width = 125;
                Thickness btnMargin = button.Margin;
                btnMargin.Top = 5;
                button.Margin = btnMargin;
                StackMain.Children.Add(button);
            }
            // Create the text box
            webpageTextBox = new TextBox();
            webpageTextBox.Name = "textBox";
            webpageTextBox.TextAlignment = TextAlignment.Center;
            webpageTextBox.HorizontalAlignment = HorizontalAlignment.Center;
            webpageTextBox.Width = 125;
            // Set the margin
            Thickness margin = webpageTextBox.Margin;
            margin.Top = 10;
            webpageTextBox.Margin = margin;
            // Add to the stack panel
            StackMain.Children.Add(webpageTextBox);

        }

        public void ButtonClick(object sender, RoutedEventArgs args, string url)
        {
            // Update the text box
            webpageTextBox.Text = url;

            // Create a message box if the user wants to open the webpage
            MessageBoxResult result = MessageBox.Show($"Would you like to open the webpage?\n{url}", "Array of Strings" ,MessageBoxButton.YesNo);
            
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // Create the process of opening the webpage
                    ProcessStartInfo process = new ProcessStartInfo();
                    process.FileName = url;
                    process.UseShellExecute = true;
                    // Start the process
                    Process.Start(process);
                    break;
            }
        }
    }
}
