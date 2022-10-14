using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

class Program
{
    static string[] texts = new string[] { "Выберите уровень (выбирайте стрелками вниз-вверх, чтобы поддтвердить нажми - Enter)\n",
                "1 - уровень", "2 - уровень" , "Выход"};

    static void Text(int i)//Замена цвета менющки
    {
        if (i == 1)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(texts[1]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(texts[2]);
            Console.WriteLine(texts[3]);
        }
        if (i == 2)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.WriteLine(texts[1]);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(texts[2]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(texts[3]);
        }
        if (i == 3)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.WriteLine(texts[1]);
            Console.WriteLine(texts[2]);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(texts[3]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    static int keys()//работа менюшки
    {
        int num = 0;
        bool flag = false;
        do
        {
            ConsoleKeyInfo keyPushed = Console.ReadKey();
            if (keyPushed.Key == ConsoleKey.DownArrow)
            {
                num++;
                Text(num);
            }
            if (keyPushed.Key == ConsoleKey.UpArrow)
            {
                num--;
                Text(num);
            }
            if (keyPushed.Key == ConsoleKey.Enter)
            {
                flag = true;
            }
            if (num == 0)
            {
                num = 3;
                Text(3);
            }
            if (num == 4)
            {
                num = 1;
                Text(1);
            }
        } while (!flag);
        return num;
    }
    static void Main(string[] args)
    {
    restart:
        Console.Clear();
        Console.CursorVisible = false;
        Console.SetWindowSize(110, 50);
        foreach (string text in texts)
            Console.WriteLine(text);
        int num = keys();//вызов менюшки 
        var lvl = 0;
        var n = 0;
        var m = 0;
        switch (num)
        {
            case 1: { lvl = 1; n = 20; m = 29; Console.ReadKey(); } break;
            case 2: { lvl = 2; n = 27; m = 27; Console.ReadKey(); } break;
            case 3: { }; goto end;
        }
        var exit = 0;
        var playerX = 0;
        var playerY = 0;
        var mapI = 0;
        var mapJ = 0;
        int i, j;
        int x;
        int y = (Console.WindowHeight / 2) - 6;
        string[] lines1 = File.ReadAllLines(@"maps\level1.txt");
        string[] lines2 = File.ReadAllLines(@"maps\level2.txt");
        char[,] map = new char[30, 30];
        if (lvl == 1)
        {
            for (i = 0; i < 20; i++)
            {
                for (j = 0; j < 29; j++)
                {
                    map[i, j] = lines1[i][j];
                }
            }
        }
        if (lvl == 2)
        {
            for (i = 0; i < 27; i++)
            {
                for (j = 0; j < 27; j++)
                {
                    map[i, j] = lines2[i][j];
                }
            }
        }
        Console.Clear();
        for (i = 0; i < n; i++)
        {
            x = (Console.WindowWidth / 2) - 8;
            for (j = 0; j < m; j++)
            {
                if (map[i, j] == '1')
                {
                    Console.SetCursorPosition(x, y); Console.Write("█"); x++; continue;
                }
                if (map[i, j] == '2')
                {
                    mapI = i;
                    mapJ = j;
                    playerX = x;
                    playerY = y;
                    Console.SetCursorPosition(x, y); Console.Write("☺"); x++; continue;
                }
                if (map[i, j] == '3')
                {
                    Console.SetCursorPosition(x, y); Console.Write("♥"); x++; continue;
                }
                if (map[i, j] == '4')
                {
                    Console.SetCursorPosition(x, y); Console.Write("П"); x++; continue;
                }
                if (map[i, j] == '0')
                {
                    Console.SetCursorPosition(x, y); Console.Write(" "); x++; continue;
                }
            }
            y++;
        }
        Console.WriteLine();
        x = playerX;
        y = playerY;
        i = mapI;
        j = mapJ;
        Console.SetCursorPosition(x, y);
        Console.CursorVisible = false;
        ConsoleKey key;
        while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter && exit == 0)
        {
            if (exit == 1)
            {
                break;
            }
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (map[i - 1, j] == '0' || map[i - 1, j] == '2' || map[i - 1, j] == '3' || map[i - 1, j] == '4')
                    {
                        Console.SetCursorPosition(x, y--); Console.Write(" ");
                        Console.SetCursorPosition(x, y); Console.Write("☺");
                        if (map[i - 1, j] == '4')
                        {
                            goto restart;
                        }
                        i--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[i + 1, j] == '0' || map[i + 1, j] == '2' || map[i + 1, j] == '3' || map[i + 1, j] == '4')
                    {
                        Console.SetCursorPosition(x, y++); Console.Write(" ");
                        Console.SetCursorPosition(x, y); Console.Write("☺");
                        if (map[i + 1, j] == '4')
                        {
                            goto restart;
                        }
                        i++;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (map[i, j + 1] == '0' || map[i, j + 1] == '2' || map[i, j + 1] == '3' || map[i, j + 1] == '4')
                    {
                        Console.SetCursorPosition(x++, y); Console.Write(" ");
                        Console.SetCursorPosition(x, y); Console.Write("☺");
                        if (map[i, j + 1] == '4')
                        {
                            goto restart;
                        }
                        j++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[i, j - 1] == '0' || map[i, j - 1] == '2' || map[i, j - 1] == '3' || map[i, j - 1] == '4')
                    {
                        Console.SetCursorPosition(x--, y); Console.Write(" ");
                        Console.SetCursorPosition(x, y); Console.Write("☺");
                        if (map[i, j - 1] == '4')
                        {
                            goto restart;
                        }
                        j--;
                    }
                    break;
            }
        }
        Console.CursorVisible = false;
        Console.WriteLine();
    end: Console.ReadKey();
    }
}

