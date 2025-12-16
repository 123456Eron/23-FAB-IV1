using System;

namespace Lab5Variant6
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SimulateTargetHits();
            Console.WriteLine();
            SimulateEqualEvents();

            Console.WriteLine();
            Console.WriteLine("Нажмите Enter для выхода.");
            Console.ReadLine();
        }

        private static void SimulateTargetHits()
        {
            const int trials = 100;
            const int outcomesCount = 2; // например, 4 равновероятных исхода попадания
            int[] counts = new int[outcomesCount];
            Random random = new Random();

            Console.WriteLine("Задание 1. Моделирование попаданий в мишень (равновероятные исходы).");
            Console.WriteLine("№\tИсход");

            for (int i = 1; i <= trials; i++)
            {
                int outcome = random.Next(outcomesCount); // 0..outcomesCount-1
                counts[outcome]++;

                Console.WriteLine("{0}\t{1}", i, outcome);
            }

            Console.WriteLine("Частоты исходов:");
            for (int i = 0; i < outcomesCount; i++)
            {
                Console.WriteLine("Исход {0}: {1}", i, counts[i]);
            }
        }

        private static void SimulateEqualEvents()
        {
            const int trials = 100;
            const int eventsCount = 3; // пример: 3 равновероятных события
            int[] counts = new int[eventsCount];
            Random random = new Random();

            Console.WriteLine("Задание 2. Моделирование равновероятных событий.");
            Console.WriteLine("№\tСобытие");

            for (int i = 1; i <= trials; i++)
            {
                int eventIndex = random.Next(eventsCount); // 0..eventsCount-1
                counts[eventIndex]++;

                Console.WriteLine("{0}\t{1}", i, eventIndex);
            }

            Console.WriteLine("Частоты событий:");
            for (int i = 0; i < eventsCount; i++)
            {
                Console.WriteLine("Событие {0}: {1}", i, counts[i]);
            }
        }
    }
}
