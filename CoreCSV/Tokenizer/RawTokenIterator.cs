using System.Reflection;

namespace CoreCSV;

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

    public RawToken? PreviewNextValue()
    {
        var previewPosition = _position + 1;
        return previewPosition >= _length
            ? null
            : value[previewPosition];
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