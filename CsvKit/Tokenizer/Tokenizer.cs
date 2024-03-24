using System.Text;

namespace CsvKit;

internal class Tokenizer(string fieldSeparator, string lineSeparator, string quoteSeparator)
{
    #region Public
    public TokenList Parse(string input)
    {
        var rawResult = TokenizeRaw(input);

#if DEBUG
        Console.WriteLine("Pass 1:");
        foreach (var outputLine in rawResult.ToStringList()) {
            Console.WriteLine($"\t|{outputLine}");
        }
#endif

        var result = TokenizeFinal(rawResult);

#if DEBUG
        Console.WriteLine("Pass 2:");
        if (result.Error) {
            Console.WriteLine(result.ErrorMessage);
        }
        else {
            foreach (var outputLine in result.ToStringList()) {
                Console.WriteLine($"\t|{outputLine}");
            }
        }
#endif

        return result;
    }
    #endregion

    #region Private
    private readonly char _fieldSep = fieldSeparator[0];

    private readonly char _lineSep = lineSeparator[0];

    private readonly char _quoteSep = quoteSeparator[0];

    private RawTokenList TokenizeRaw(string input)
    {
        var source = new SourceIterator(input);
        var rawResult = new RawTokenList();
        var rawToken = new StringBuilder();

        while (!source.EndOfData()) {
            var @char = source.NextValue();

            if (@char == _quoteSep || @char == _fieldSep || @char == _lineSep) {
                if (rawToken.Length > 0) {
                    rawResult.AddToken(rawToken.ToString(), false);
                    rawToken.Clear();
                }

                rawResult.AddToken(@char.ToString(), true);
                continue;
            }

            rawToken.Append(@char);
        }

        if (rawToken.Length > 0) {
            rawResult.AddToken(rawToken.ToString(), false);
        }

        return rawResult;
    }

    private TokenList TokenizeFinal(RawTokenList rawTokenList)
    {
        var source = new RawTokenIterator(rawTokenList.Items);
        var result = new TokenList();
        RawToken rawToken;

        while (!source.EndOfData()) {
            rawToken = source.NextValue();

            if (rawToken.IsSeparator) {
                HandleSeparators();
                if (result.Error) {
                    return result;
                }
            }
            else {
                result.AddToken(TokenTypes.StringValue, rawToken.Value);
            }
        }

        return result;

        void HandleSeparators()
        {
            RawToken? preview;

            switch (rawToken.Value) {
                case { } s when s == quoteSeparator:
                    if (result.IsLastItemSeparator()) {
                        result.AddToken(TokenTypes.StringValue, GetQuotedString());
                        break;
                    }

                    result.ErrorOccured($"Unexpected token '{quoteSeparator}'");
                    return;

                case { } s when s == fieldSeparator:
                    result.AddToken(TokenTypes.FieldSeparator, fieldSeparator);
                    break;

                case { } s when s == lineSeparator:
                    result.AddToken(TokenTypes.LineSeparator, lineSeparator);
                    break;
            }
        }

        string GetQuotedString()
        {
            var quotedString = new StringBuilder();
            var countQuotes = 1;

            while (!source.EndOfData()) {
                var token = source.NextValue();
                if (token.Value != quoteSeparator) {
                    quotedString.Append(token.Value);
                    continue;
                }

                countQuotes++;

                if (source.PreviewNextValue() is { } s && s.Value == quoteSeparator) {
                    quotedString.Append(quoteSeparator[0]);
                    source.NextValue();
                    countQuotes++;
                    continue;
                }

                break;
            }

            if (countQuotes % 2 != 0) {
                result.ErrorOccured($"Unterminated string literal '{quotedString}'");
            }

            return quotedString.ToString();
        }
    }
    #endregion
}
