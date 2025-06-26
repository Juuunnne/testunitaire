using Processor.Contracts;
using System.Text.RegularExpressions;

namespace Processor.Services;

public class StringProcessor : IStringProcessor
{
    public string Reverse(string input)
    {
        string reversed = "";
        for (int i = input.Length -1; i >= 0; i--)
        {
            reversed += input[i];
        }
        
        return reversed;
    }

    public bool IsPalindrome(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException("Le contenu ne peut pas être vide ou nul");
        }

        if (input.Trim().Length == 0)
        {
            return false;
        }

        string reversed = Reverse(input);
        return input.Replace(" ", "").ToLower() == reversed.Replace(" ", "").ToLower();
    }

    public int CountWords(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        string cleanedInput = Regex.Replace(input, @"[^\w\s]", "");
        string[] words = cleanedInput.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    public string Capitalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException("Input cannot be null or empty");

        input = input.Trim();

        return char.ToUpper(input[0]) + input.Substring(1);
    }
}