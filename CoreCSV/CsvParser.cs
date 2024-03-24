// ReSharper disable MemberCanBePrivate.Global

using System.Diagnostics.CodeAnalysis;

namespace CoreCSV;

public class CsvParser(FieldSeparators fieldSeparator, LineSeparators lineSeparator)
{
    #region Public
    public FieldSeparators FieldSeparator { get; } = fieldSeparator;

    public LineSeparators LineSeparator { get; } = lineSeparator;

    public List<List<string>> Run(string source)
    {
        var interimResult = new Tokenizer(FieldSeparator.ToStringFast(), LineSeparator.ToStringFast());
        return ResultList(interimResult.Parse(source));
    }
    #endregion

    #region Private
    [SuppressMessage("Performance", "CA1822:Member als statisch markieren")]
    private List<List<string>> ResultList(TokenList interimResult)
    {
        var result = new List<List<string>>();
        // var fields = LineResult();
        // parsedData.Add(fields);
        return result;
    }
    #endregion
}
