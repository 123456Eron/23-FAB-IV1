namespace MMKMFP_LR1_Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите входные значения X1-X5 в столбик");
            bool[] exis = new bool[5];
            //Построчный ввод в массив входных значений.
            for (int i = 0; i < exis.Length; i++) exis[i] = Convert.ToBoolean(Console.ReadLine());  
            //Расчёт математической модели и вывод.
            Console.WriteLine($"\r\nY = {(exis[0] && exis[1]) || (!exis[2] && exis[3]) || exis[4]}");  
        }
    }
}
