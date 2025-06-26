using System;
using System.Collections.Generic;

public class StringProcessor
{
    public static List<char> InvalidCharsCheck(string input)
    {
        var invalidChars = new List<char>();
        foreach (char c in input)
        {
            if (c < 'a' || c > 'z')
            {
                invalidChars.Add(c);
            }
        }
        return invalidChars;
    }

    public static (string processedString, Dictionary<char, int> charCount, string longestSubstring) ProcessString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return (input, null, null);
        }

        var invalidCharacters = InvalidCharsCheck(input);
        if (invalidCharacters.Count > 0)
        {
            return ("Ошибка! Введены неподходящие символы: " + string.Join(", ", invalidCharacters), null, null);
        }

        string processedString;
        if (input.Length % 2 != 0)
        {
            // Нечетное количество символов
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            processedString = new string(charArray) + input;
        }
        else
        {
            // Четное количество символов
            int halfLength = input.Length / 2;
            string firstHalf = input.Substring(0, halfLength);
            string secondHalf = input.Substring(halfLength);

            char[] firstHalfArray = firstHalf.ToCharArray();
            Array.Reverse(firstHalfArray);
            string reversedFirstHalf = new string(firstHalfArray);

            char[] secondHalfArray = secondHalf.ToCharArray();
            Array.Reverse(secondHalfArray);
            string reversedSecondHalf = new string(secondHalfArray);

            processedString = reversedFirstHalf + reversedSecondHalf;
        }

        // Подсчет символов
        var charCount = new Dictionary<char, int>();
        foreach (char c in processedString)
        {
            if (charCount.ContainsKey(c))
            {
                charCount[c]++;
            }
            else
            {
                charCount[c] = 1;
            }
        }

        // Поиск наибольшей подстроки
        string longestSubstring = FindLongestSubstring(processedString);

        return (processedString, charCount, longestSubstring);
    }

    private static string FindLongestSubstring(string input)
    {
        string vowels = "aeiouy";
        string longest = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = i + 1; j < input.Length; j++)
            {
                if (vowels.Contains(input[i]) && vowels.Contains(input[j]))
                {
                    string substring = input.Substring(i, j - i + 1);
                    if (substring.Length > longest.Length)
                    {
                        longest = substring;
                    }
                }
            }
        }

        return longest;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите строку:");
        string userInput = Console.ReadLine();
        var (result, charCount, longestSubstring) = ProcessString(userInput);

        Console.WriteLine("Результат: " + result);

        if (charCount != null)
        {
            Console.WriteLine("Количество повторений символов:");
            foreach (var kvp in charCount)
            {
                Console.WriteLine($"'{kvp.Key}': {kvp.Value}");
            }
        }

        if (longestSubstring != null)
        {
            Console.WriteLine("Наибольшая подстрока, начинающаяся и заканчивающаяся на гласную: " + longestSubstring);
        }

        Console.ReadLine();
    }
}
