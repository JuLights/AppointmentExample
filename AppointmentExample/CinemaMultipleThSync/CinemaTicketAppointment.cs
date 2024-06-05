using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppointmentExample.CinemaMultipleThSync
{
    public class CinemaTicketAppointment
    {
        private List<string> _seats;
        private static SemaphoreSlim _semaphore;
        private static Semaphore _sem;
        private Dictionary<string, string> bookings = new Dictionary<string, string>();
        public CinemaTicketAppointment(SemaphoreSlim semaphore)
        {
            //_seats = seats;
            _semaphore = semaphore;
        }

        public void BookTicket()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " is waiting to book a ticket.");
            _semaphore.Wait(); // Acquire semaphore

            Console.WriteLine(Thread.CurrentThread.Name + " is booking a ticket...");
            Thread.Sleep(2000); // Simulate ticket booking process

            Console.WriteLine(Thread.CurrentThread.Name + " booked a ticket.");

            _semaphore.Release(); // Release semaphore
        }
    }
}
