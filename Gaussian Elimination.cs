using System;

namespace GaussianElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] coefficients = {
                { 3, -2, 5, 0 },
                { 4, 5, 8, 1 },
                { 1, 1, 2, 1 }
            };

            double[] constants = { -1, 4, 2 };

            double[] solutions = Solve(coefficients, constants);

            Console.WriteLine("Solution for the system of linear equations:");
            for (int i = 0; i < solutions.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {solutions[i]}");
            }
        }

        static double[] Solve(double[,] coefficients, double[] constants)
        {
            int n = constants.Length;
            double[] solutions = new double[n];

            // Gaussian Elimination with partial pivoting
            for (int i = 0; i < n; i++)
            {
                int maxIndex = FindMaxIndex(coefficients, i);
                SwapRows(coefficients, constants, i, maxIndex);

                for (int j = i + 1; j < n; j++)
                {
                    double factor = coefficients[j, i] / coefficients[i, i];
                    for (int k = i; k < n; k++)
                    {
                        coefficients[j, k] -= factor * coefficients[i, k];
                    }
                    constants[j] -= factor * constants[i];
                }
            }

            // Back substitution
            for (int i = n - 1; i >= 0; i--)
            {
                solutions[i] = constants[i] / coefficients[i, i];
                for (int j = i - 1; j >= 0; j--)
                {
                    constants[j] -= solutions[i] * coefficients[j, i];
                    coefficients[j, i] = 0;
                }
            }

            return solutions;
        }

        static int FindMaxIndex(double[,] coefficients, int col)
        {
            int maxIndex = col;
            int n = coefficients.GetLength(0);
            for (int i = col + 1; i < n; i++)
            {
                if (Math.Abs(coefficients[i, col]) > Math.Abs(coefficients[maxIndex, col]))
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        static void SwapRows(double[,] coefficients, double[] constants, int row1, int row2)
        {
            int n = coefficients.GetLength(1);
            if (row1 != row2)
            {
                for (int i = 0; i < n; i++)
                {
                    double temp = coefficients[row1, i];
                    coefficients[row1, i] = coefficients[row2, i];
                    coefficients[row2, i] = temp;
                }

                double tempConstant = constants[row1];
                constants[row1] = constants[row2];
                constants[row2] = tempConstant;
            }
        }
    }
}