using System;

namespace Lab02_04
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //2
                /*
                Console.WriteLine("Экспонента: {0} ", MyMath.Exponent());
                Console.WriteLine("Число PI: {0} ~ 3,141592653589793", MyMath.Pi(1E-100));
                Console.WriteLine("Ln2: {0}", MyMath.Ln2(1E-8));
                Console.WriteLine();
                */

                //3a
                /*
                Console.Write("Введите ряд символов: ");
                double aver = MyMath.Average(Console.ReadLine());
                Console.WriteLine("Среднее арифметическое: {0}", aver);
                Console.WriteLine();
                

                //3b
                Console.Write("Введите ряд чисел: ");
                double geom = MyMath.Geometric(Console.ReadLine());
                Console.WriteLine("Среднее геометрическое: {0}", geom);
                Console.WriteLine();
                */

                //4
                Console.Write("Введите исходное число: ");
                int number = int.Parse(Console.ReadLine());
                Console.Write("Введите номер системы счисления: ");
                int notation = int.Parse(Console.ReadLine());
                Console.WriteLine("Ответ: {0}", MyMath.ConvertTo(number, notation));
                
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод.");
                Environment.Exit(0);
            }
            Console.WriteLine();
        }
    }
}
