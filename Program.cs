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

    public static (string processedString, Dictionary<char, int> charCount) ProcessString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return (input, null);
        }

        var invalidCharacters = InvalidCharsCheck(input);
        if (invalidCharacters.Count > 0)
        {
            return ("Ошибка! Введены неподходящие символы: " + string.Join(", ", invalidCharacters), null);
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

        return (processedString, charCount);
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите строку:");
        string userInput = Console.ReadLine();
        var (result, charCount) = ProcessString(userInput);

        Console.WriteLine("Результат: " + result);

        if (charCount != null)
        {
            Console.WriteLine("Количество повторений символов:");
            foreach (var kvp in charCount)
            {
                Console.WriteLine($"'{kvp.Key}': {kvp.Value}");
            }
        }

        Console.ReadLine();
    }
}
