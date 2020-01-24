using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using StringCalculator;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestStringCalculator
{
    [TestClass]
    public class StringCalculator_UnitTest6
    {
        private StringCalculator1 calc = new StringCalculator1();

        [TestMethod]
        public void Add_PassedEmptyString_Returns0()
        {
            var input = "";
            var expectedResult = 0;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Add_Passed1Number_Returns1()
        {
            var input = "1";
            var expectedResult = 1;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Add_PassedTwoNumber_Returns4()
        {
            var input = "2,2";
            var expectedResult = 4;
            var result = calc.Add(input);
        }

        [TestMethod]
        public void Add_PassedCustomDelimiter_Returns8()
        {
            var input = "//;\n2;2;2;2";
            var expectedResult = 8;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Add_NegativesError()
        {
            var input = "//;\n2;2;2;-2";
            var expectedResult = 6;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Add_PassedNumbersGreaterThan1000_Returns8()
        {
            var input = "//;\n2;2;2;2,1002";
            var expectedResult = 8;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void Add_PassedMoreThanOneDelimiter_Returns6()
        {
            var input = "//[*][%]\n1*2%3";
            var expectedResult = 6;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void Add_PassedDelimiterOfAnyLen_Returns6()
        {
            var input = "//[***]\n1***2***3";
            var expectedResult = 6;
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }
        //Thanks!!For Watching..
    }
}