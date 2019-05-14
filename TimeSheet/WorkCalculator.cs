using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace TimeSheetFramework
{
    public  class WorkCalculator
    {
        private CultureInfo itsLocale = new CultureInfo("en-GB");

        //returns the number of hours worked
        public double calculateHoursWorked( string checkInTime, string checkOutTime )
        {
            double theHoursWorked = 0;

            TimeSpan tp1 = TimeSpan.Parse(checkInTime);
            TimeSpan tp2 = TimeSpan.Parse(checkOutTime);

            double diff = tp2.TotalMilliseconds - tp1.TotalMilliseconds;
            long hours = (long)(diff / (60 * 60 * 1000) % 24);
            double mins = diff / (60 * 1000) % 60;
            long secs = (long)((diff / 1000) % 60);

            theHoursWorked = hours + mins / 60;

            Console.WriteLine(hours.ToString("00") + ":" + mins.ToString("00"));

            return theHoursWorked;
        }

    }
}
