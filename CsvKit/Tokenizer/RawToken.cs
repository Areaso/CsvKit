namespace CsvKit;

internal class RawToken(string value, bool isSeparator)
{
    #region Public
    public string Value { get; } = value;

    public bool IsSeparator { get; } = isSeparator;
    #endregion
}