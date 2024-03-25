namespace CsvKit;

public sealed record Error(string Code, string Description)
{
    #region Public
    public static readonly Error None = new(string.Empty, string.Empty);
    #endregion
}