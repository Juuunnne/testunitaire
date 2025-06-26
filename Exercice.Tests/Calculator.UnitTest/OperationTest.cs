namespace Calculator.Test;

public class OperationTest
{
    [Trait("Category", "Operation_Add")]
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, -2, -3)]
    [InlineData(0, 0, 0)]
    [InlineData(100, 200, 300)]
    [InlineData(-100, 200, 100)]
    public void Add_WithVariousInputs_ShouldReturnOperationSum(int a, int b, int expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        int result = operation.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Substrack")]
    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, -3, -2)]
    [InlineData(10, -2, 12)]
    [InlineData(500, 200, 300)]
    public void Substract_WithVariousInputs_ShouldReturnOperationDifference(int a, int b, int expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        int result = operation.Subtract(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Multiply")]
    [Theory]
    [InlineData(6, 6, 36)]
    [InlineData(0, 5, 0)]
    [InlineData(-3, -3, 9)]
    [InlineData(4, -2, -8)]
    [InlineData(100, 100, 10000)]
    public void Multiply_WithVariousInputs_ShouldReturnOperationProduct(int a, int b, int expected)
    {
        // Arrange
        var operation = new Operation();
        // Act
        int result = operation.Multiply(a, b);
        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Divide")]
    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(0, 1, 0)]
    [InlineData(-6, -2, 3)]
    [InlineData(9, -3, -3)]
    public void Divide_WithVariousInputs_ShouldReturnOperationResult(int a, int b, double expecected)
    {
        // Arrange
        var operation = new Operation();

        //Act
        var result = operation.Divide(a, b);

        // Assert
        Assert.Equal(expecected, result);
    }

    [Trait("Category", "Operation_Divide_With_Zero_Number")]
    [Fact]
    public void Divide_WithNumberByZero_ShouldThrowException()
    {
        // Arrange
        var operation = new Operation();
        int a = 5;
        int b = 0;

        //Act
        Assert.Throws<DivideByZeroException>(() => operation.Divide(a, b));
    }

    [Trait("Category", "Operation_Power")]
    [Theory]
    [InlineData(2, 3, 8)]
    [InlineData(5, 0, 1)]
    [InlineData(3, 2, 9)]
    [InlineData(10, 1, 10)]
    [InlineData(-2, 3, -8)]
    [InlineData(0.25, 1.50, 0.125)]
    public void Power_WithVariousInputs_ShouldReturnOperationPower(double a, double b, double expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        double result = operation.Power(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Power_With_Negative_Exponent")]
    [Fact]
    public void Power_WithNumberByZero_ShouldThrowException()
    {
        // Arrange
        var operation = new Operation();
        double a = 5.0;
        double b = -5.0;

        //Act
        Assert.Throws<ArgumentException>(() => operation.Power(a, b));
    }

    [Trait("Category", "Operation_Square")]
    [Theory]
    [InlineData(2, 4)]
    [InlineData(5, 25)]
    [InlineData(-3, 9)]
    [InlineData(10, 100)]
    [InlineData(0.25, 0.0625)]
    [InlineData(0, 0)]
    public void Square_WithVariousInputs_ShouldReturnOperationSquare(double a, double expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        double result = operation.Square(a);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Cube")]
    [Theory]
    [InlineData(2, 8)]
    [InlineData(5, 125)]
    [InlineData(-3, -27)]
    [InlineData(10, 1000)]
    [InlineData(0.25, 0.015625)]
    public void Cube_WithVariousInputs_ShouldReturnOperationCube(double a, double expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        double result = operation.Cube(a);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Factorial")]
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(6, 720)]
    public void Factorial_WithPositiveInputs_ShouldReturnOperationFactorial(int a, int expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        int result = operation.Factorial(a);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_Factorial_With_Negative_Input")]
    [Fact]
    public void Factorial_WithNegativeInput_ShouldThrowException()
    {
        // Arrange
        var operation = new Operation();
        int a = -5;

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => operation.Factorial(a));
    }

    [Trait("Category", "Operation_SquareRoot")]
    [Theory]
    [InlineData(4, 2)]
    [InlineData(25, 5)]
    [InlineData(0, 0)]
    [InlineData(100, 10)]
    public void SquareRoot_WithPositiveInputs_ShouldReturnOperationSquareRoot(int a, int expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        int result = operation.SquareRoot(a);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_SquareRoot_With_Negative_Input")]
    [Fact]
    public void SquareRoot_WithNegativeInput_ShouldThrowException()
    {
        // Arrange
        var operation = new Operation();
        int a = -4;

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => operation.SquareRoot(a));
    }

    [Trait("Category", "Operation_CubeRoot")]
    [Theory]
    [InlineData(8, 2)]
    [InlineData(0, 0)]
    [InlineData(1000, 10)]
    [InlineData(-27, -3)]
    public void CubeRoot_WithPositiveInputs_ShouldReturnOperationCubeRoot(int a, int expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        double result = operation.CubeRoot(a);

        // Assert
        Assert.Equal(expected, result);
    }

    [Trait("Category", "Operation_IsEven")]
    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(100, true)]
    [InlineData(0, true)]
    [InlineData(-2, true)]
    public void IsEven_WithEvenNumber_ShouldReturBool(int a, bool expected)
    {
        // Arrange
        var operation = new Operation();

        // Act
        bool result = operation.IsEven(a);

        // Assert
        Assert.Equal(expected, result);
    }
}