﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleHashing;

namespace Utillities;

public class Utilities
{
    public static bool CheckStringIsAnInt(string s)
    {
        switch (s)
        {
            case null:
            case "":
                return false;
        }

        return int.TryParse(s, out var i);
    }

    public static int ConvertToInt32(string? s)
    {
        return s.Equals(null) ? 0 : int.Parse(s);
    }

    public static bool HashVerification(string? passwordHash, string password)
    {
        return PBKDF2.Verify(passwordHash, password);
    }

    public static void Disclaimer()
    {
        WriteColor("Disclaimer this is NOT a real bank application. \n", ("{NOT}", ConsoleColor.Red));
        Console.Write("This console based application is part of an assignment done by s3813866 and s3785952 \n" +
                      "for their first Assignment in Web Development Technologies COSC2277.");

        // TODO
        // Thread.Sleep(5000);
        Console.Clear();
    }

    public static string ReadPassword(char mask = '*')
    {
        const int enter = 13, backsp = 8, ctrlbacksp = 127;
        int[] filtered = {0, 27, 9, 10 /*, 32 space, if you care */}; // const

        var pass = new Stack<char>();
        char chr;

        while ((chr = Console.ReadKey(true).KeyChar) != enter)
            if (chr == backsp)
            {
                if (pass.Count <= 0) continue;
                Console.Write("\b \b");
                pass.Pop();
            }
            else if (chr == ctrlbacksp)
            {
                while (pass.Count > 0)
                {
                    Console.Write("\b \b");
                    pass.Pop();
                }
            }
            else if (filtered.Any(x => chr == x))
            {
            }
            else
            {
                pass.Push(chr);
                Console.Write(mask);
            }

        Console.WriteLine();

        return new string(pass.Reverse().ToArray());
    }

    public static void WriteColor(string str, params (string substring, ConsoleColor color)[] colors)
    {
        var words = Regex.Split(str, @"( )");

        foreach (var word in words)
        {
            var cl = colors.FirstOrDefault(x => x.substring.Equals("{" + word + "}"));
            if (cl.substring != null)
            {
                Console.ForegroundColor = cl.color;
                Console.Write(cl.substring.Substring(1, cl.substring.Length - 2));
                Console.ResetColor();
            }
            else
            {
                Console.Write(word);
            }
        }
    }

    public static void WriteColoured(string text, ConsoleColor colour)
    {
        Console.ForegroundColor = colour;
        Console.WriteLine(text);
        Console.ResetColor();
    }


    public static bool IsEightDigits(string str)
    {
        return Regex.IsMatch(str, @"^\d{8}$");
    }

    public static double GetRidOfDecimals(double number)
    {
        return Math.Round(number, 0);
    }

    public static bool IsLong(string str)
    {
        if (str.Length > 30)
            return true;
        return false;
    }
}