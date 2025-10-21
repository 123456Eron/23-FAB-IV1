namespace MMKMFP_LR1_Task_2
{
    internal class Program
    {
        static bool IsValid(string s) => s.Length > 0 && s.Trim('0', '1').Length == 0;
        static string Add(string a, string b)
        {
            // Остаток.
            int c = 0;
            var result = "";
            for (int i = a.Length - 1; i >= 0; i--)
            {
                // Считаем сумму с учётом переноса с учётом char формата.
                int sum = a[i] - '0' + b[i] - '0' + c;
                result = (sum % 2) + result;
                c = sum / 2;
            }
            //Приписываем слева остаток.
            return c == 1 ? "1" + result : result;
        }

        static void Main()
        {
            Console.WriteLine("Введите в двоичном формате слагаемые одиноковой длины в столбик:");
            var a = Console.ReadLine();
            var b = Console.ReadLine();
            if (!IsValid(a) || !IsValid(b) || a.Length != b.Length)
            {
                Console.WriteLine("Ошибка ввода");
                return;
            }
            Console.WriteLine(Add(a, b));
        }
    }
}
