using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheetFramework;


namespace TimeSheetTestFramework
{
    [TestClass]
    class TimesheetMSTest
    {
        private Timesheet itsTimeSheet = new Timesheet();

        [TestMethod]
        //[Ignore]
        public  void    inject_test_data()
        {
            var values = new[] {
                new { checkInTime="9:00 AM", checkOutTime="9:00 pm", hourlyRate=7, checkInTimeNormaized="09:00", checkOutTimeNormalized="21:00", hoursWorked="12.00", pay="84.00" },
                new { checkInTime="10:30 AM", checkOutTime="12:30PM", hourlyRate=4, checkInTimeNormaized="10:30", checkOutTimeNormalized="12:30", hoursWorked="2.00", pay="8.00" }
            };
            
            values.ToList().ForEach(val => {
                // Arrange
                itsTimeSheet.setCheckInTime(val.checkInTime);
                itsTimeSheet.setCheckOutTime(val.checkOutTime);
                itsTimeSheet.setHourlyRate(val.hourlyRate.ToString());

                // Act
                itsTimeSheet.execute();

                // Assert
                string checkInTimeNormalized = itsTimeSheet.CheckInTimeNormalized();
                string checkOutTimeNormalized = itsTimeSheet.CheckOutTimeNormalized();
                string hoursWorked = itsTimeSheet.HoursWorked();
                string pay = itsTimeSheet.Pay();

                Assert.AreEqual(val.checkInTimeNormaized, checkInTimeNormalized);
                Assert.AreEqual(val.checkOutTimeNormalized, checkOutTimeNormalized);
                Assert.AreEqual(val.hoursWorked, hoursWorked);
                Assert.AreEqual(val.pay, pay);
            });
        }
    }
}
