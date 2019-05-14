using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TimeSheetTestFramework
{
    [Binding]
    public class CalculateHoursAndPayFromTimesheetsSteps
    {
        private Timesheet itsTimesheetHandler = new Timesheet();

        [Given(@"I checked-in at ((?:1[0-2]|0?[1-9]):[0-5][0-9] *[AaPp][Mm]) and checked out at ((?:1[0-2]|0?[1-9]):[0-5][0-9] *[AaPp][Mm])")]
        public void GivenIChecked_InAtAMAndCheckedOutAtPM(string checkInTime, string checkoutTime)
        {
            itsTimesheetHandler.setCheckInTime(checkInTime);
            itsTimesheetHandler.setCheckOutTime(checkoutTime);
        }

        [Given(@"have an hourly rate of £(.*)")]
        public void GivenHaveAnHourlyRateOf(Decimal hourlyRate)
        {
            itsTimesheetHandler.setHourlyRate(hourlyRate.ToString());
        }

        [When(@"my day is done")]
        public void WhenMyDayIsDone()
        {
            itsTimesheetHandler.execute();
        }

        [Then(@"the normaiized check-in time should equal (.*)")]
        public void ThenTheNormaiizedCheck_InTimeShouldEqual(string normalizedCheckinTime)
        {
            Assert.AreEqual(normalizedCheckinTime, itsTimesheetHandler.CheckInTimeNormalized());
        }

        [Then(@"the normalized check-out time should (.*)")]
        public void ThenTheNormalizedCheck_OutTimeShould(string normalizedCheckoutTime)
        {
            Assert.AreEqual(normalizedCheckoutTime, itsTimesheetHandler.CheckOutTimeNormalized());
        }

        [Then(@"the hours worked is (.*)")]
        public void ThenTheHoursWorkedIs(Decimal hoursworked)
        {
            Assert.AreEqual(hoursworked.ToString(), itsTimesheetHandler.HoursWorked());
        }

        [Then(@"my pay should be £(.*)")]
        public void ThenMyPayShouldBe(Decimal pay)
        {
            Assert.AreEqual(pay.ToString(), itsTimesheetHandler.Pay());
        }

        [Given(@"I have filled out the form as follows")]
        public void GivenIHaveFilledOutTheFormAsFollows(Table table)
        {
            List<TimesheetData> testData = new List<TimesheetData>();
            int rowNumber = 1;
            foreach (var row in table.Rows)
            {
                string checkInTime = row["Check In Time"];
                string checkOutTime = row["Check Out Time"];
                string hourlyRate = row["Hourly Rate"];
                string checkInTimeNormalized = row["Check In Time Normalized"];
                string checkOutTimeNormalized = row["Check Out Time Normalized"];
                string hoursWorked = row["Hours Worked"];
                string pay = row["Pay"];
                testData.Add(new TimesheetData()
                {
                    itsCheckInTime = checkInTime,
                    itsCheckOutTime = checkOutTime,
                    itsHourlyRate = Double.Parse(hourlyRate),
                    itsCheckInTimeNormalized = checkInTimeNormalized,
                    itsCheckOutTimeNormalized = checkOutTimeNormalized,
                    itsHoursWorked = Double.Parse(hoursWorked),
                    itsPay = Double.Parse(pay),
                    itsRowNo = rowNumber++
                });
            }
            ScenarioContext.Current["test data"] = testData;
        }

        [When(@"I select ProcessEmployeeTimes")]
        public void WhenISelectProcessEmployeeTimes()
        {
            Timesheet ts = new Timesheet();

            foreach (TimesheetData tsheet in (List<TimesheetData>)ScenarioContext.Current["test data"])
            {
                ts.setCheckInTime(tsheet.itsCheckInTime);
                ts.setCheckOutTime(tsheet.itsCheckOutTime);
                ts.setHourlyRate(tsheet.itsHourlyRate.ToString());
                ts.execute();
                tsheet.resultCheckInTimeNormalized = ts.CheckInTimeNormalized();
                tsheet.resultCheckOutTimeNormalized = ts.CheckOutTimeNormalized();
                tsheet.resultHoursWorked = Double.Parse(ts.HoursWorked());
                tsheet.resultPay = Double.Parse(ts.Pay());
            }
        }

        [Then(@"the result should be as per the table")]
        public void ThenTheResultShouldBeAsPerTheTable()
        {
            foreach (TimesheetData tsheet in (List<TimesheetData>)ScenarioContext.Current["test data"])
            {
                Assert.AreEqual(tsheet.itsCheckInTimeNormalized, tsheet.resultCheckInTimeNormalized);
                Assert.AreEqual(tsheet.itsCheckOutTimeNormalized, tsheet.resultCheckOutTimeNormalized);
                Assert.AreEqual(tsheet.itsHoursWorked, tsheet.resultHoursWorked);
                Assert.AreEqual(tsheet.itsPay, tsheet.resultPay);
            }
        }
    }

    public class TimesheetData
    {
        public int itsRowNo { get; set; }
        public String itsCheckInTime { get; set; }
        public String itsCheckOutTime { get; set; }
        public double itsHourlyRate { get; set; }
        public String itsCheckInTimeNormalized { get; set; }
        public String itsCheckOutTimeNormalized { get; set; }
        public double itsPay { get; set; }
        public double itsHoursWorked { get; set; }
        public String resultCheckInTimeNormalized { get; set; }
        public String resultCheckOutTimeNormalized { get; set; }
        public double resultPay { get; set; }
        public double resultHoursWorked { get; set; }
    }
}
