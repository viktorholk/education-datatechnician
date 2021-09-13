using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookingAssignment4.ServiceReference1;
using Newtonsoft.Json;
namespace BookingAssignment4
{
    public partial class AjaxReceiver : System.Web.UI.Page
    {
        static Service1Client client = new Service1Client();
        static BookingItem[] bookings;
        static readonly int bookingNumber = 919114;
        static readonly int bookingCount = 100;

        private static BookingItem[] GetBookingItems()
        {
            // Create the booking number
            bool created = client.CreateBookingNumber(bookingNumber, bookingCount);

            // Get the bookings
            bookings = client.GetBookingItems(bookingNumber);

            // We need to add ids to the bookings so we can handle them on the client and server side
            if (created)
            {
                for (int i = 0; i < bookings.Length; i++)
                {
                    // Set the id booking
                    bookings[i].Id = i;
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
            return JsonConvert.SerializeObject(GetBookingItems());
        }

        [WebMethod]
        public static string SaveBooking(int id)
        {
            // Find the booking from the id
            var bookings = GetBookingItems();

            foreach (var booking in bookings)
            {
                if (booking.Id == id)
                {
                    if (booking.State == 0)
                        booking.State = 1;
                    else if (booking.State == 1)
                        booking.State = 0;

                    // Save the booking
                    client.SetBookingItems(bookingNumber, bookings);
                    return JsonConvert.SerializeObject(booking);
                }
            }
            return "{}";
        }
    }
}