using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookingAssignment3.ServiceReference1;
using Newtonsoft.Json;
namespace BookingAssignment3
{
    public partial class AjaxReceiver : System.Web.UI.Page
    {
        static Service1Client client = new Service1Client();
        static BookingItem[] bookings;
        static readonly int bookingNumber  = 9191;
        static readonly int bookingCount   = 9;


        private static BookingItem[] GetBookings()
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
                    bookings[i].Name = $"Button{ i + 1 }";
                }
                // Save in the SOAP service
                client.SetBookingItems(bookingNumber, bookings);
            }

            return bookings;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string SerializeBookings()
        {
            // Get the bookings and serialize them to JSON
            return JsonConvert.SerializeObject(GetBookings());
        }

        [WebMethod]
        public static string SetBooking(string name)
        {
            // Get the booking that matches the name
            var bookings = GetBookings();
            foreach (var booking in bookings)
            {
                if (booking.Name == name) {
                    // Update the state
                    if (booking.State == 0)
                        booking.State = 1;
                    else booking.State = 0;
                    // Save
                    client.SetBookingItems(bookingNumber, bookings);

                    // return the State
                    return booking.State.ToString();
                }
            }
            return "";
        }

    }
}