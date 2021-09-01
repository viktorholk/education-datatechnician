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
using System.Windows.Shapes;
using SOAP.ServiceReference1;
namespace SOAP
{
    /// <summary>
    /// Interaction logic for AddWebpageWindow.xaml
    /// </summary>
    public partial class AddWebpageWindow : Window
    {
        public AddWebpageWindow()
        {
            InitializeComponent();
        }

        private void AddWebpageButton_Click(object sender, RoutedEventArgs e)
        {
            // Add new homepage
            if (NameTextBox.Text != "" && UrlTextBox.Text != "")
            {
                // Description can be empty

                // Create the Hjemmeside object
                Hjemmeside webPage = new Hjemmeside
                {
                    Navn = NameTextBox.Text,
                    Url = UrlTextBox.Text,
                    Beskrivelse = DescriptionTextBox.Text
                };

                // add the new hjemmeside to the SOAP service
                Service1Client client = new Service1Client();
                client.addHjemmesider(webPage);
                // Add to the stackpanel
                // Close the window
                this.Close();
            }
        }
    }
}
