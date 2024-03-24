namespace CoreCSV;

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
        var result = new List<string>();

        foreach (var token in Items) {
            result.Add(token.Value);
        }

        return result;
    }
    #endregion
}
