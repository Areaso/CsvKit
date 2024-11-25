#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace CsvKit;

public static class EnumExtensions
{
    #region Public
    public static string ToStringFast(this FieldSeparators myEnum)
    {
        return myEnum switch {
            FieldSeparators.Comma => ",",
            FieldSeparators.Semicolon => ";",
            FieldSeparators.Tabulator => "\t"
        };
    }

    public static string ToStringFast(this LineSeparators myEnum)
    {
        return myEnum switch {
            LineSeparators.Native => Environment.NewLine,
            LineSeparators.Unix => "\n",
            LineSeparators.Windows => "\r\n"
        };
    }

    public static string ToStringFast(this QuoteSeparators myEnum)
    {
        return myEnum switch {
            QuoteSeparators.SingleQuote => "'",
            QuoteSeparators.DoubleQuote => "\""
        };
    }
    #endregion
}