using System;
using System.Globalization;

internal static class Program
{
    private const double G = 9.81;
    private const double K1 = 5.0;
    private const double K2 = 0.02;

    private static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        RunTask1();
        Console.WriteLine();
        RunTask2();
    }

    private static void RunTask1()
    {
        Console.WriteLine("Задание 1. Свободное падение с сопротивлением, вариант 6.");
        var mass = ReadDouble("Введите массу тела m (кг): ");
        var v0 = ReadDouble("Введите начальную скорость v0 (м/с): ");
        var h0 = ReadDouble("Введите начальную высоту h0 (м): ");
        var dt = ReadDouble("Введите шаг по времени Δt (с): ");

        Console.WriteLine();
        Console.WriteLine("i\t t, c\t v, м/с\t h, м");

        var t = 0.0;
        var v = v0;
        var h = h0;
        var i = 0;
        const double maxTime = 1000.0; // защита от зависания

        Console.WriteLine($"{i}\t {t:F3}\t {v:F3}\t {h:F3}");

        while (t < maxTime)
        {
            var dv = (G - (K1 * v + K2 * v * v) / mass) * dt;
            v += dv;
            h += v * dt;
            t += dt;
            i++;

            if (h <= 0.0)
            {
                h = 0.0;
                Console.WriteLine($"{i}\t {t:F3}\t {v:F3}\t {h:F3}");
                break;
            }

            Console.WriteLine($"{i}\t {t:F3}\t {v:F3}\t {h:F3}");
        }
    }

    private static void RunTask2()
    {
        Console.WriteLine("Задание 2. Полёт под углом к горизонту, вариант 6.");
        const double v0 = 75.0;
        var alpha = Math.PI / 8.0;
        const double dt = 0.05;

        var vx = v0 * Math.Cos(alpha);
        var vy = v0 * Math.Sin(alpha);

        Console.WriteLine("i\t t, c\t x, м\t y, м");

        var t = 0.0;
        var x = 0.0;
        var y = 0.0;
        var i = 0;

        Console.WriteLine($"{i}\t {t:F3}\t {x:F3}\t {y:F3}");

        while (true)
        {
            t += dt;
            x += vx * dt;
            vy -= G * dt;
            y += vy * dt;
            i++;

            if (y <= 0.0)
            {
                y = 0.0;
                Console.WriteLine($"{i}\t {t:F3}\t {x:F3}\t {y:F3}");
                break;
            }

            Console.WriteLine($"{i}\t {t:F3}\t {x:F3}\t {y:F3}");
        }
    }

    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            Console.WriteLine("Ошибка ввода. Повторите попытку.");
        }
    }
}
