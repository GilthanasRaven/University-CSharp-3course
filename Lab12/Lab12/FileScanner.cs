using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Lab12
{
    public static class FileScanner
    {
        //Анализ файла на слова
        static public Dictionary<string, int> FileAnalyse(string path)
        {
            Dictionary<string, int> wordCounter = new Dictionary<string, int>();

            if (File.Exists(path))
            {
                string[] content = File.ReadAllLines(path);

                foreach (var str in content)
                {
                    string[] words = str.Split(" ");
                    
                    foreach (var word in words)
                    {
                        if(word != "")
                        {
                            if (wordCounter.ContainsKey(word))
                            {
                                wordCounter[word] = wordCounter[word]+1;
                            }
                            else
                            {
                                wordCounter.Add(word, 1);
                            }
                        }
                    }
                }

                return wordCounter;
            }
            else
            {
                throw new IOException("File error.");
            }
        }

        //Узнать самое частое слово
        static public string FileAnalyseOften(string path)
        {
            var wordCounter = FileAnalyse(path);

            if (wordCounter.Count == 0)
            {
                return "";
            }

            int maxCount = wordCounter.Values.Max();

            return wordCounter.Where(x => x.Value == maxCount).FirstOrDefault().Key;
        }

        //Поиск слова и вывод информации
        static public string FileSearch(string path, string searchWord)
        {
            if(searchWord == "" || searchWord == Environment.NewLine || searchWord == "\r")
            {
                throw new ArgumentException("Illegal search argument.", nameof(searchWord));
            }

            StringBuilder result = new StringBuilder();
            int searchCounter = 0;

            result.Append("Результат поиска слова \"");
            result.Append(searchWord);
            result.Append("\":\n");

            using (StreamReader wordReader = new StreamReader(path))
            {
                string str;
                int lineCounter = 0;

                while ((str = wordReader.ReadLine()) != null)
                {
                    lineCounter++;
                    var words = str.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x));
                        
                        
                    foreach (var word in words)
                    {
                        if (word == searchWord)
                        {
                            searchCounter++;
                            result.AppendFormat("{0} - \"{1}\"\n", lineCounter, str);
                        }
                    }
                }
            }

            result.AppendFormat("Всего слово встречается {0} раз.\n", searchCounter);

            return result.ToString();
            
        }
    }
}
