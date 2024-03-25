using System.Diagnostics;
using CsvKit;

namespace CsvKitDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        var data = DemoData.RandomRows(10);
        
        var s = new Stopwatch();
        s.Start();

        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var result = parser.Run("a");

        s.Stop();
        Console.WriteLine($"Processed in {s.Elapsed.Microseconds} µs");
        Console.WriteLine($"Processed in {s.Elapsed.Milliseconds} ms");
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