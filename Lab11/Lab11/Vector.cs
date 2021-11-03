using System;
using System.Collections.Generic;
using System.Text;

namespace Lab11
{
    public class Vector<T> : IComparable, IComparable<Vector<T>> where T : new()
    {
        #region Массив

        T[] arrayObjects;

        public T[] GetArray()
        {
            return arrayObjects;
        }

        #endregion

        #region Конструкторы

        public Vector()
        {

        }

        public Vector(T[] array)
        {
            arrayObjects = (T[])array.Clone();
        }

        public Vector(Vector<T> copyVector)
        {
            arrayObjects = (T[])copyVector.arrayObjects.Clone();
        }

        #endregion

        #region Операции

        //СЛОЖЕНИЕ
        //Сложение векторов
        public static Vector<T> Add(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            CheckCorrect(vectorLeft, vectorRight);
            var vectorResult = new Vector<T>(vectorLeft);

            for(int i = 0; i < vectorRight.Count(); i++)
            {
                vectorResult.arrayObjects[i] += (dynamic)vectorRight.arrayObjects[i];
            }

            return vectorResult;
        }
        //Перегрузка сложения
        public static Vector<T> operator+(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            return Add(vectorLeft, vectorRight);
        }

        //ВЫЧИТАНИЕ
        //Вычитание векторов
        public static Vector<T> Sub(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            CheckCorrect(vectorLeft, vectorRight);
            var vectorResult = new Vector<T>(vectorLeft);

            for (int i = 0; i < vectorRight.Count(); i++)
            {
                vectorResult.arrayObjects[i] -= (dynamic)vectorRight.arrayObjects[i];
            }

            return vectorResult;
        }
        public void Sub(Vector<T> vectorRight)
        {
            CheckCorrect(this, vectorRight);

            for (int i = 0; i < vectorRight.Count(); i++)
            {
                arrayObjects[i] -= (dynamic)vectorRight.arrayObjects[i];
            }
        }
        //Перегрузка вычитания
        public static Vector<T> operator-(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            return Sub(vectorLeft, vectorRight);
        }

        //УМНОЖЕНИЕ
        //Умножение на число
        public static Vector<T> Multiply(Vector<T> vectorLeft, double doubleRight)
        {
            CheckNull(vectorLeft);
            CheckEmpty(vectorLeft);

            var vectorResult = new Vector<T>(vectorLeft);

            for (int i = 0; i < vectorResult.Count(); i++)
            {
                vectorResult.arrayObjects[i] *= (dynamic)doubleRight;
            }

            return vectorResult;
        }
        //Перегрузка умножения
        public static Vector<T> operator*(Vector<T> vectorLeft, double doubleRight)
        {
            return Multiply(vectorLeft, doubleRight);
        }
        public static Vector<T> operator*(double doubleLeft, Vector<T> vectorRight)
        {
            return Multiply(vectorRight, doubleLeft);
        }

        //ДЕЛЕНИЕ
        //Деление на число
        public static Vector<T> Division(Vector<T> vectorLeft, double doubleRight)
        {
            CheckNull(vectorLeft);
            CheckEmpty(vectorLeft);

            var vectorResult = new Vector<T>(vectorLeft);

            for (int i = 0; i < vectorResult.Count(); i++)
            {
                vectorResult.arrayObjects[i] /= (dynamic)doubleRight;
            }

            return vectorResult;
        }
        //Перегрузка деления
        public static Vector<T> operator/(Vector<T> vectorLeft, double doubleRight)
        {
            return Division(vectorLeft, doubleRight);
        }

        //Перегрузка вывода
        public override string ToString() 
        {
            var result = new StringBuilder();
            
            for(int i = 0; i < this.Count(); i++)
            {
                result.Append(this.arrayObjects[i]);
                result.Append(" ");
            }

            return result.ToString();
        }

        #endregion

        #region Методы

        //Модуль
        public static double Abs(Vector<T> vectorValue)
        {
            CheckNull(vectorValue);
            CheckEmpty(vectorValue);

            var typeObject = new T();
            double resultSummary = 0;

            if (typeObject is Complex)
            {
                for (int i = 0; i < vectorValue.Count(); i++)
                {
                    resultSummary += Math.Pow(Complex.Abs((dynamic)vectorValue.arrayObjects[i]), 2);
                }
            }
            else
            {
                for (int i = 0; i < vectorValue.Count(); i++)
                {
                    resultSummary += Math.Pow((dynamic)vectorValue.arrayObjects[i], 2);
                }
            }

            return Math.Sqrt(resultSummary);
        }

        //Скалярное произведение
        public static T Scalar(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            CheckCorrect(vectorLeft, vectorRight);

            T resultSummary = new T();

            for (int i = 0; i < vectorLeft.Count(); i++)
            {
                resultSummary += (dynamic)vectorLeft.arrayObjects[i]*vectorRight.arrayObjects[i];
            }

            return resultSummary;
        }

        //Ортогонализация
        public static Vector<T>[] Orthogonalizate(Vector<T>[] vectorArray)
        {
            var resultArray = new Vector<T>[vectorArray.Length];

            resultArray[0] = vectorArray[0];
            for (int i = 1; i < vectorArray.Length; i++)
            {
                var iterVector = new Vector<T>(vectorArray[i]);

                CheckNull(iterVector);
                CheckEmpty(iterVector);

                for (int j = 0; j < vectorArray.Length - 1; j++)
                {
                    var projVector = (dynamic)Scalar(vectorArray[i], resultArray[j]) / Scalar(resultArray[j], resultArray[j]) * resultArray[j];
                    iterVector.Sub(projVector);
                }
                resultArray[i] = iterVector;
            }

            return resultArray;
        }

        #endregion

        #region Проверки

        public int Count()
        {
            return arrayObjects.Length;
        }

        public static bool IsNull(Vector<T> valueVector)
        {
            return valueVector is null;
        }

        private static void CheckNull(Vector<T> valueVector)
        {
            if (IsNull(valueVector))
            {
                throw new ArgumentNullException("Vector object is null.");
            }
            if (valueVector.arrayObjects is null)
            {
                throw new ArgumentNullException("Vector array is null.");
            }
        }

        private static void CheckLength(Vector<T> leftVector, Vector<T> rightVector)
        {
            if (leftVector.Count() != leftVector.Count())
            {
                throw new ArgumentException("The sizes of the vectors are not equal.");
            }
        }

        public static bool IsEmpty(Vector<T> valueVector)
        {
            return valueVector.Count() == 0;
        }

        private static void CheckEmpty(Vector<T> valueVector)
        {
            if (IsEmpty(valueVector))
            {
                throw new ArgumentException("Vector is empty.");
            }
        }

        private static void CheckCorrect(Vector<T> leftVector, Vector<T> rightVector)
        {
            CheckNull(leftVector);
            CheckNull(rightVector);
            CheckEmpty(leftVector);
            CheckEmpty(rightVector);
            CheckLength(leftVector, rightVector);
        }

        #endregion

        #region Сравнения

        //Общее сравнение
        public int CompareTo(object objectValue)
        {
            if (this is null || objectValue is null)
            {
                throw new ArgumentNullException(nameof(objectValue));
            }
            else if (objectValue is Vector<T>)
            {
                return CompareTo(objectValue as Vector<T>);
            }
            else
            {
                throw new ArgumentException("Unreal to compare these types.");
            }
        }

        //Сравнение векторов
        public int CompareTo(Vector<T> vectorValue)
        {
            CheckLength(this, vectorValue);

            return Abs(this).CompareTo(Abs(vectorValue));
        }

        //Перегрузка операторов сравнения
        public static bool operator>=(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            return vectorLeft.CompareTo(vectorRight) != -1;
        }
        public static bool operator<=(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            return vectorLeft.CompareTo(vectorRight) != 1;
        }
        public static bool operator >(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            return vectorLeft.CompareTo(vectorRight) == 1;
        }
        public static bool operator <(Vector<T> vectorLeft, Vector<T> vectorRight)
        {
            return vectorLeft.CompareTo(vectorRight) == -1;
        }

        #endregion
    }
}
