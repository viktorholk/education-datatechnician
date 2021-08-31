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
using Array_of_Strings.classes;
namespace Array_of_Strings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox textBox;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the buttons
            var data = Jsonhandler.Load<HomePage>("data.json");

            foreach (HomePage homePage in data)
            {
                // Init the event of the button and pass the reference of the textbox
                homePage.Button.Click += (sender, args) => { homePage.ButtonClick(sender, args, ref textBox); };
                StackMain.Children.Add(homePage.Button);
            }

            // Create the text box
            textBox = new TextBox
            {
                Name = "textBox",
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 125
            };
            // Set the margin
            Thickness margin = textBox.Margin;
            margin.Top = 10;
            textBox.Margin = margin;
            // Add to the stack panel
            StackMain.Children.Add(textBox);
        }
    }
}
