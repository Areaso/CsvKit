using System.Reflection;

namespace CoreCSV;

internal class SourceIterator(string value)
{
    #region Public
    public char NextValue()
    {
        _position++;
        return _position >= _length
            ? throw new ArgumentOutOfRangeException(nameof(_position), $"in {MethodBase.GetCurrentMethod()?.Name}")
            : value[_position];
    }

    public bool EndOfData()
    {
        return _position + 1 >= _length;
    }
    #endregion

    #region Private
    private int _position = -1;

    private readonly int _length = value.Length;
    #endregion
}