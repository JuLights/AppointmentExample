using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentExample
{
    public class TimeHelper
    {
        public static string ShiftTimeByOneHour(string time)
        {
            string timeFormat = "h:mm tt";

            DateTime timeObj = DateTime.ParseExact(time, timeFormat, CultureInfo.InvariantCulture);

            DateTime newTimeObj = timeObj.AddHours(1);

            return newTimeObj.ToString(timeFormat, CultureInfo.InvariantCulture);
        }
    }
}
