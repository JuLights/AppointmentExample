using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentExample
{
    public class DoctorAppointment
    {
        private List<string> _timeSlots;
        private Dictionary<string, string> bookings = new Dictionary<string, string>();
        private object lockObject = new object();

        public DoctorAppointment(List<string> timeSlots)
        {
            _timeSlots = timeSlots;
        }


        public bool BookAppointment(string patientName, string timeSlot)
        {
            lock (lockObject)
            {
                var newTime = TimeHelper.ShiftTimeByOneHour(timeSlot); //for Second Chance for patient shifted by 1 hour,
                                                                       //timeSlot already taken

                if (_timeSlots.Contains(timeSlot) && !bookings.ContainsKey(timeSlot))
                {
                    bookings[timeSlot] = patientName;
                    Console.WriteLine($"{patientName} successfully booked {timeSlot}");
                    return true;
                }
                else if(_timeSlots.Contains(newTime) && !bookings.ContainsKey(newTime))
                {
                    //can do new booking if first user failed to book on prefered time,
                    //i can shift it by 1hrs and book next from timeSlot , shifting function in TimeHelper.cs

                    if (_timeSlots.Contains(newTime) && !bookings.ContainsKey(newTime))
                    {
                        bookings[newTime] = patientName;
                        Console.WriteLine($"{patientName} successfully booked {newTime} -- SECOND CHANCE");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine($"{patientName} failed to book {timeSlot} and also {newTime}, slots already taken. -- UNLUCKY BRO");
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
                    Console.WriteLine($"Time Slot: {booking.Key}, Patient: {booking.Value}");
                }
            }
        }

    }
}
