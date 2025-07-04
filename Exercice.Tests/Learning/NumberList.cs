namespace Learning;

public class NumberList : INumberList
{
    private List<int> _numbers = new List<int>();

    public void Add(int number)
    {
        _numbers.Add(number);
    }

    public bool Remove(int number)
    {
        if (_numbers.Contains(number))
        {
            _numbers.Remove(number);
            return true;
        }
        return false;
    }

    public void Clear()
    {
        _numbers.Clear();
    }

    public int Count()
    {
        return _numbers.Count;
    }

    public int Sum()
    {
        if (_numbers.Count == 0)
            return 0;

        return _numbers.Sum();
    }

    public int Max()
    {
        if (_numbers.Count == 0)
            return 0;
        return _numbers.Max();
    }

    public int Min()
    {
        if (_numbers.Count == 0)
            return 0;
        return _numbers.Min();
    }

    public double Average()
    {
        if (_numbers.Count == 0)
            return 0.0;
        return _numbers.Average();
    }

    public bool Contains(int number)
    {
        return _numbers.Contains(number);
    }
}