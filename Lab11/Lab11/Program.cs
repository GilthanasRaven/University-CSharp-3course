using System;

namespace Lab11
{
    class Program
    {
        private static void fooExample(object obj, EventArgsComplexZeroDivision args)
        {
            Console.WriteLine("Произошло непредвиденное деление на 0...");
        }

        static void Main(string[] args)
        {
            //Агригейт экспешн
            //Пример
            Complex exampleComplexA = new Complex(-1.00, 0.00);
            Complex exampleComplexB = new Complex(1.00, 2.00);
            exampleComplexA.HandlerComplexZeroDivisionEvent += fooExample;

            try
            {
                Console.WriteLine(exampleComplexA+exampleComplexB);
                Console.WriteLine(exampleComplexA-exampleComplexB);
                Console.WriteLine(exampleComplexA*exampleComplexB);
                Console.WriteLine(exampleComplexA/exampleComplexB);
                Console.WriteLine(Complex.Abs(exampleComplexA));
                Console.WriteLine(Complex.Arg(exampleComplexA));
                var sqrtArray = Complex.Sqrt(exampleComplexA, 4);
                Console.WriteLine("Корни:\n {0};\n {1};\n {2};\n {3};\n", sqrtArray[0], sqrtArray[1], sqrtArray[2], sqrtArray[3]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Complex[] coords = { new Complex(1, 3), new Complex(1, 2), new Complex(1, 2) };
            Vector<Complex> a1 = new Vector<Complex>(coords);

            try
            {
                Console.WriteLine(a1+a1);
                Console.WriteLine(a1-a1);
                Console.WriteLine(a1*3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                    Console.WriteLine(a1 > a1*2);
                    Console.WriteLine(a1+a1 <= a1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
