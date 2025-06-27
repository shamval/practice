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

    public static (string processedString, Dictionary<char, int> charCount, string longestSubstring, string sortedString) ProcessString(string input, string sortAlgorithm)
    {
        if (string.IsNullOrEmpty(input))
        {
            return (input, null, null, null);
        }

        var invalidCharacters = InvalidCharsCheck(input);
        if (invalidCharacters.Count > 0)
        {
            return ("Ошибка! Введены неподходящие символы: " + string.Join(", ", invalidCharacters), null, null, null);
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

        // Сортировка
        string sortedString = sortAlgorithm.ToLower() switch
        {
            "quicksort" => QuickSort(processedString),
            "treesort" => TreeSort(processedString),
            _ => "Ошибка! Неверный алгоритм сортировки."
        };

        return (processedString, charCount, longestSubstring, sortedString);
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

    private static string QuickSort(string input)
    {
        if (input.Length <= 1)
            return input;

        char pivot = input[input.Length / 2];
        string less = string.Empty;
        string equal = string.Empty;
        string greater = string.Empty;

        foreach (char c in input)
        {
            if (c < pivot)
                less += c;
            else if (c == pivot)
                equal += c;
            else
                greater += c;
        }

        return QuickSort(less) + equal + QuickSort(greater);
    }

    private static string TreeSort(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var root = new TreeNode(input[0]);

        foreach (char c in input.Substring(1))
        {
            root.Insert(c);
        }

        return root.InOrderTraversal();
    }

    private class TreeNode
    {
        public char Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(char value)
        {
            Value = value;
        }

        public void Insert(char value)
        {
            if (value < Value)
            {
                if (Left == null)
                    Left = new TreeNode(value);
                else
                    Left.Insert(value);
            }
            else
            {
                if (Right == null)
                    Right = new TreeNode(value);
                else
                    Right.Insert(value);
            }
        }

        public string InOrderTraversal()
        {
            var sortedList = new List<char>();
            InOrderTraversal(this, sortedList);
            return new string(sortedList.ToArray());
        }

        private void InOrderTraversal(TreeNode node, List<char> sortedList)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, sortedList);
                sortedList.Add(node.Value);
                InOrderTraversal(node.Right, sortedList);
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите строку:");
        string userInput = Console.ReadLine();

        Console.WriteLine("Выберите алгоритм сортировки (quicksort или treesort):");
        string choice = Console.ReadLine();

        var (result, charCount, longestSubstring, sortedString) = ProcessString(userInput, choice);

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

        Console.WriteLine("Отсортированная строка: " + sortedString);

        Console.ReadLine();
    }
}