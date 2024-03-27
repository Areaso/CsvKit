// ReSharper disable MemberCanBePrivate.Global

using System.Diagnostics.CodeAnalysis;

namespace CsvKit;

public class CsvParser(FieldSeparators fieldSep, LineSeparators lineSep, QuoteSeparators quoteSep)
{
    #region Public
    public FieldSeparators FieldSeparator { get; } = fieldSep;

    public LineSeparators LineSeparator { get; } = lineSep;

    public QuoteSeparators QuoteSeparator { get; } = quoteSep;

    public List<List<string>>? ResultsData { get; private set; }

    public Result Run(string source)
    {
        ResultsData = null;
        Result status;

        var tokenizer = new Tokenizer(FieldSeparator.ToStringFast(), LineSeparator.ToStringFast(),
            QuoteSeparator.ToStringFast());

        var tokenizerResult = tokenizer.Parse(source);

        if (tokenizerResult.Error) {
            status = Result.Failure(tokenizerResult.ErrorMessage);
        }
        else {
            ResultsData = ResultList(tokenizerResult);
            status = Result.Success();
        }

        return status;
    }
    #endregion

    #region Private
    // ReSharper disable once MemberCanBeMadeStatic.Local
    [SuppressMessage("Performance", "CA1822:Member als statisch markieren")]
    private List<List<string>>? ResultList(TokenList tokenList)
    {
        List<List<string>>? result = null;
        List<string>? rowData = null;

        var lastTokenType = TokenTypes.StringValue;

        for (var i = 0; i < tokenList.Count(); i++) {
            var token = tokenList[i];

            switch (token.Type) {
                case TokenTypes.StringValue:
                    rowData ??= [];
                    rowData.Add(token.Value);
                    break;

                case TokenTypes.FieldSeparator:
                    if (lastTokenType == TokenTypes.FieldSeparator) {
                        rowData ??= [];
                        rowData.Add(string.Empty);
                    }

                    break;

                case TokenTypes.LineSeparator:
                    result ??= [];
                    if (rowData != null) {
                        result.Add(rowData);
                    }

                    rowData = [];
                    break;
            }

            lastTokenType = token.Type;
        }

        if (rowData is { Count: > 0 }) {
            result ??= [];
            result.Add(rowData);
        }

        return result is { Count: > 0 } ? result : null;
    }
    #endregion
}