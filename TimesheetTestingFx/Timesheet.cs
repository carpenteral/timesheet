using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetFramework;

namespace TimeSheetTestFramework
{
    public  class Timesheet
    {
        private TimeSheetImpl itsImplementation = new TimeSheetImpl();

        public String getCheckInTimeNormalized()
        {
            return itsImplementation.getCheckInTimeNormalized();
        }

        public String getCheckOutTimeNormalized()
        {
            return itsImplementation.getCheckOutTimeNormalized();
        }
    
        public void setCheckInTime(String checkintime)
        {
            itsImplementation.setCheckInTime(checkintime);
        }

        public void setCheckOutTime(String checkouttime)
        {
            itsImplementation.setCheckOutTime(checkouttime);
        }

        public void setHourlyRate(string rate)
        {
            itsImplementation.setHourlyRate(Double.Parse(rate));
        }
    
        public  String  CheckInTimeNormalized()
        {
            return itsImplementation.getCheckInTimeNormalized();
        }

        public  String  CheckOutTimeNormalized()
        {
            return itsImplementation.getCheckOutTimeNormalized();
        }

        public String Pay()
        {
            double  thePay = itsImplementation.calculatePay();
            if (thePay < 0)
                return "NIL";
            else
                return (thePay).ToString("0.00");
        }

        public  String  HoursWorked()
        {
            double hoursWorked = itsImplementation.getHoursWorked();
            if (hoursWorked < 0)
                return "ERR";
            else
                return (hoursWorked).ToString("0.00");
        }
        //returns the number of hours worked
        public double calculateHoursWorked()
        {
            double  hoursWorked = itsImplementation.calculateHoursWorked();

            return hoursWorked;
        }
    
        public  void    execute()
        {
            calculateHoursWorked();
        }
    }
}
