using System;

public class StringProcessor
{
    public static string ProcessString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (input.Length % 2 != 0)
        {
            // Нечетное количество символов
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            string reversedString = new string(charArray);
            return reversedString + input;
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

            return reversedFirstHalf + reversedSecondHalf;
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите строку:");
        string userInput = Console.ReadLine();
        string result = ProcessString(userInput);
        Console.WriteLine("Результат: " + result);
        Console.ReadKey();
    }
}