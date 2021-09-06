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
using WpfBooking.ServiceReference1;
namespace WpfBooking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly int bookingNumber = 5125;
        BookingItem[] bookings;
        Service1Client client;
        public MainWindow()
        {
            InitializeComponent();

            client = new Service1Client();

            client.CreateBookingNumber(bookingNumber, 12);

            // Get the bookings
            bookings = client.GetBookingItems(bookingNumber);

            // Update the images to the correct state
            UpdateImages();

            PrintAll();
        }
        private void Image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 0;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 1;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 2;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 3;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 4;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 5;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 6;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 7;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 8;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 9;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 10;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }

        private void Image12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            int id = 11;
            bookings[id].Id = id;
            bookings[id].Name = img.Name;
            ChangeBookingStateAndImage(id, img);
        }
        private void ChangeBookingStateAndImage(int id, Image img)
        {
            if (bookings[id].State == 0)
            {
                img.Source = new BitmapImage(new Uri(@"images/SeatClose.png", UriKind.RelativeOrAbsolute));
                bookings[id].State = 1;
            }
            else if (bookings[id].State == 1)
            {
                img.Source = new BitmapImage(new Uri(@"images/SeatOpen.png", UriKind.RelativeOrAbsolute));
                bookings[id].State = 0;
            }
        }
        private void UpdateImages()
        {
            foreach (var booking in bookings)
            {
                // If the booking name is unknown is has not been initialized yet and we know the seat will be open
                if (booking.Name == "unknown") continue;
                Image image = (Image)this.FindName(booking.Name);
                if (booking.State == 0)
                {
                    image.Source = new BitmapImage(new Uri(@"images/SeatOpen.png", UriKind.RelativeOrAbsolute));

                } else if (booking.State == 1)
                {
                    image.Source = new BitmapImage(new Uri(@"images/SeatClose.png", UriKind.RelativeOrAbsolute));
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Update the booking items
            client.SetBookingItems(bookingNumber, bookings);
            PrintAll();
        }
        private void PrintAll()
        {
            foreach (var booking in bookings)
            {
                Console.WriteLine($"{booking.Id}, {booking.Name}, {booking.State}");
            }
            Console.WriteLine();
        }
    }
}
