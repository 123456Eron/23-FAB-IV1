using System;
using System.Globalization;

namespace Lab4_Variant6
{
    internal static class Program
    {
        private const int SampleSize = 1000;

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            GenerateUniform();
            GenerateExponential();
            GenerateNormal();
        }

        private static void GenerateUniform()
        {
            const double x0 = 0.6;
            double[] values = new double[SampleSize];

            double x = x0;
            for (int i = 0; i < SampleSize; i++)
            {
                x = NextUniformByFunction(x);
                values[i] = x;
            }

            Console.WriteLine("Задание 1. Равномерное распределение (линейный конгруэнтный метод, вариант 6).");
            PrintBasicStatistics(values);
            Console.WriteLine();
        }

        private static double NextUniformByFunction(double current)
        {
            double argument = current;
            double value = Math.Cos(argument);
            double fractional = value - Math.Floor(value);
            if (fractional < 0.0)
            {
                fractional += 1.0;
            }

            return fractional;
        }

        private static void GenerateExponential()
        {
            const double lambda = 0.06;
            double[] values = new double[SampleSize];

            Random random = new Random();
            for (int i = 0; i < SampleSize; i++)
            {
                double eta = random.NextDouble();
                if (eta == 1.0)
                {
                    eta = 1.0 - double.Epsilon;
                }

                double x = -Math.Log(1.0 - eta) / lambda;
                values[i] = x;
            }

            Console.WriteLine("Задание 2. Экспоненциальное распределение (λ = 0.06).");
            PrintBasicStatistics(values);
            Console.WriteLine();
        }

        private static void GenerateNormal()
        {
            double[] values = new double[SampleSize];

            Random random = new Random();
            for (int i = 0; i < SampleSize; i++)
            {
                double eta = random.NextDouble();
                double x = NormalFromUniform(eta);
                values[i] = x;
            }

            Console.WriteLine("Задание 3. Нормальное распределение (приведённый закон).");
            PrintBasicStatistics(values);
            Console.WriteLine();
        }

        private static double NormalFromUniform(double eta)
        {
            if (eta == 0.5)
            {
                return 0.0;
            }

            int sign = eta > 0.5 ? 1 : -1;
            double value = sign * Math.Sqrt(-Math.PI / 2.0 * Math.Log(4.0 * eta * (1.0 - eta)));
            return value;
        }

        private static void PrintBasicStatistics(double[] data)
        {
            double sum = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }

            double mean = sum / data.Length;

            double sumSquares = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                double deviation = data[i] - mean;
                sumSquares += deviation * deviation;
            }

            double variance = sumSquares / data.Length;

            Console.WriteLine("Количество значений: {0}", data.Length);
            Console.WriteLine("Математическое ожидание (выборочное): {0:F6}", mean);
            Console.WriteLine("Дисперсия (выборочная): {0:F6}", variance);
        }
    }
}
