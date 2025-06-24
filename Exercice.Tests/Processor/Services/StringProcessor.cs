using Processor.Contracts;

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
        string reversed = Reverse(input);
        return input.Replace(" ", "").ToLower() == reversed.Replace(" ", "").ToLower();
    }

    public int CountWords(string input)
    {
        throw new NotImplementedException();
    }

    public string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException("Input cannot be null or empty");
        if (int.TryParse(input, out int number))
            throw new ArgumentException("Input cannot be a number");
        return input.ToUpper();
    }
}