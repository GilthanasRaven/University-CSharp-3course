using System;

namespace Lab05_08
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lab05
            /*
            try
            {
                //Console.WriteLine("Результат чтения с файла: {0}", Converter.ConvertDoubleFromFile(Environment.CurrentDirectory + "\\Test.txt"));

                Console.WriteLine("-----");

                Console.Write("Введите исходную дробь (с запятой): ");
                double number = double.Parse(Console.ReadLine());
                Console.Write("Введите номер системы счисления: ");
                int notation = int.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("Ответ: {0}", Converter.ConvertDoubleToNotation(number, notation));
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод.");
                Environment.Exit(0);
            }
            */

            /*
            //Lab06
            try
            {
                Console.Write("Введите строку: ");
                string input = Console.ReadLine();
                string[] words = Converter.AlphSort(input);

                Console.WriteLine();
                Console.Write("Результат: ");
                for(int i = 0; i < words.Length; i++)
                {
                    Console.Write(words[i] + " ");
                }
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод.");
                Environment.Exit(0);
            }
            */

            /*
            //Lab07
            try
            {
                Console.Write("Введите строку: ");
                string input = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine(Converter.SymbAlphSort(input));
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод.");
                Environment.Exit(0);
            }
            */

            //Lab08
            try
            {
                Console.Write("Введите строку: ");
                string input = Console.ReadLine();
                Console.Write("Введите особое слово: ");
                string special = Console.ReadLine();
                Console.Write("Введите номер заглавного слова: ");
                int number = int.Parse(Console.ReadLine());
                Converter.InfoString(input, special, number);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод.");
                Environment.Exit(0);
            }
        }
    }
}
