using System.Diagnostics;

namespace CsvKit;

internal static class Program
{
    private static void Main(string[] args)
    {
        var data = DemoData.RandomRows(10000);
        
        var s = new Stopwatch();
        s.Start();

        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var result = parser.Run(data);

        s.Stop();
        Console.WriteLine($"Processed in {s.ElapsedMilliseconds} ms");
        Console.WriteLine("");

        // foreach (var row in result) {
        //     foreach (var field in row) {
        //         Console.Write($"{field}\t");
        //     }
        //
        //     Console.WriteLine();
        // }
    }
}