using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetFramework
{
    public class TimeSheetImpl
    {
        private String itsCheckInTime;
        private String itsCheckOutTime;
        private String itsCheckInTimeNormalized = "NIL";
        private String itsCheckOutTimeNormalized = "NIL";
        private double itsRate;
        private double itsHoursWorked = 0;

        private TimeFormatter timefx = new TimeFormatter();

        public String getCheckInTimeNormalized()
        {
            return itsCheckInTimeNormalized;
        }

        public String getCheckOutTimeNormalized()
        {
            return itsCheckOutTimeNormalized;
        }

        public void setCheckInTime(String checkintime)
        {
            this.itsCheckInTime = checkintime;
            try
            {
                itsCheckInTimeNormalized = timefx._format(itsCheckInTime);
            }
            catch (BadlyFormedTime)
            {
                itsCheckInTimeNormalized = timefx.getTokens()[0];
            }
        }

        public void setCheckOutTime(String checkouttime)
        {
            this.itsCheckOutTime = checkouttime;
            try
            {
                itsCheckOutTimeNormalized = timefx._format(itsCheckOutTime);
            }
            catch (BadlyFormedTime ex)
            {
                itsCheckOutTimeNormalized = timefx.getTokens()[0];
            }
        }

        public void setHourlyRate(double rate)
        {
            itsRate = rate;
        }

        public  double  calculatePay()
        {
            return (itsRate * itsHoursWorked);
        }

        public  double  getHoursWorked()
        {
            return itsHoursWorked;
        }

        //returns the number of hours worked
        public double calculateHoursWorked()
        {
            WorkCalculator wc = new WorkCalculator();
            if (itsCheckInTimeNormalized.Equals("ERR") || itsCheckOutTimeNormalized.Equals("ERR"))
                itsHoursWorked = 0;
            else
                itsHoursWorked = wc.calculateHoursWorked(itsCheckInTimeNormalized, itsCheckOutTimeNormalized);

            return itsHoursWorked;
        }
    }
}
