using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TimeSheetFramework;

namespace TimeSheetTestFramework
{
    [TestFixture]
    class WorkCalculatorTest
    {
        [Test]
        public void testSameTimes()
        {
            WorkCalculator wc = new WorkCalculator();
            string inTime = "13:13";
            string outTime = "13:13";
            double expectedResult = 0.0;

            double hoursWorked = wc.calculateHoursWorked(inTime, outTime);

            Assert.AreEqual(expectedResult, hoursWorked);
        }

        [Test]
        public void test_out_time_greater_than_in_time()
        {
            WorkCalculator wc = new WorkCalculator();
            string inTime = "09:00";
            string outTime = "17:30";
            double expectedResult = 8.5;

            double hoursWorked = wc.calculateHoursWorked(inTime, outTime);

            Assert.AreEqual(expectedResult, hoursWorked);
        }
    }
}
