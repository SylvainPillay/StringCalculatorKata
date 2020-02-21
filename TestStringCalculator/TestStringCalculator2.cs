using System;
using NUnit.Framework;
using StringCalculator;

namespace TestStringCalculator
{
    public class TestStringCalculator2
    {
        [Test]
        public void Add_GivenEmptyString_ShouldReturnZero()
        {
            //Arrange
            var useCase = SutBuilder();
            var input = "";
            var expectedResult = 0;
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("1",1)]
        [TestCase("10",10)]
        [TestCase("100",100)]
        public void Given_SingleNumber_ShouldReturnNumber(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase("1,1",2)]
        [TestCase("10,50",60)]
        [TestCase("100,250",350)]
        public void Add_GivenTwoNumbers_ShouldReturnSum(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,1\n7",9)]
        [TestCase("10\n12,8",30)]
        [TestCase("100,100\n93",293)]
        public void Add_GivenNewLinesBetweenNumbers_ShouldAdd(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase("//a\n1,1\n7a2",11)]
        [TestCase("//#\n10\n12#8", 30)]
        [TestCase("//=\n100=100=93=7", 300)]
        public void Add_GivenCustomDelimiterSupplied_ShouldSplitString_AndAddNumbers(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase("//a\n1,-11\n7a2", "negatives not allowed -11")]
        [TestCase("//#\n-10\n-12#-8", "negatives not allowed -10,-12,-8")]
        [TestCase("//=\n-100=-100=-93=7", "negatives not allowed -100,-100,-93")]
        public void Add_GivenNegativeNumbers_ShouldThrowError(string input, string expectedMessage)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act + Assert
            var ex = Assert.Throws<Exception>(() => useCase.Add(input));
            Assert.That(ex.Message.Equals(expectedMessage));
        }
        [TestCase("//a\n1,1\n7a2,1001", 11)]
        [TestCase("//#\n10\n12#8,999", 1029)]
        [TestCase("//=\n1000,100=100=93=7", 1300)]
        public void Add_ShouldNotAdd_NumbersGreaterThan1000(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase("//[aa]\n1,1\n7aa2,1001", 11)]
        [TestCase("//[#33]\n10\n12#338,999", 1029)]
        [TestCase("//[=+=]\n1000,100=+=100=+=93=+=7", 1300)]
        public void Add_GivenCustomDelimiterOfAnyLength_ShouldSum(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase("//[aa][aq]\n1,1\n7aa2aq1001", 11)]
        [TestCase("//[#33][zw]\n10\n12#338zw999", 1029)]
        [TestCase("//[=+=][ex]\n1000ex100=+=100=+=93=+=7", 1300)]
        public void Add_GivenMultipleCustomDelimiterOfAnyLength_ShouldSum(string input, int expected)
        {
            //Arrange
            var useCase = SutBuilder();
            //Act
            var result = useCase.Add(input);
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        private static StringCalculator2 SutBuilder()
        {
            return new StringCalculator2();
        }
    }
}