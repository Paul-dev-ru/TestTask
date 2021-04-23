using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {


        static void Main()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Console.WriteLine("Введите путь к текстовому файлу:");
            string adress1 = @"C:\Users\kolts\Desktop\Проги для собеса\ConsoleApp1\ConsoleApp1\test.txt";
            //string adress1 = Console.ReadLine();
            string text = File.ReadAllText(adress1);
            Console.WriteLine("Содержание файла:\n{0}\n", text);
            char[] a = text.ToCharArray();
            List<string> triplet = new List<string>();
            int cnt;
            bool flag;
            string str = "";
            string str1 = "";

            for (int i = 0; i < a.Length - 3; i++)
            {
                if (char.IsLetter(a[i]) & char.IsLetter(a[i + 1]) & char.IsLetter(a[i + 2]))
                {
                    Thread thr = new Thread(() =>  // создание потока
                    {
                        cnt = 0;
                        flag = true;
                        for (int j = 0; j < a.Length - 3; j++)
                        {
                            if (a[i] == a[j] & a[i + 1] == a[j + 1] & a[i + 2] == a[j + 2]) // определение того, точно ли 3 эл-та идущие подряд являются символами
                            {

                                if (flag)
                                {
                                    str = Char.ToString(a[i]) + Char.ToString(a[i + 1]) + Char.ToString(a[i + 2]); // Запись 3х эл-ов
                                    flag = false;
                                }
                                cnt++; //счётчик для одинаковых триплетов
                            }

                        }
                        str = str + " " + cnt.ToString() +" "; // сохранение триплета и кол-во его повторений в тексте

                        MatchCollection matches = Regex.Matches(str1, str); //рег-ое выр-ие для отсечения повторных входов в массив. Если в str1 нет походих элементов из str, тогда добавляем в строку str1 строку str
                        if (matches.Count == 0)
                        {
                            str1 += str;
                        }


                        triplet.Add(str); 
                    });
                    thr.Start(); //запуск потока
                    thr.Join(); //Метод join позволяет дождаться выполнение каждого потока, чтобы не привести программу к критической ошибке
                }
                
            }

            Regex reg = new Regex(@"\d+"); //поиск любой цифры
            MatchCollection m = reg.Matches(str1);
            int[] arr = new int[m.Count];
            for (int i = 0; i < arr.Length; i++)
            {
                    arr[i] = int.Parse(m[i].Value); //вывод кол-во повторений каждого триплета
            }

            Array.Sort(arr); // сортировка всех совпадений триплетов по возрастанию
            Array.Reverse(arr); //перевернули сортировку с "по возрастанию" на "по убыванию"

            string temp;

            for (int i = 0; i < triplet.Count; i++)
            {
                for (int j = 0; j < triplet.Count - 1; j++)
                {

                    if (Convert.ToInt32(triplet[j].Substring(4)) < Convert.ToInt32(triplet[j + 1].Substring(4))) //сортировка списка триплетов по убыванию
                    {
                        temp = triplet[j];
                        triplet[j] = triplet[j + 1];
                        triplet[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\n\nВсего найдено {0} триплетов.\n", triplet.Count);
            Console.WriteLine("10 Самых встречающихся триплетов:\n");
            int k = 0;
            if (arr.Length < 10) //если уникальных триплетов меньше 10, отбираем из имеющихся самые повторяющиеся
            {
 
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(triplet[k].Substring(0, 3) + "; "); // отображение >10 самых повторяющихся триплетов
                    k += arr[i];
                    if (i == arr.Length -1)
                    {
                        Console.Write(triplet[k].Substring(0, 3) + "; "); 
                        k += arr[i];
                    }
                }
            }

            else // если больше 10, тогда отбираем 10 самых повторяющихся
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(triplet[k].Substring(0, 3) + ", "); // отображение 10 самых повторяющихся триплетов
                    k += arr[i];
                    if (i == 9)
                    {
                        Console.Write(triplet[k].Substring(0, 3) + "; ");
                        k += arr[i];
                    }
                }
            }


            stopWatch.Stop(); //прекращение подсчёта времени работы программы
            TimeSpan ts = stopWatch.Elapsed;
            string TimeWork = String.Format("{0:00} часов, {1:00} минут, {2:00} секунд, {3:000} миллисекунд",
                                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds); // вывод времени работы программы
            Console.WriteLine("\n\nВремя выполнения программы: " + TimeWork);
            Console.ReadKey();
        }
    }
}