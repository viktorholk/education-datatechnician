using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using H3SOAPWebApp.ServiceReference1;
namespace H3SOAPWebApp
{
    public partial class index : System.Web.UI.Page
    {
        // Global variables
        Service1Client client = new Service1Client();
        BookingItem[] bookings;
        int bookingNumber   = 4222;
        int bookingCount    = 12;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create the booking number
            bool created = client.CreateBookingNumber(bookingNumber, bookingCount);
            // Get the bookings
            bookings = client.GetBookingItems(bookingNumber);

            // If the booking number is successfully created we want to map each booking's name to match the ID of the corrosponding ImageButton
            if (created)
            {
                for (int i = 0; i < bookings.Length; i++)
                {
                    // Set the name of the booking to match the imagebutton
                    bookings[i].Name = $"ImageButton{ i + 1 }";
                }
                // Save in the SOAP service
                client.SetBookingItems(bookingNumber, bookings);
            } else
            {
                // The booking number already exists, meaning that we want to update the images to the state of the bookings
                foreach (var booking in bookings)
                {
                    // Get the img matching the booking
                    ImageButton imageButton = (ImageButton)FindControl(booking.Name);

                    if (booking.State == 0)
                    {
                        // Update the image and state to Open
                        imageButton.ImageUrl = "images/seatOpen.png";
                    } else
                    {
                        // Update the image and state to close
                        imageButton.ImageUrl = "images/seatClose.png";
                    }
                }
            }
        }
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;

            // Go through all the bookings and match the booking name to the id of the image button
            // Since we on creation map the image button names to the bookiing
            foreach (var booking in bookings)
            {
                if (booking.Name == imageButton.ID)
                {
                    if (booking.State == 0)
                    {
                        imageButton.ImageUrl = "images/seatClose.png";
                        booking.State = 1;
                    } else
                    {
                        imageButton.ImageUrl = "images/seatOpen.png";
                        booking.State = 0;
                    }
                    break;
                }
            }
            // Save the bookings
            client.SetBookingItems(bookingNumber, bookings);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}