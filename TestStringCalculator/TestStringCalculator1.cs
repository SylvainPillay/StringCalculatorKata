using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    // TODO the files in the Bin & Obj folder have unfortunately become tracked in this repository - done
    //       please research how to untrack them and then action that research.
    // TODO please update these tests to use NUnit, MSTest is the pain - done
    // TODO given your new found knowledge try and improve the test names - done
    // TODO Tests should have //Arrange //Act //Assert comments - done
    [TestFixture]
    public class TestStringCalculator1 
    {
        // TODO, tests should not share state. Doing so couples them and allows them to interact, which is bad. 
        //        Please create an instance per test in their arrange sections using a factory function. - done

        public StringCalculator1 StringCalculatorSutBuilder()
        {
            var useCase = new StringCalculator1();
            return useCase;
        }

        [Test] 
        public void Add_GivenInput_Is_Empty_ShouldReturnZero()
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
        [TestCase("1", 1)]
        [TestCase("25", 25)]
        [TestCase("100",100)]
        public void Add_GivenOnlySingleNumber_ShouldReturnNumber(string input, int expectedResult)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        // TODO, this test doesn't have an assert
        [TestCase("1,1", 2)]
        [TestCase("25,35", 60)]
        [TestCase("100,200", 300)]
        public void Add_GivenTwoNumbers_ShouldReturnSumOfNumbers(string input, int expectedResult)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        // TODO triangulate, refer to TODO comment on class
        [TestCase("//;\n2;2;2;2", 8)]
        [TestCase("//$\n23$31,62$2", 118)]
        [TestCase("//z\n200z278\n2", 480)]
        public void Add_GivenCustomDelimiterAvailable_ShouldSplitWithDelimiter_AndSumAllNumbers(string input, int expectedResult)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [TestCase("//;\n2;-92;2;-2")]
        [TestCase("//$\n-37$87")]
        [TestCase("-74,98")]
        public void Add_GivenNegativeNumberFound_ShouldThrowError(string input)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act + Assert
            var ex = Assert.Throws<Exception>(() => useCase.Add(input));
            Assert.That(ex.Message.Contains("negatives not allowed"));
        }

        // TODO triangulate, refer to TODO comment on class
        [TestCase("//;\n2;2;2;2,1002", 8)]
        [TestCase("//;\n999;0;7;36,1001", 1042)]
        [TestCase("//;\n1000;87;1002", 1087)]
        public void Add_GivenInputContainsNumbersGreaterThan_1000_ShouldNotSumNumber(string input, int expectedResult)
        {
            //Arrange 
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[a][z]\n98z213a8a32", 351)]
        [TestCase("//[*][%][@]\n1*2%3@8", 14)]
        public void Add_GivenMoreThanOneCustomDelimiter_ShouldSplitStringUsingAllDelimiters_AndSum(string input, int expectedResult)
        {
            //Arrange

            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        // TODO triangulate, refer to TODO comment on class
        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[$$]\n10$$20$$30", 60)]
        [TestCase("//[asd]\n100asd200", 300)]
        public void Add_GivenCustomDelimiterOfAnyLength_ShouldSplitStringAndSum(string input, int expectedResult)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestCase("//[***][%%]\n1***2%%3", 6)]
        [TestCase("//[$$][!!!]\n10!!!20$$30", 60)]
        [TestCase("//[asd][pos]\n100asd200pos1pos9", 310)]
        public void Add_GivenMultipleCustomDelimitersOfAnyLength_ShouldSplitStringAndSum(string input, int expectedResult)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}