namespace CoreCSV;

public static class EnumExtensions
{
    #region Public
    public static string ToStringFast(this FieldSeparators myEnum)
    {
        return myEnum switch {
            FieldSeparators.Comma => ",",
            FieldSeparators.Semicolon => ";",
            FieldSeparators.Tabulator => "\t",
            _ => myEnum.ToString()
        };
    }
    
    public static string ToStringFast(this LineSeparators myEnum)
    {
        return myEnum switch {
            LineSeparators.Native => Environment.NewLine,
            LineSeparators.Unix => "\n",
            LineSeparators.Windows => "\r\n",
            _ => myEnum.ToString()
        };
    }
    #endregion
}
