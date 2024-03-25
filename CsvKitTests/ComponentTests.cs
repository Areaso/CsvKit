using CsvKit;

namespace CsvKitTests;

[TestClass]
public class ComponentTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var actual = parser.Run("a");

        var expected = new List<List<string>> {
            new() { "a" }
        };
        
        Assert.AreEqual(actual.Count, expected.Count);
        for (var i = 0; i < actual.Count; i++) {
            CollectionAssert.AreEqual(actual[i], expected[i]);
        }
    }
}