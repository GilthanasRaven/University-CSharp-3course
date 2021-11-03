using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab05_08
{
    class Converter
    {
        private const double EPS = 1E-12;
        private const int MAXDECIMAL = 12;
        private const int MAXNOTATION = 32;

        //Lab05
        //Перевод десятичных
        public static string ConvertDoubleToNotation(double number, int notation)
        {
            string result = "";
            string intResult = "";
            string fracResult = "";

            List<double> decimals = new List<double>();
            List<int> integers = new List<int>();

            double numberPart = number;
            int intPart = (int)numberPart;
            double fracPart = numberPart - intPart;

            decimals.Add(fracPart);
            integers.Add(intPart);

            intResult = ConvertIntegerToNotation(intPart, notation);
            while(fracPart != 0)
            {
                numberPart = Math.Round(fracPart*notation, MAXDECIMAL);
                intPart = (int)numberPart;
                fracPart = Math.Round(numberPart-intPart, MAXDECIMAL);

                //Console.WriteLine("---");
                //Console.WriteLine(numberPart);
                //Console.WriteLine(intPart);
                //Console.WriteLine(fracPart);
                //Console.WriteLine("---");

                if (decimals.Contains(fracPart) && integers.Contains(intPart) && decimals.IndexOf(fracPart) == integers.IndexOf(intPart))
                {
                    fracResult = fracResult.Insert(decimals.IndexOf(fracPart), "(");
                    fracResult += ")";
                    break;
                }
                else
                {
                    fracResult += ConvertIntegerToNotation(intPart, MAXNOTATION);
                    decimals.Add(fracPart);
                    integers.Add(intPart);
                }
            }

            result = intResult + "," + fracResult;

            return result;
        }

        public static string ConvertDoubleFromFile(string path)
        {
            Console.WriteLine(path);

            string[] parse;

            double number = 0;
            int notation = 0;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    parse = sr.ReadLine().Split(" ");

                    if(parse.Length != 2)
                    {
                        throw new FormatException();
                    }

                    number = double.Parse(parse[0]);
                    notation = int.Parse(parse[1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Некорректный файл.");
                Environment.Exit(0);
            }
            

            return ConvertDoubleToNotation(number, notation);
        }

        //Lab06
        public static string[] AlphSort(string input)
        {
            if(input == "")
            {
                throw new FormatException();
            }

            string[] words = input.Split(" ");
            string create = "";
            
            Array.Sort(words);

            for (int i = 0; i < words.Length; i++)
            {
                if(words[i] != "")
                {
                    create += words[i][words[i].Length - 1];
                }
            }

            Array.Resize(ref words, words.Length + 1);

            words[words.Length-1] = create;

            return words;
        }

        //Lab07
        public static string SymbAlphSort(string input)
        {
            if (input == "")
            {
                throw new FormatException();
            }

            input = string.Concat(input.OrderBy(x => x).ToArray());

            return input;
        }

        //Lab08
        public static void InfoString(string input, string special, int number)
        {
            string[] words = input.Split(" ");

            if (input == "" || special == "" || words.Length < 2 || number < 1)
            {
                throw new FormatException();
            }

            Console.WriteLine();
            Console.WriteLine("Количество вхождений: {0}", CountWords(input, special));

            words[words.Length-2] = special;
            Console.Write("Результат: ");
            for (int i = 0; i < words.Length; i++)
            {
                Console.Write(words[i] + " ");
            }
            Console.WriteLine();

            words = words.Where(x => char.IsUpper(x[0])).ToArray();
            if(words.Length <= number)
            {
                Console.WriteLine("Заглавное слово {0}: {1}", number, words[number-1]);
            }
            else
            {
                Console.WriteLine("Заглавное слово {0} отсутствует.", number);
            }
        }

        private static int CountWords(string input, string word)
        {
            int count = (input.Length-input.Replace(word, "").Length) / word.Length;
            return count;
        }

        //Вспомогательное
        //Lab04
        //Антигорнер
        public static string ConvertIntegerToNotation(int number, int notation)
        {
            string result;
            int remainder;

            if (notation < 0)
            {
                throw new FormatException();
            }

            if (number == 0)
            {
                return "0";
            }
            else
            {
                result = "";
            }

            while (number != 0)
            {
                remainder = number % notation;
                if (remainder < 10)
                {
                    remainder = remainder + '0';
                }
                else
                {
                    remainder = remainder + 'A' - 10;
                }

                result = (char)remainder + result;
                number /= notation;
            }

            return result;
        }
    }
}
