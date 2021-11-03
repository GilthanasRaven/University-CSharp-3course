using System;
using System.Windows;
using System.Numerics;

namespace Lab01
{
    public partial class App : Application
    {
        //Константы
        private const int MAXPOWER = 3;
        private const int MAXNUM = 20;
        private static double eps = 1E-6;

        //Решение Кардано-Виета
        public static Complex[] SolverViet(double[] coeffs)
        {
            if (coeffs.Length != 4 || coeffs[3] == 0)
            {
                return null;
            }

            Complex[] results = new Complex[3];

            /*
            double upperCoeff = coeffs[3];

            for (int i = 0; i < 3; i++)
            {
                coeffs[i] /= upperCoeff;
            }
            */

            double a = coeffs[3];
            double b = coeffs[2];
            double c = coeffs[1];
            double d = coeffs[0];

            double coeffP = (3*a*c - b*b) / (3*a*a);
            double coeffQ = (2*b*b*b - 9*a*b*c + 27*a*a*d)/(27*a*a*a);

            double valueQ = coeffQ*coeffQ/4 + coeffP*coeffP*coeffP/27;

            if (Math.Abs(valueQ) < eps)
            { 
                valueQ = 0;
            }

            if (valueQ > 0)
            {
                //Действительный и два комплексных
                double valueU = -coeffQ/2d+Math.Sqrt(valueQ);
                if (valueU < 0)
                {
                    valueU = -Math.Exp(Math.Log(-valueU)/3);
                }
                else if (valueU > 0)
                {
                    valueU = Math.Exp(Math.Log(valueU)/3);
                }
                else
                {
                    valueU = -coeffQ/2d-Math.Sqrt(valueQ);
                    valueU = -Math.Exp(Math.Log(-valueU)/3);
                }

                double valueY = valueU - coeffP/(3*valueU);
                double partReal1 = valueY - b/(3*a);
                double partReal2 = -(valueU-coeffP/(3*valueU))/2 - b/(3*a);
                double partImagine = Math.Sqrt(3)/2 * (valueU + coeffP/(3*valueU));

                if(Math.Abs(partReal1) < eps)
                {
                    partReal1 = 0;
                }
                if(Math.Abs(partReal2) < eps)
                {
                    partReal2 = 0;
                }
                if (Math.Abs(partImagine) < eps)
                {
                    partImagine = 0;
                }

                results[0] = new Complex(partReal1, 0);
                results[1] = new Complex(partReal2, partImagine);
                results[2] = new Complex(partReal2, -partImagine);
            }
            else if(valueQ < 0)
            {
                //Три действительных
                double fi;
                if (Math.Abs(coeffQ) < eps)
                { 
                    fi = Math.PI / 2;
                }
                else
                {
                    if (coeffQ < 0)
                    { 
                        fi = Math.Atan(Math.Sqrt(-valueQ) / (-coeffQ / 2));
                    }
                    else
                    { 
                        fi = Math.Atan(Math.Sqrt(-valueQ) / (-coeffQ / 2)) + Math.PI;
                    }
                }
                double r = 2 * Math.Sqrt(-coeffP/3);
                double partReal1 = r * Math.Cos(fi / 3) - coeffs[2] / (3 * a);
                double partReal2 = r * Math.Cos((fi + 2 * Math.PI) / 3) - b / (3 * a);
                double partReal3 = r * Math.Cos((fi + 4 * Math.PI) / 3) - b / (3 * a);

                if (Math.Abs(partReal1) < eps)
                {
                    partReal1 = 0;
                }
                if (Math.Abs(partReal2) < eps)
                {
                    partReal2 = 0;
                }
                if (Math.Abs(partReal3) < eps)
                {
                    partReal3 = 0;
                }

                results[0] = new Complex(partReal1, 0);
                results[1] = new Complex(partReal2, 0);
                results[2] = new Complex(partReal3, 0);
            }
            else if(valueQ == 0)
            {
                if (Math.Abs(coeffQ) < eps)
                {
                    //Кратные действительные
                    double partReal1 = -b/(3*a);
                    double partReal2 = -b/(3*a);
                    double partReal3 = -b/(3*a);

                    if (Math.Abs(partReal1) < eps)
                    {
                        partReal1 = 0;
                    }
                    if (Math.Abs(partReal2) < eps)
                    {
                        partReal2 = 0;
                    }
                    if (Math.Abs(partReal3) < eps)
                    {
                        partReal3 = 0;
                    }

                    results[0] = new Complex(partReal1, 0);
                    results[1] = new Complex(partReal2, 0);
                    results[2] = new Complex(partReal3, 0);
                }
                else
                {
                    //Один действительный и два кратных действительных
                    double valueU = Math.Exp(Math.Log(Math.Abs(coeffQ)/2) / 3);
                    if (coeffQ < 0)
                    {
                        valueU *= -1;
                    }
                    double partReal1 = -2*valueU - b / (3*a);
                    double partReal2 = valueU - b / (3*a);
                    double partReal3 = valueU - b / (3*a);

                    if (Math.Abs(partReal1) < eps)
                    {
                        partReal1 = 0;
                    }
                    if (Math.Abs(partReal2) < eps)
                    {
                        partReal2 = 0;
                    }
                    if (Math.Abs(partReal3) < eps)
                    {
                        partReal3 = 0;
                    }

                    results[0] = new Complex(partReal1, 0);
                    results[1] = new Complex(partReal2, 0);
                    results[2] = new Complex(partReal3, 0);
                }
            }

            return results;
        }

        //Решение Кардано
        public static Complex[] SolverCardano(double[] coeffs)
        {
            if (coeffs.Length != 4 || coeffs[3] == 0)
            {
                return null;
            }

            Complex[] results = new Complex[3];

            double a = coeffs[3];
            double b = coeffs[2];
            double c = coeffs[1];
            double d = coeffs[0];

            double coeffP = -b*b / (3*a*a) + c/a;
            double coeffQ = 2*b*b*b / (27*a*a*a) - b*c / (3*a*a) + d/a;
            double valueQ = coeffQ*coeffQ/4 + coeffP*coeffP*coeffP/27;

            Complex valueT1 = new Complex();
            Complex valueT2 = new Complex();

            if (Math.Abs(valueQ) < eps)
            {
                valueQ = 0;
            }

            if (valueQ > 0)
            {
                double sqrtQ = Math.Sqrt(valueQ);

                double partPos = -coeffQ/2 + sqrtQ;
                double partNeg = -coeffQ/2 - sqrtQ;

                if (partPos < 0)
                {
                    valueT1 = -Math.Pow(-partPos, 1/3d);
                }
                else
                {
                    valueT1 = Math.Pow(partPos, 1/3d);
                }

                if (partNeg < 0)
                {
                    valueT2 = -Math.Pow(-partNeg, 1/3d);
                }
                else
                {
                    valueT2 = Math.Pow(partNeg, 1/3d);
                }

                results[0] = valueT1 + valueT2;
                results[1] = -(valueT1 + valueT2) / 2 + (Math.Sqrt(3) * (valueT1 - valueT2) / 2) * Complex.ImaginaryOne;
                results[2] = -(valueT1 + valueT2) / 2 - (Math.Sqrt(3) * (valueT1 - valueT2) / 2) * Complex.ImaginaryOne;
            }
            else if (valueQ == 0)
            {
                if (-coeffQ/2 > 0)
                {
                    valueT1 = Math.Pow(-coeffQ/2, 1/3d);
                }
                else
                {
                    valueT1 = -Math.Pow(coeffQ/2, 1/3d);
                }

                results[0] = 2*valueT1;
                results[1] = -valueT1;
                results[2] = -valueT1;
            }
            else if(valueQ < 0)
            {
                Complex sqrtQ = Complex.Sqrt(valueQ);

                Complex partPos = -coeffQ/2 + sqrtQ;
                Complex partNeg = -coeffQ/2 - sqrtQ;

                valueT1 = Complex.Pow(partPos, 1/3d);
                valueT2 = Complex.Pow(partNeg, 1/3d);

                results[0] = valueT1 + valueT2;
                results[1] = -(valueT1 + valueT2) / 2 + (Math.Sqrt(3) * (valueT1 - valueT2) / 2) * Complex.ImaginaryOne;
                results[2] = -(valueT1 + valueT2) / 2 - (Math.Sqrt(3) * (valueT1 - valueT2) / 2) * Complex.ImaginaryOne;
            }

            for (int i = 0; i < results.Length; i++)
            {
                results[i] -= b/(3*a);
            }

            if (Math.Abs(results[0].Real) < eps)
            {
                results[0] = new Complex(0, results[0].Imaginary);
            }
            if (Math.Abs(results[1].Real) < eps)
            {
                results[1] = new Complex(0, results[1].Imaginary);
            }
            if (Math.Abs(results[2].Real) < eps)
            {
                results[2] = new Complex(0, results[2].Imaginary);
            }

            return results;
        }

        //Парсер
        public static double[] SolveParser(string equation)
        {
            if(equation.Length == 0)
            {
                return null;
            }

            equation = equation.Replace(" ", "");
            equation = equation.Replace("-", "+-");
            equation = equation.Replace("X", "x");
            equation = equation.Replace("x^", "x");

            if (equation[0] == '+')
            {
                equation = equation.Remove(0, 1);
            }

            string[] monoms = equation.Split('+');
            double[] coeffs = new double[MAXPOWER + 1];

            foreach (var monom in monoms)
            {
                if (monom.Length != 0)
                {
                    string[] parts = monom.Split('x');
                    double coeff;
                    int power;

                    //Моном
                    if (parts.Length == 2)
                    {
                        if (!Int32.TryParse(parts[1], out power) || power > MAXPOWER || power <= 0)
                        {
                            if (parts[1] == "")
                            {
                                power = 1;
                            }
                            else
                            {
                                return null;
                            }
                        }

                        if (Double.TryParse(parts[0], out coeff) && parts[0].Length < MAXNUM)
                        {
                            coeffs[power] += coeff;
                        }
                        else
                        {
                            if (parts[0] == "")
                            {
                                coeffs[power] += 1;
                            }
                            else if (parts[0] == "-")
                            {
                                coeffs[power] += -1;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    //Свободный член
                    else if (parts.Length == 1)
                    {
                        power = 0;

                        if (Double.TryParse(parts[0], out coeff) && parts[0].Length < MAXNUM)
                        {
                            coeffs[power] += coeff;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            return coeffs;
        }
    }
}
