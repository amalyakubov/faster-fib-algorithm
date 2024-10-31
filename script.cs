using System;
using System.Numerics;

namespace QuickPowerAlgorithm;

class Program
{
    static BigInteger[,] MatrixMultiplication(BigInteger[,] firstMatrix, BigInteger[,] secondMatrix)
    {
        if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
            throw new ArgumentException("Matrix dimensions don't match for multiplication");

        BigInteger[,] result = new BigInteger[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
        for (int i = 0; i < firstMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < secondMatrix.GetLength(1); j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < firstMatrix.GetLength(1); k++)
                {
                    result[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                }
            }
        }
        return result;
    }

    static BigInteger[,] MatrixExponentiation(BigInteger[,] inputMatrix, int power)
    {
        if (power < 0)
            throw new ArgumentException("Power must be non-negative");

        if (power == 0)
        {
            // Return identity matrix
            int n = inputMatrix.GetLength(0);
            BigInteger[,] identity = new BigInteger[n, n];
            for (int i = 0; i < n; i++)
                identity[i, i] = 1;
            return identity;
        }

        if (power == 1)
            return inputMatrix;

        BigInteger[,] result = MatrixExponentiation(inputMatrix, power / 2);
        result = MatrixMultiplication(result, result);

        if (power % 2 == 1)
            result = MatrixMultiplication(result, inputMatrix);

        return result;
    }

    static BigInteger Fibonacci(int n)
    {
        if (n < 0)
            throw new ArgumentException("n must be non-negative");

        if (n <= 1)
            return n;

        BigInteger[,] fibMatrix = new BigInteger[,]
        {
            { 1, 1 },
            { 1, 0 },
        };

        return MatrixExponentiation(fibMatrix, n - 1)[0, 0];
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Enter the number to find the Fibonacci number:");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        try
        {
            Console.WriteLine($"The {n}th Fibonacci number is {Fibonacci(n)}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
