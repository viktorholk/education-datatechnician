using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using SOAP.ServiceReference1;

namespace SOAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Hjemmeside> Hjemmesider;
        public MainWindow()
        {
            InitializeComponent();
            // Hide script errors from webpageBrowser
            HideScriptErrors(webpageBrowser, true);

            // create the hjemmesider instance
            Hjemmesider = new List<Hjemmeside>();

            // Add the buttons from SOAP
            RefreshButtons();

        }

        private void RefreshButtons()
        {
            Service1Client SOAP = new Service1Client();

            Hjemmeside[] hjemmesider = SOAP.getHjemmesider();
            foreach (var hjemmeside in hjemmesider)
            {
                // Make sure we only add buttons that doesn't already exists
                if (!Hjemmesider.Any(i => i.Url == hjemmeside.Url))
                {
                    // Create the button
                    Button button = new Button
                    {
                        Tag = hjemmeside.Url,
                        Content = hjemmeside.Navn
                    };
                    // Style and alignment for button
                    button.Width = 125;
                    Thickness _margin = button.Margin;
                    _margin.Top += 5;
                    button.Margin = _margin;

                    // Set the event
                    button.Click += (sender, args) => {
                        string url = button.Tag.ToString();
                        webpageTextBox.Text = url;
                        webpageBrowser.Source = new Uri($"https://{url}");
                    };

                    // Add to stack panel
                    ScrollStack.Children.Add(button);
                    // Add to Hjemmesider list so we wont be adding the same button more than once
                    Hjemmesider.Add(hjemmeside);

                }
            }
        }
        private void AddWebPage_Click(object sender, RoutedEventArgs e)
        {
            AddWebpageWindow addWebpageWindow = new AddWebpageWindow();
            addWebpageWindow.Show();

        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshButtons();
        }

        public void HideScriptErrors(WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null)
            {
                wb.Loaded += (o, s) => HideScriptErrors(wb, hide); //In case we are to early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }
    }
}
