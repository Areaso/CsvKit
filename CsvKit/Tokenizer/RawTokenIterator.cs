using System.Reflection;

namespace CsvKit;

internal class RawTokenIterator(IReadOnlyList<RawToken> value)
{
    #region Public
    public RawToken NextValue()
    {
        _position++;

        return _position >= _length
            ? throw new ArgumentOutOfRangeException(nameof(_position), $"in {MethodBase.GetCurrentMethod()?.Name}")
            : value[_position];
    }

    public char PreviewNextChar()
    {
        var previewPosition = _position + 1;
        return previewPosition >= _length
            ? '\0'
            : value[previewPosition].Value[0];
    }

    public bool EndOfData()
    {
        return _position + 1 >= _length;
    }
    #endregion

    #region Private
    private int _position = -1;

    private readonly int _length = value.Count;
    #endregion
}