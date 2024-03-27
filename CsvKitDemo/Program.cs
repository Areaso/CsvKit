using System.Diagnostics;
using System.Globalization;
using CsvKit;
using TextTableBuilder;

namespace CsvKitDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        _ = args;
        
        var sourceData = DemoData.RandomRows(100000);

        var s = new Stopwatch();
        s.Start();
        
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var result = parser.Run(sourceData);
        
        s.Stop();

        Console.WriteLine($"Processed in {s.Elapsed.TotalMicroseconds} µs");
        Console.WriteLine($"Processed in {s.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine("");

        // if (result.IsSuccess() && parser.ResultsData?.Count > 0) {
        //     var table1 = new Table();
        //     for (var i = 0; i < parser.ResultsData[0].Count; i++) {
        //         table1.AddColumn(i.ToString());
        //     }
        //     
        //     foreach (var row in parser.ResultsData) {
        //         table1.AddRow(row.ToArray());
        //     }
        //     
        //     var tableBuilder = new TableBuilder();
        //     Console.WriteLine(tableBuilder.Build(table1, new CultureInfo("de_DE")));
        // }
        // else {
        //     Console.WriteLine($"Error: {result.Description}");
        // }
    }
}