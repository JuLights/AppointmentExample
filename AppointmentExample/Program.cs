using System.Text;
using System.Threading;
using AppointmentExample.Auction;
using AppointmentExample.CinemaMultipleThSync;
using AppointmentExample.CinemaSingleThSync;
using AppointmentExample.DoctorSingleThSync;

namespace AppointmentExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DoctorAppointment();  DOCTOR APPOINTMENT WITHOUT LOCK
            //Console.WriteLine();
            //DoctorAppointmentWithoutLock(); CINEMA TICKET APPOINTMENT WITHOUT LOCK
            //Console.WriteLine();
            //CinemaTicketsAppointment(); CINEMA TICKET APPOINTMENT WITH LOCK
            //CinemaMultipleThSync(); CINEMA TICKET APPOINTMENT WITH SemaphoreSlim
            //Console.WriteLine();


            var car = new Car();
            var auction = new Auction.Auction(car, (decimal)100.00, TimeSpan.FromSeconds(10));
            AuctionTester(15, (decimal)150.00, (decimal)500.00, auction);

            Console.ReadLine();
        }

        private static void AuctionTester(int numberOfConcurrentThs,decimal increaseConstant, decimal startPrice, Auction.Auction auction)
        {
            Thread[] threads = new Thread[numberOfConcurrentThs];
            auction.StartAuction();
            Thread.Sleep(2000);
            var rnd = new Random();
            for (int i = 0; i < numberOfConcurrentThs; i++)
            {
                threads[i] = new Thread(() => auction.AddOffer($"User{i}", increaseConstant));
                Thread.Sleep(rnd.Next(1,15)*500);
                threads[i].Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

        }



        //Cinema ticket appointment with SemaphoreSlim
        private static void CinemaMultipleThSync()
        {
            var semaphore = new SemaphoreSlim(3, 7); //2 user at a time
            var cinemaTickets = new CinemaTicketAppointment(semaphore);

            int count = 1;

            Thread[] threads = new Thread[7]; // 5 users trying to book tickets
            threads[0] = new Thread(() => cinemaTickets.BookTicket());
            threads[1] = new Thread(() => cinemaTickets.BookTicket());
            threads[2] = new Thread(() => cinemaTickets.BookTicket());
            threads[3] = new Thread(() => cinemaTickets.BookTicket());
            threads[4] = new Thread(() => cinemaTickets.BookTicket());
            threads[5] = new Thread(() => cinemaTickets.BookTicket());
            threads[6] = new Thread(() => cinemaTickets.BookTicket());

            foreach (var thread in threads)
            {
                thread.Name = $"User{count}";
                count++;
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

        }

        //Doctor Appointment with lock //CORRECT WAY
        private static void DoctorAppointment()
        {
            DoctorAppointment appointmentSystem = new DoctorAppointment(new List<string> { "9:00 AM", "10:00 AM", "11:00 AM" });

            Thread[] threads = new Thread[7];
            threads[0] = new Thread(() => appointmentSystem.BookAppointment("Alice", "9:00 AM"));
            threads[1] = new Thread(() => appointmentSystem.BookAppointment("Bob", "9:00 AM"));
            threads[2] = new Thread(() => appointmentSystem.BookAppointment("Charlie", "9:00 AM"));
            threads[3] = new Thread(() => appointmentSystem.BookAppointment("Nick", "10:00 AM"));
            threads[4] = new Thread(() => appointmentSystem.BookAppointment("David", "10:00 AM"));
            threads[5] = new Thread(() => appointmentSystem.BookAppointment("Eve", "11:00 AM"));
            threads[6] = new Thread(() => appointmentSystem.BookAppointment("Frank", "11:00 AM"));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            appointmentSystem.DisplayBookings();
        }
        //Doctor Appointment with lock //BAD WAY
        private static void DoctorAppointmentWithoutLock()
        {
            DoctorAppointmentWithoutLock appointmentSystem = new DoctorAppointmentWithoutLock(new List<string> { "9:00 AM", "10:00 AM", "11:00 AM" });

            Thread[] threads = new Thread[7];
            threads[0] = new Thread(() => appointmentSystem.BookAppointment("Alice", "9:00 AM"));
            threads[1] = new Thread(() => appointmentSystem.BookAppointment("Bob", "9:00 AM"));
            threads[2] = new Thread(() => appointmentSystem.BookAppointment("Charlie", "9:00 AM"));
            threads[3] = new Thread(() => appointmentSystem.BookAppointment("Nick", "10:00 AM"));
            threads[4] = new Thread(() => appointmentSystem.BookAppointment("David", "10:00 AM"));
            threads[5] = new Thread(() => appointmentSystem.BookAppointment("Eve", "11:00 AM"));
            threads[6] = new Thread(() => appointmentSystem.BookAppointment("Frank", "11:00 AM"));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            appointmentSystem.DisplayBookings();
        }
        //Cinema Appointment with lock // CORRECT WAY
        private static void CinemaTicketsAppointment()
        {
            CinemaTickets cinema = new CinemaTickets(new List<string> { "A1", "A2", "A3", "B1", "B2", "B3" });

            Thread[] threads = new Thread[6];
            threads[0] = new Thread(() => cinema.BookTicket("Alice", "A1"));
            threads[1] = new Thread(() => cinema.BookTicket("Bob", "A1"));
            threads[2] = new Thread(() => cinema.BookTicket("Charlie", "A2"));
            threads[3] = new Thread(() => cinema.BookTicket("David", "A2"));
            threads[4] = new Thread(() => cinema.BookTicket("Eve", "B1"));
            threads[5] = new Thread(() => cinema.BookTicket("Frank", "B1"));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            cinema.DisplayBookings();

        }
        


    }
}
