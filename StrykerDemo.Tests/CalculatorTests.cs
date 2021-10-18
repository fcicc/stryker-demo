using System;
using Xunit;

namespace StrykerDemo.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_WithTwoIntegers_ReturnsSum()
        {
            // Arrange
            Calculator calculator = new Calculator();
            int a = 2;
            int b = 3;

            // Act
            int result = calculator.Add(a, b);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Subtract_WithTwoIntegers_ReturnsDifference()
        {
            // Arrange
            Calculator calculator = new Calculator();
            int a = 10;
            int b = 7;

            // Act
            int result = calculator.Subtract(a, b);

            // Assert
            Assert.Equal(3, result);
        }
    }
}
