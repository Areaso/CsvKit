namespace CoreCSV;

internal class TokenList
{
    #region Public
    // ReSharper disable once MemberCanBePrivate.Global
    public bool Error { get; private set; }

    public string ErrorMessage {
        get => $"{_errorMessage} : near({string.Join("", ToStringList())})";
        private set => _errorMessage = value;
    }

    public void AddToken(TokenTypes type, string value)
    {
        if (Error) {
            return;
        }

        if (type == TokenTypes.StringValue) {
            if (IsLastItemStringValue()) {
                ErrorOccured($"Missing separator between '{Item[^1].Value}' and '{value}'");
            }
        }

        if (value != string.Empty) {
            Item.Add(new(type, value.Trim()));
        }
    }

    public void ErrorOccured(string message)
    {
        Error = true;
        ErrorMessage = message;
    }

    public bool IsLastItemSeparator()
    {
        return Item.Count == 0 || Item[^1].IsSeparator();
    }

    public bool IsLastItemStringValue()
    {
        return Item.Count > 0 && Item[^1].IsStringValue();
    }

    public List<string> ToStringList()
    {
        return Item.Select(token => token.Value).ToList();
    }
    #endregion

    #region Private
    private List<Token> Item { get; } = [];

    private string _errorMessage = string.Empty;
    #endregion
}
