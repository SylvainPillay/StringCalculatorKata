using System;
using NUnit.Framework;
using StringCalculator;
using Assert = NUnit.Framework.Assert;

namespace TestStringCalculator
{
    [TestFixture]
    public class TestStringCalculator1 
    {
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

        [TestCase("6\n1,1", 8)]
        [TestCase("87,25\n35", 147)]
        [TestCase("\n100,200", 300)]
        public void Add_GivenNewLineBetweenNumbers_WithCommas_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

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

        [TestCase("//;\n2;-92;2;-2", "negatives not allowed -92,-2")]
        [TestCase("//$\n-37$87", "negatives not allowed -37")]
        [TestCase("-74,98", "negatives not allowed -74")]
        public void Add_GivenNegativeNumberFound_ShouldThrowError(string input,string expectedErrorMessage)
        {
            //Arrange
            var useCase = StringCalculatorSutBuilder();
            //Act + Assert
            var ex = Assert.Throws<Exception>(() => useCase.Add(input));
            Assert.That(ex.Message.Equals(expectedErrorMessage));
        }

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