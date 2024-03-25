namespace CsvKit;

public sealed record Result()
{
    #region Static
    public static Result Success()
    {
        return new(true, "");
    }

    public static Result Failure(string description)
    {
        return new(false, description);
    }
    #endregion

    #region Public
    public string Description { get; } = string.Empty;

    public bool IsSuccess()
    {
        return _success;
    }

    public bool IsFailure()
    {
        return !_success;
    }
    #endregion

    #region Private
    private readonly bool _success;

    private Result(bool success, string description) : this()
    {
        _success = success;
        Description = description;
    }
    #endregion
}