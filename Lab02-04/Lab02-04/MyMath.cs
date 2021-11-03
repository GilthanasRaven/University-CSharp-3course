using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02_04
{
    class MyMath
    {
        private const double defaultEps = 1E-15;
        private const int limit = 15;

        //Lab02
        //Экспонента
        public static double Exponent(double eps = defaultEps)
        {
            double result = 0;
            double shift = 1;
            int n = 0;

            while(Math.Abs(shift) > eps)
            {
                shift = 1;
                for(int i = 1; i <= n; i++)
                {
                    shift /= i;
                }
                result += shift;
                n++;
            }

            return Math.Round(result, limit);
        }

        //Пи
        public static double Pi(double eps = defaultEps)
        {
            double result = 0;
            double shift = 1;
            double n = 0;

            while (Math.Abs(shift) > eps)
            {
                shift = (1f/Math.Pow(16, n))*(4d/(8*n+1) - 2d/(8*n+4) - 1d/(8*n+5) - 1d/(8*n+6));
                result += shift;
                n++;
            }

            return Math.Round(result, limit);
        }

        //Логарифм
        public static double Ln2(double eps = defaultEps)
        {
            double result = 0;
            double shift = 1;
            int n = 1;

            while (Math.Abs(shift) > eps)
            {
                shift = 1d/n;

                if(n%2 == 0)
                {
                    result -= shift;
                }
                else
                {
                    result += shift;
                }
                
                n++;
            }

            return Math.Round(result, limit);
        }

        //Lab03
        //Среднее арифметическое
        public static double Average(string parse)
        {
            string[] lexems = parse.Split(' ');
            double result = 0;
            int counter = 0;

            foreach (var lexem in lexems)
            {
                double number;
                if (Double.TryParse(lexem, out number))
                {
                    result += number;
                    counter++;
                }
            }

            if(counter == 0)
            {
                return 0;
            }

            result /= counter;

            return result;
        }
        //Среднее геометрическое
        public static double Geometric(string parse)
        {
            string[] lexems = parse.Split(' ');
            double result = 1;

            for (int i = 0; i < lexems.Length; i++)
            {
                double number;
                if (Double.TryParse(lexems[i], out number))
                {
                    result *= number;
                }
                else
                {
                    throw new FormatException();
                }
            }

            result = Math.Pow(result, 1/3d);

            return result;
        }

        //Lab04
        //Антигорнер
        public static string ConvertTo(int number, int notation)
        {
            string result;
            int remainder;

            if(notation < 0)
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
