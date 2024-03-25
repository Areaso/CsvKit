// ReSharper disable MemberCanBePrivate.Global

using System.Diagnostics.CodeAnalysis;

namespace CsvKit;

public class CsvParser(FieldSeparators fieldSep, LineSeparators lineSep, QuoteSeparators quoteSep)
{
    #region Public
    public FieldSeparators FieldSeparator { get; } = fieldSep;

    public LineSeparators LineSeparator { get; } = lineSep;

    public QuoteSeparators QuoteSeparator { get; } = quoteSep;

    public List<List<string>>? Result { get; private set; }

    public bool Error { get; private set; }

    public string ErrorMessage { get; private set; } = string.Empty;

    public bool Run(string source)
    {
        Error = false;
        ErrorMessage = string.Empty;
        
        var tokenizer = new Tokenizer(FieldSeparator.ToStringFast(), LineSeparator.ToStringFast(),
            QuoteSeparator.ToStringFast());

        var parsedResult = tokenizer.Parse(source);

        if (parsedResult.Error) {
            Result = null;
            Error = true;
            ErrorMessage = parsedResult.ErrorMessage;
        }
        else {
            Result = ResultList(parsedResult);
        }

        return parsedResult.Error;
    }
    #endregion

    #region Private
    [SuppressMessage("Performance", "CA1822:Member als statisch markieren")]

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private List<List<string>> ResultList(TokenList tokenList)
    {
        var result = new List<List<string>>();

        var rowData = new List<string>();
        for (var i = 0; i < tokenList.Count(); i++) {
            var token = tokenList[i];

            switch (token.Type) {
                case TokenTypes.StringValue:
                    rowData.Add(token.Value);
                    break;

                case TokenTypes.FieldSeparator:
                    continue;

                case TokenTypes.LineSeparator:
                    result.Add(rowData);
                    rowData.Clear();
                    break;
            }
        }

        if (rowData.Count > 0) {
            result.Add(rowData);
        }

        return result;
    }
    #endregion
}