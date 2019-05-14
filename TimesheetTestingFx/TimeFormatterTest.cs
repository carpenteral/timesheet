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
    class TimeFormatterTest
    {
        [Test]
        public void testGetInstance()
        {
            Console.WriteLine("getInstance");
            TimeFormatter result = TimeFormatter.getInstance();
            Assert.IsNotNull(result);
        }

        /**
         * Test of format method, of class TimeFormatter.
         */
        [Test]
        public void testFormat_AM()
        {
            Console.WriteLine("format");
            String input = "08:25AM";
            TimeFormatter instance = new TimeFormatter();
            String expResult = "08:25";
            String result = instance._format(input);
            Assert.AreEqual(expResult, result);
        }

        [Test]//, ExpectedException(typeof(BadlyFormedTime))]
        public void testFormat_BAD_PM()
        {
            Console.WriteLine("format");
            String input = "13:25AM";
            TimeFormatter instance = new TimeFormatter();
            Assert.Throws<BadlyFormedTime>(() => instance._format(input));
        }

        [Test]
        public void testFormat_Edge_1202PM()
        {
            Console.WriteLine("format");
            String input = "12:02PM";
            TimeFormatter instance = new TimeFormatter();
            String expResult = "12:02";
            String result = instance._format(input);
            Assert.AreEqual(expResult, result);
        }

        [Test]
        public void testFormat_Edge_1202AM()
        {
            Console.WriteLine("format");
            String input = "12:02AM";
            TimeFormatter instance = new TimeFormatter();
            String expResult = "00:02";
            String result = instance._format(input);
            Assert.AreEqual(expResult, result);
        }

        [Test]
        public void testFormat_GOOD_PM()
        {
            Console.WriteLine("format");
            String input = "13:25PM";
            TimeFormatter instance = new TimeFormatter();
            String expResult = "13:25";
            String result = instance._format(input);
            Assert.AreEqual(expResult, result);
        }

        [Test]
        public void testFormat_PM_TO_24()
        {
            Console.WriteLine("format");
            String input = "4:25PM";
            TimeFormatter instance = new TimeFormatter();
            String expResult = "16:25";
            String result = instance._format(input);
            Assert.AreEqual(expResult, result);
        }

        /**
         * Test of tokenise method, of class TimeFormatter.
         */
        [Test]
        public void testTokenise_numbers()
        {
            Console.WriteLine("tokenise");
            String input = "09:13";
            String[] inputTest = {"09", "13"};
            TimeFormatter instance = new TimeFormatter();
            instance.tokenise(input, ":");
            Assert.AreEqual(inputTest, instance.getTokens());
        }
    
        [Test]
        public void testTokenise_numbers_with_PM()
        {
            Console.WriteLine("tokenise");
            String input = "09:13 PM";
            String[] inputTest = {"09", "13", "PM"};
            TimeFormatter instance = new TimeFormatter();
            instance.tokenise(input, @":|\s+");
            Assert.AreEqual(inputTest, instance.getTokens());
        }
    
        [Test]
        public void testTokenise_numbers_with_AM()
        {
            Console.WriteLine("tokenise");
            String input = "09:13  am";
            String[] inputTest = {"09", "13", "AM"};
            TimeFormatter instance = new TimeFormatter();
            instance.tokenise(input, @":|\s+");
            Assert.AreEqual(inputTest, instance.getTokens());
        }

        [Test]
        public void testTokeniseSplit_numbers()
        {
            Console.WriteLine("tokenise");
            String input = "09:13";
            String[] inputTest = {"09", "13"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }

        [Test]
        public void testTokeniseSplit_numbers_PM()
        {
            Console.WriteLine("tokenise");
            String input = "09:13 PM";
            String[] inputTest = {"09", "13", "PM"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }

        [Test]
        public void testTokeniseSplit_24HR_AM_Edge()
        {
            Console.WriteLine("tokenise");
            String input = "13:13 AM";
            String[] inputTest = {"ERR", "ERR", "ERR"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }

        [Test]
        public void testTokeniseSplit_numbers_AM()
        {
            Console.WriteLine("tokenise");
            String input = "09:13 AM";
            String[] inputTest = {"09", "13", "AM"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }

        [Test]
        public void testTokeniseSplit_numbers_AM_NO_Space()
        {
            Console.WriteLine("tokenise");
            String input = "09:13AM";
            String[] inputTest = {"09", "13", "AM"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }

        [Test]
        public void testTokeniseSplit_numbers_PM_NO_Space()
        {
            Console.WriteLine("tokenise");
            String input = "09:13PM";
            String[] inputTest = {"09", "13", "PM"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }

        [Test]
        public void testTokeniseSplit_24hr_HMM_to_HHMM()
        {
            Console.WriteLine("tokenise");
            String input = "9:13";
            String[] inputTest = {"9", "13"};
            TimeFormatter instance = new TimeFormatter();
            String[] result = instance.tokeniseWithSplit(input, @":|\s+");
            Assert.AreEqual(inputTest, result);
        }
    }
}
