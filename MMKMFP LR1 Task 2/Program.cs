using System.Threading.Tasks;

namespace MMKMFP_LR1_Task_2
{
    class BinaryAdder
    {
        // Функция проверки валидности
        static bool IsValid(string s) => s.Length > 0 && s.Trim('0', '1').Length == 0;

        // Одноразрядный двоичный сумматор
        private bool Summator(bool A, bool B, bool P0, out bool P)
        {
            P = A && B || B && P0 || A && P0;
            return A ^ B ^ P0;               
        }

        string AddBinary(string a, string b)
        {
            // Выравниваем числа по длине, приписывая нули слева
            int maxLen = Math.Max(a.Length, b.Length);
            a = a.PadLeft(maxLen, '0');
            b = b.PadLeft(maxLen, '0');

            bool carry = false;  
            char[] result = new char[maxLen];

            // Складываем начиная с младших разрядов (справа налево)
            for (int i = maxLen - 1; i >= 0; i--)
            {
                // Преобразуем символы в логические значения
                bool bitA = a[i] == '1';
                bool bitB = b[i] == '1';

                bool sum = Summator(bitA, bitB, carry, out carry);
                result[i] = sum ? '1' : '0';
            }

            string sumResult = new string(result);
            // Если остался перенос - добавляем единицу в начало
            return carry ? "1" + sumResult : sumResult;
        }
 
        static void Main()
        {
            BinaryAdder adder = new BinaryAdder();
            Console.WriteLine("Двоичный сумматор");
            // Ввод чисел с проверкой валидности
            // Цикл продолжается пока ввод не станет валидным
            string num1;
            do
            {
                Console.Write("Введите первое двоичное число: ");
                num1 = Console.ReadLine();
            } while (!IsValid(num1)); 

            string num2;
            do
            {
                Console.Write("Введите второе двоичное число: ");
                num2 = Console.ReadLine();
            } while (!IsValid(num2));
            // Выполняем сложение и выводим результат
            string result = adder.AddBinary(num1, num2);
            Console.WriteLine($"Результат: {num1} + {num2} = {result}");
        }
    }
}
