using CsvKit;

namespace CsvKitTests;

[TestClass]
public class ComponentTests
{
    public static IEnumerable<object[]> AdditionData
    {
        get
        {
            return new[]
            { 
                new object[] { "a", new List<List<string>> { new() { "a" } } },
                new object[] { "a;b", new List<List<string>> { new() { "a", "b" } } },
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(AdditionData))]
    public void Test0(string source, List<List<string>> expected)
    {
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var status = parser.Run(source);

        if (status.IsSuccess()) {
            var actual = parser.ResultsData;
            
            Assert.AreEqual(actual.Count, expected.Count);
            for (var i = 0; i < actual.Count; i++) {
                CollectionAssert.AreEqual(actual[i], expected[i]);
            }
        }
        else {
            Assert.Fail(status.Description);
        }
    }

    [TestMethod]
    public void Test1()
    {
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var status = parser.Run("a");

        if (status.IsSuccess()) {
            var actual = parser.ResultsData;

            var expected = new List<List<string>> {
                new() { "a" }
            };

            Assert.AreEqual(actual.Count, expected.Count);
            for (var i = 0; i < actual.Count; i++) {
                CollectionAssert.AreEqual(actual[i], expected[i]);
            }
        }
        else {
            Assert.Fail(status.Description);
        }
    }

    [TestMethod]
    public void Test2()
    {
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var status = parser.Run("\"\"");

        if (status.IsSuccess()) {
            var actual = parser.ResultsData;
            List<List<string>> expected = [];

            Assert.AreEqual(actual.Count, expected.Count);
            for (var i = 0; i < actual.Count; i++) {
                CollectionAssert.AreEqual(actual[i], expected[i]);
            }
        }
        else {
            Assert.Fail(status.Description);
        }
    }
}