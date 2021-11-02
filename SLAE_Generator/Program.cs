﻿using System;
using System.IO;
using System.Text;

namespace SLAE_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность уравнения:");
            var n = Convert.ToInt32(Console.ReadLine());
            var m = new double[n, n];
            var v = new double[n];
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {
                m[i, i] = 1;
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{m[i, j],-2}");
                }
                v[i] = Math.Pow(-1, rand.Next(3)) * rand.Next(1, 100);
                Console.WriteLine(v[i]);
            }

            double k;
            for (int i = 0; i < n; i++)
            {
                k = rand.NextDouble() + 0.5;
                for (int j = 0; j < n; j++)
                {
                    m[i, j] *= k;
                }
                v[i] *= k;

                for (int i2 = 0; i2 < i; i2++)
                {
                    k = Math.Pow(-1, rand.Next(3)) * rand.NextDouble();
                    for (int j = 0; j < n; j++)
                    {
                        m[i2, j] += m[i, j] * k;
                    }
                    v[i2] += v[i] * k;
                }

                for (int i2 = i + 1; i2 < n; i2++)
                {
                    k = Math.Pow(-1, rand.Next(3)) * rand.NextDouble();
                    for (int j = 0; j < n; j++)
                    {
                        m[i2, j] += m[i, j] * k;
                    }
                    v[i2] += v[i] * k;
                }
            }


            var maxIndex = 1;
            var max = Math.Abs(m[maxIndex, 0]);
            for (int i = maxIndex + 1; i < n; i++)
            {
                var curValue = Math.Abs(m[i, 0]);
                if (curValue > max)
                {
                    maxIndex = i;
                    max = curValue;
                }
            }

            k = -m[0, 0] / m[maxIndex, 0];
            m[0, 0] = 0;
            for (int j = 1; j < n; j++)
            {
                m[0, j] += m[maxIndex, j] * k;
            }
            v[0] += v[maxIndex] * k;


            Console.WriteLine();

            var sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.Append($"{m[i, j]}|");
                    Console.Write($"{Math.Round(m[i, j], 2)}\t");
                }
                sb.Append($"{v[i]}\n");
                Console.WriteLine(Math.Round(v[i], 2));
            }

            //не забываем менять путь к файлу, если нужно
            var filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\SLAE.txt";
            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Полное имя сохраненного файла: {filePath}");
            Console.WriteLine("P.S. Не закрывай меня: тебе еще корни сравнивать...");
            Console.WriteLine("Чтобы закрыть приложение, введите пароль 'C# is best'.");
            var pwd = "";
            do
            {
                Console.Write($"Пароль: ");
                pwd = Console.ReadLine();
                Console.CursorTop -= 1;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            while (pwd != "C# is best");
        }
    }
}
