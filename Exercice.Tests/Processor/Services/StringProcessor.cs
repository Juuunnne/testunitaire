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
        throw new NotImplementedException();
    }

    public int CountWords(string input)
    {
        throw new NotImplementedException();
    }

    public string Capitalize(string input)
    {
        throw new NotImplementedException();
    }
}