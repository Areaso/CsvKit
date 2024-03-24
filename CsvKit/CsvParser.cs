// ReSharper disable MemberCanBePrivate.Global

using System.Diagnostics.CodeAnalysis;

namespace CsvKit;

public class CsvParser(FieldSeparators fieldSeparator, LineSeparators lineSeparator)
{
    #region Public
    public FieldSeparators FieldSeparator { get; } = fieldSeparator;

    public LineSeparators LineSeparator { get; } = lineSeparator;

    public List<List<string>> Run(string source)
    {
        var tokenizer = new Tokenizer(FieldSeparator.ToStringFast(), LineSeparator.ToStringFast());
        return ResultList(tokenizer.Parse(source));
    }
    #endregion

    #region Private
    [SuppressMessage("Performance", "CA1822:Member als statisch markieren")]

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private List<List<string>> ResultList(TokenList tokenList)
    {
        var result = new List<List<string>>();

        var lineData = new List<string>();
        for (var i = 0; i < tokenList.Count(); i++) {
            var token = tokenList[i];

            switch (token.Type) {
                case TokenTypes.StringValue:
                    lineData.Add(token.Value);
                    break;

                case TokenTypes.FieldSeparator:
                    continue;

                case TokenTypes.LineSeparator:
                    result.Add(lineData);
                    lineData = [];
                    break;
            }
        }

        if (result.Count > 0) {
            result.Add(lineData);
        }

        return result;
    }
    #endregion
}
