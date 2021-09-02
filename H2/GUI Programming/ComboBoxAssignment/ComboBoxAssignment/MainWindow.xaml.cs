using System;
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
using ComboBoxAssignment.ServiceReference1;
namespace ComboBoxAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Webpage : Hjemmeside
        {
            public Webpage(Hjemmeside hjemmeside)
            {
                this.Navn = hjemmeside.Navn;
                this.Url = hjemmeside.Url;
                this.Beskrivelse = hjemmeside.Beskrivelse;
            }

            public override string ToString()
            {
                return Navn;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Service1Client client = new Service1Client();

            Hjemmeside[] hjemmesider = client.getHjemmesider();

            // Add default value
            WebpagesComboBox.Items.Add("Select a webpage");
            WebpagesComboBox.SelectedItem = WebpagesComboBox.Items[0];

            foreach (var hjemmeside in hjemmesider)
            {
                // Since we are adding a object to the combobox we will be needing to override the ToString() method
                // because the text of the combobox will be the ToString() of the object
                Webpage webpage = new Webpage(hjemmeside);

                WebpagesComboBox.Items.Add(webpage);
            }

            WebpagesComboBox.SelectionChanged += (sender, SelectionChangedEventArgs) => {
                ComboBox comboBox = (ComboBox)sender;
                // get the selected item
                if (comboBox.SelectedItem.GetType() == typeof(Webpage))
                {
                    Webpage selectedWebpage = (Webpage)comboBox.SelectedItem;

                    // Update the label
                    WebpageUrlLabel.Content = selectedWebpage.Url;
                    // Update the TextBlock and clear the last TextBlock
                    WebpageInfoTextBlock.Inlines.Clear();
                    WebpageInfoTextBlock.Inlines.Add(new Run(selectedWebpage.Navn) { FontWeight = FontWeights.Bold });
                    WebpageInfoTextBlock.Inlines.Add("\n");
                    WebpageInfoTextBlock.Inlines.Add(selectedWebpage.Url);
                    WebpageInfoTextBlock.Inlines.Add("\n");
                    WebpageInfoTextBlock.Inlines.Add(new Run(selectedWebpage.Beskrivelse) { FontSize = 10 });
                }
            };
        }
    }
}
