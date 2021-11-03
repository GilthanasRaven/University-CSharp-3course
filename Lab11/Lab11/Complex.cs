using System;
using System.Text;

namespace Lab11
{
    //Основной класс
    public class Complex
    {
        #region Данные

        private double real;
        private double imagine;

        private const double eps = 1e-12;

        public event EventHandler<EventArgsComplexZeroDivision> HandlerComplexZeroDivisionEvent;

        #endregion

        #region Конструкторы

        //Пустой конструктор
        public Complex() : this(0, 0)
        {

        }

        //Реальный конструктор
        public Complex(double valueReal) : this(valueReal, 0)
        {

        }

        //Реальный и мнимый конструктор
        public Complex(double valueReal, double valueImagine)
        {
            real = valueReal;
            imagine = valueImagine;
        }

        //Конструктор копирования
        public Complex(Complex copyComplex) : this(copyComplex.real, copyComplex.imagine) 
        { 
        
        }

        #endregion

        #region Операции

        //СЛОЖЕНИЕ
        //Сложение комплексных
        public static Complex Add(Complex complexLeft, Complex complexRight)
        {
            CheckNull(complexLeft);
            CheckNull(complexRight);

            return new Complex(complexLeft.real + complexRight.real, complexLeft.imagine + complexRight.imagine);
        }
        //Сложение с числом
        public static Complex Add(Complex complexLeft, double doubleRight)
        {
            CheckNull(complexLeft);

            return new Complex(complexLeft.real + doubleRight, complexLeft.imagine);
        }
        //Перегрузка сложения
        public static Complex operator+(Complex complexLeft, Complex complexRight)
        {
            return Add(complexLeft, complexRight);
        }
        public static Complex operator+(double doubleLeft, Complex complexRight)
        {
            return Add(complexRight, doubleLeft);
        }
        public static Complex operator+(Complex complexLeft, double doubleRight)
        {
            return Add(complexLeft, doubleRight);
        }

        //ВЫЧИТАНИЕ
        //Вычитание комплексных
        public static Complex Sub(Complex complexLeft, Complex complexRight)
        {
            CheckNull(complexLeft);
            CheckNull(complexRight);

            return new Complex(complexLeft.real-complexRight.real, complexLeft.imagine-complexRight.imagine);
        }
        //Вычитание с числом
        public static Complex Sub(Complex complexLeft, double doubleRight)
        {
            CheckNull(complexLeft);

            return new Complex(complexLeft.real-doubleRight, complexLeft.imagine); ;
        }
        //Перегрузка вычитания
        public static Complex operator-(Complex complexLeft, Complex complexRight)
        {
            return Sub(complexLeft, complexRight);
        }

        //УМНОЖЕНИЕ
        //Умножение комплексных
        public static Complex Multiply(Complex complexLeft, Complex complexRight)
        {
            CheckNull(complexLeft);
            CheckNull(complexRight);

            return new Complex(complexLeft.real*complexRight.real-complexLeft.imagine*complexRight.imagine, complexLeft.real*complexRight.imagine+complexLeft.imagine*complexRight.real);
        }
        //Умножение на число
        public static Complex Multiply(Complex complexLeft, double doubleRight)
        {
            CheckNull(complexLeft);

            return new Complex(complexLeft.real*doubleRight, complexLeft.imagine*doubleRight);
        }
        //Перегрузка умножения
        public static Complex operator*(Complex complexLeft, Complex complexRight)
        {
            return Multiply(complexLeft, complexRight);
        }
        public static Complex operator*(Complex complexLeft, double doubleRight)
        {
            return Multiply(complexLeft, doubleRight);
        }
        public static Complex operator*(double doubleLeft, Complex complexRight)
        {
            return Multiply(complexRight, doubleLeft);
        }

        //ДЕЛЕНИЕ
        //Деление комплексных
        public static Complex Division(Complex complexLeft, Complex complexRight)
        {
            CheckNull(complexLeft);
            CheckNull(complexRight);

            if (Abs(complexRight) < eps)
            {
                var divisonEvent = new EventArgsComplexZeroDivision(complexLeft, complexRight);
                complexLeft.HandlerComplexZeroDivisionEvent?.Invoke(complexLeft, divisonEvent);
                throw new DivideByZeroException("Dividing on zero.");
            }

            double resultReal = (complexLeft.real*complexRight.real+complexLeft.imagine*complexRight.imagine)/(complexRight.real*complexRight.real+complexRight.imagine*complexRight.imagine);
            double resultImagine = (complexRight.real*complexLeft.imagine-complexLeft.real*complexRight.imagine)/(complexRight.real*complexRight.real+complexRight.imagine*complexRight.imagine);

            return new Complex(resultReal, resultImagine);
        }
        //Перегрузка деления
        public static Complex operator/(Complex complexLeft, Complex complexRight)
        {
            return Division(complexLeft, complexRight);
        }

        //Перегрузка вывода
        public override string ToString() 
        {
            var result = new StringBuilder();

            //эпсилон
            if(real == 0 && imagine != 0)
            {
                result.Append("i*");
                result.Append(imagine);
            }
            else if(real != 0 && imagine == 0)
            {
                result.Append(real);
            }
            else if(real == 0 && imagine == 0)
            {
                result.Append(0);
            }
            else
            {
                result.Append(real);
                result.Append("+i*");
                result.Append(imagine);
            }

            return result.ToString();
        }

        #endregion

        #region Методы

        //Модуль
        public static double Abs(Complex valueComplex)
        {
            CheckNull(valueComplex);

            return Math.Sqrt(valueComplex.real*valueComplex.real+valueComplex.imagine*valueComplex.imagine);
        }
        //Аргумент
        public static double Arg(Complex valueComplex)
        {
            CheckNull(valueComplex);

            double result = 0;
            double arctangComplex = Math.Atan(Math.Abs(valueComplex.imagine/valueComplex.real));

            if (valueComplex.real > 0)
            {
                if(valueComplex.imagine < 0)
                {
                    result = 2*Math.PI-arctangComplex;
                }
                else if (valueComplex.imagine >= 0)
                {
                    result = arctangComplex;
                }
            }
            else if(valueComplex.real == 0)
            {
                if (valueComplex.imagine < 0)
                {
                    result = 3 * Math.PI / 2;
                }
                else if (valueComplex.imagine > 0)
                {
                    result = Math.PI / 2;
                }
            }
            else if(valueComplex.real < 0)
            {
                if (valueComplex.imagine < 0)
                {
                    result = Math.PI + arctangComplex;
                }
                else if (valueComplex.imagine >= 0)
                {
                    result = Math.PI - arctangComplex;
                }
            }

            return result;
        }
        //Корень
        public static Complex[] Sqrt(Complex valueComplex, int power)
        {
            CheckNull(valueComplex);

            if(power < 1)
            {
                throw new ArgumentException("Illegal power.");
            }

            var resultArray = new Complex[power];

            for(int i = 0; i < power; i++)
            {
                double complexAbs = Abs(valueComplex);
                double complexArg = Arg(valueComplex);
                double complexReal = Math.Pow(complexAbs, (double)(1/power))*(Math.Cos((complexArg+2*Math.PI*i)/power));
                double complexImagine = Math.Pow(complexAbs, (double)(1/power))*(Math.Sin((complexArg+2*Math.PI*i)/power));

                if(Math.Abs(complexReal) < eps)
                {
                    complexReal = 0;
                }
                if(Math.Abs(complexImagine) < eps)
                {
                    complexImagine = 0;
                }

                resultArray[i] = new Complex(complexReal, complexImagine);
            }

            return resultArray;
        }
        //Степень
        public static Complex Power(Complex valueComplex, int power)
        {
            CheckNull(valueComplex);
            
            if(power == 0)
            {
                return new Complex(1, 0);
            }

            double complexAbs = Math.Pow(Complex.Abs(valueComplex), power);
            double complexArg = Complex.Arg(valueComplex);
            double complexReal = complexAbs*(Math.Cos(complexArg*power));
            double complexImagine = complexAbs*Math.Cos(complexArg*power);
            return new Complex(complexReal, complexImagine);
        }

        #endregion

        #region Проверки

        public static bool IsNull(Complex checkComplex)
        {
            return checkComplex is null;
        }

        private static void CheckNull(Complex checkComplex)
        {
            if(IsNull(checkComplex))
            {
                throw new ArgumentNullException("Complex object is null.");
            }
        }

        //Сравнения

        #endregion
    }
}
