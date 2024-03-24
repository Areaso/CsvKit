namespace CsvKit;

internal class RawTokenList
{
    #region Public
    public List<RawToken> Items { get; } = [];

    public void AddToken(string value, bool isSeparator)
    {
        if (value != string.Empty) {
            Items.Add(new(value, isSeparator));
        }
    }

    public List<string> ToStringList()
    {
        return Items.Select(token => token.Value).ToList();;
    }
    #endregion
}
