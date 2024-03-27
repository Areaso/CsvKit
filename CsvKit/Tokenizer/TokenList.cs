namespace CsvKit;

internal class TokenList
{
    #region Public
    public bool Error { get; private set; }
    
    public string ErrorMessage {
        get => $"{_errorMessage} : near({string.Join("", ToStringList())})";
        private set => _errorMessage = value;
    }

    public Token this[int index] {
        get { return TokenItems[index]; }
    }
    
    public void AddToken(TokenTypes type, string value)
    {
        if (Error) {
            return;
        }

        if (type == TokenTypes.StringValue) {
            // Is last item a StringValue?
            if (TokenItems.Count > 0 && TokenItems[^1].IsStringValue()) {
                ErrorOccured($"Missing separator between '{TokenItems[^1].Value}' and '{value}'");
            }
        }

        if (value != string.Empty) {
            TokenItems.Add(new(type, value.Trim()));
        }
    }

    public int Count()
    {
        return TokenItems.Count;
    }

    public void ErrorOccured(string message)
    {
        Error = true;
        ErrorMessage = message;
    }

    public bool IsLastItemSeparator()
    {
        return TokenItems.Count == 0 || TokenItems[^1].IsSeparator();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public IEnumerable<string> ToStringList()
    {
        return TokenItems.Select(token => token.Value).ToList();
    }
    #endregion

    #region Private
    private List<Token> TokenItems { get; } = [];

    private string? _errorMessage;
    #endregion
}
