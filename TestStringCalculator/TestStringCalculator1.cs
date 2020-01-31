using System;
using System.Reflection.Metadata.Ecma335;
using NUnit.Framework;
using StringCalculator;
using Assert = NUnit.Framework.Assert;

namespace TestStringCalculator
{
    // TODO feedback a lot of the cases being tested below only have one example input/output.
    //       It's a good idea to triangulate (have 3 examples) for each case. This helps us prevent off by 1
    //       errors, build trust in our tests, when in low gear helps grow the algorithm, and finally reduces
    //       the likelihood of optimizations or refactoring breaking code without tests failing.
    //
    //       Examples:
    //         In the single number scenario, the production code could be modified with the short circuit:
    //            if(input.Length == 1) return 1;
    //         And no test would fail!
    //         
    //         In the ignore numbers greater than 1000 scenario:
    //           - There is currently an off by one issue:
    //               Currently the input "2,2,2,2,1000" will return 8 when it should return 1008.
    //           - The production code could be modified to:
    //               return numberList.Where(a => a < 1000).Sum();  
    //             And no test would fail!
    //
    //       You are more than welcome to use the [TestCase] attribute.
    //
    // TODO the files in the Bin & Obj folder have unfortunately become tracked in this repository
    //       please research how to untrack them and then action that research.
    // TODO please update these tests to use NUnit, MSTest is the pain
    // TODO given your new found knowledge try and improve the test names
    // TODO Tests should have //Arrange //Act //Assert comments
    [TestFixture]
    public class TestStringCalculator1 // TODO why does this class have the number 6 at the end
    {
        // TODO, tests should not share state. Doing so couples them and allows them to interact, which is bad.
        //        Please create an instance per test in their arrange sections using a factory function.

        public StringCalculator1 StringCalculatorSutBuilder()
        {
            var useCase = new  StringCalculator1();
            return useCase;
        }

        [Test]
        public void Add_PassedEmptyString_Returns0()
        {
            //Arrange
            var input = "";
            var expectedResult = 0;
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [Test]
        public void Add_Passed1Number_Returns1()
        {
            //Arrange
            var input = "1";
            var expectedResult = 1; 
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        // TODO, this test doesn't have an assert
        [Test]
        public void Add_PassedTwoNumber_Returns4()
        {
            //Arrange
            var input = "2,2";
            var expectedResult = 4;
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        // TODO triangulate, refer to TODO comment on class
        [Test]
        public void Add_PassedCustomDelimiter_Returns8()
        {
            //Arrange
            var input = "//;\n2;2;2;2";
            var expectedResult = 8;
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [Test]
        public void Add_NegativesError()
        {
            //Arrange
            var input = "//;\n2;2;2;-2";
            var useCase = StringCalculatorSutBuilder();
            //Act + Assert
            var ex = Assert.Throws<Exception>(() => useCase.Add(input));
            Assert.That(ex.Message, Is.EqualTo("negatives not allowed -2"));
        }

        // TODO triangulate, refer to TODO comment on class
        [Test]
        public void Add_PassedNumbersGreaterThan1000_Returns8()
        {
            //Arrange
            var input = "//;\n2;2;2;2,1002";
            var expectedResult = 8; 
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [Test]
        public void Add_PassedMoreThanOneDelimiter_Returns6()
        {
            //Arrange
            var input = "//[*][%]\n1*2%3";
            var expectedResult = 6; 
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [Test]
        public void Add_PassedDelimiterOfAnyLen_Returns6()
        {
            //Arrange
            var input = "//[***]\n1***2***3";
            var expectedResult = 6; 
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO missing test for multiple delimiters of different lengths 
    }
}