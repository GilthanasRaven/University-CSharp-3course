using System;
using System.Collections.Generic;
using System.IO;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Snirou\\Desktop\\Sheets\\6sem\\C#\\Lab12\\Lab12\\input.txt";

            try
            {
                try
                {

                Dictionary<string, int> wordCounter = FileScanner.FileAnalyse(path);
                foreach(var word in wordCounter)
                {
                    Console.WriteLine("{0} - {1}", word.Key, word.Value);
                }

                Console.WriteLine();
                Console.WriteLine(FileScanner.FileAnalyseOften(path));

                Console.WriteLine();
                Console.WriteLine(FileScanner.FileSearch(path, "heh"));

                }
                catch(ArgumentException)
                {
                    Console.WriteLine("Неверный параметр.");
                }
            }
            catch(IOException)
            {
                Console.WriteLine("Ошибка открытия файла \"{0}\".", path);
            }
        }
    }
}
