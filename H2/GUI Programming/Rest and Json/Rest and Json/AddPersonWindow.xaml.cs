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

namespace Rest_and_Json
{
    /// <summary>
    /// Interaction logic for AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the person object from the textboxes
            Person person = new Person();
            person.Name = NameTextbox.Text;
            person.Age = AgeTextbox.Text;
            person.URL = UrlTextbox.Text;
            person.Description = descriptionTextbox.Text;
            person.Active = ActiveCheckBox.IsEnabled;

            // Post the data
            ApiHandler.Addperson(person);
            this.Close();

        }
    }
}
