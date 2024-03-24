namespace CoreCSV;

internal class Token(TokenTypes type, string value)
{
    #region Public
    public TokenTypes Type { get; } = type;

    public string Value { get; } = value;

    public bool IsSeparator()
    {
        return Type != TokenTypes.StringValue;
    }

    public bool IsStringValue()
    {
        return Type == TokenTypes.StringValue;
    }
    #endregion
}
