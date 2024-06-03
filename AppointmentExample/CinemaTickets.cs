using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentExample
{
    public class CinemaTickets
    {
        private List<string> _seats;
        private Dictionary<string, string> bookings = new Dictionary<string, string>();
        private object lockObject = new object();

        public CinemaTickets(List<string> seats)
        {
            _seats = seats;
        }

        public bool BookTicket(string customerName, string seat)
        {
            lock (lockObject)
            {
                if (_seats.Contains(seat) && !bookings.ContainsKey(seat))
                {
                    bookings[seat] = customerName;
                    Console.WriteLine($"{customerName} successfully booked seat {seat}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{customerName} failed to book seat {seat}, seat already taken.");
                    return false;
                }
            }
        }

        public void DisplayBookings()
        {
            lock (lockObject)
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Seat: {booking.Key}, Customer: {booking.Value}");
                }
            }
        }

    }
}
