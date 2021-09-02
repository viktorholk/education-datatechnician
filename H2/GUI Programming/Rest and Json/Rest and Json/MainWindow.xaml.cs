using System;
using System.Collections.Generic;
using System.Diagnostics;
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
namespace Rest_and_Json
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RefreshButtons();
        }

        private void RefreshButtons()
        {
            Person[] people = ApiHandler.GetPeople();

            foreach (Person person in people)
            {
                // Check if either the Name or URl is matching
                // Then we will continue since we dont want duplicates
                bool exists = false;
                foreach (FrameworkElement item in StackButtonsPanel.Children)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Person tag = (Person)item.Tag;

                        if (tag.Name == person.Name && tag.URL == person.URL)
                        {
                            //MessageBox.Show($"{person.Name} with the URL {person.URL} already exists!\n skipping.");
                            exists = true;
                        }
                    }
                }
                if (exists) continue;


                Button button = new Button
                {
                    // Add the person to the tag of the button
                    Tag = person,
                    // We add a textblock to the content for formatting
                    Content = new TextBlock(),
                    Margin = new Thickness(10, 10, 10, 10)
                };

                ((TextBlock)button.Content).TextAlignment = TextAlignment.Center;
                ((TextBlock)button.Content).Inlines.Add(person.Name);
                ((TextBlock)button.Content).Inlines.Add("\n");
                ((TextBlock)button.Content).Inlines.Add(person.Age.ToString());
                ((TextBlock)button.Content).Inlines.Add("\n");
                ((TextBlock)button.Content).Inlines.Add(new Run(person.URL) { FontWeight = FontWeights.Bold });
                ((TextBlock)button.Content).Inlines.Add("\n");
                ((TextBlock)button.Content).Inlines.Add(new Run(person.Description) { FontSize = 10 });
                StackButtonsPanel.Children.Add(button);

                // Create the event
                button.Click += (sender, args) => {
                    // Update the textbox
                    UrlTextbox.Text = person.URL;
                };
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshButtons();
        }

        private void OpenInBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the webpage if there is a url in the text box
            if (UrlTextbox.Text.Length > 0)
            {
                ProcessStartInfo process = new ProcessStartInfo()
                {
                    FileName = UrlTextbox.Text,
                    UseShellExecute = true
                };
                Process.Start(process);
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow window = new AddPersonWindow();
            window.Show();
        }
    }
}
