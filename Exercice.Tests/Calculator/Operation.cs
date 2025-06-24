namespace Calculator;

public class Operation : IOperation
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public double Divide(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException("Division par zéro n'est pas permise");
        return (double)a / b;
    }

    public double Power(double a, double b)
    {
        if (b < 0) throw new ArgumentException("L'exposant ne peut pas être négatif");
        return (double)Math.Pow(a, b);
    }

    public int Square(int a)
    {
        return a * a;
    }

    public int Cube(int a)
    {
        return a * a * a;
    }

    public int Factorial(int a)
    {
        if (a < 0) throw new ArgumentOutOfRangeException("Le nombre ne peut pas être négatif");
        if (a == 0 || a == 1) return 1;

        int result = 1;
        for (int i = 2; i <= a; i++)
        {
            result *= i;
        }

        return result;
    }

    public int SquareRoot(int a)
    {
        if (a < 0) throw new ArgumentOutOfRangeException("Le nombre ne peut pas être négatif");
        return (int)Math.Sqrt(a);
    }

    public int CubeRoot(int a)
    {
        if (a < 0) throw new ArgumentOutOfRangeException("Le nombre ne peut pas être négatif");
        return (int)Math.Round(Math.Pow(a, 1.0 / 3.0));
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}