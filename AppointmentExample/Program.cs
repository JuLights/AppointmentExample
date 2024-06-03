﻿namespace AppointmentExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("/////////////////// DOCTOR APPOINTMENT WITH LOCK ////////////////////");
            Console.WriteLine();
            DoctorAppointment();
            Console.WriteLine();
            Console.WriteLine("/////////////////// DOCTOR APPOINTMENT WITHOUT LOCK ////////////////////");
            Console.WriteLine();
            DoctorAppointmentWithoutLock();
            Console.WriteLine();
            Console.WriteLine("/////////////////// CINEMA TICKET APPOINTMENT WITH LOCK ////////////////////");
            Console.WriteLine();
            CinemaTicketsAppointment();
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