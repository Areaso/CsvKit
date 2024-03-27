using CsvKit;

namespace CsvKitTests;

[TestClass]
public class ComponentTests
{
    public static IEnumerable<object[]> TestData {
        get {
            return new[] {
                new object[] { "", null },
                new object[] { "a", new List<List<string>> { new() { "a" } } },
                new object[] { "a;b", new List<List<string>> { new() { "a", "b" } } },
                new object[] { "\"", new List<List<string>> { new() } },
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void Test0(string source, List<List<string>> expected)
    {
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix, QuoteSeparators.DoubleQuote);
        var status = parser.Run(source);

        if (status.IsSuccess()) {
            var actual = parser.ResultsData;

            if (actual == null && expected == null) {
                return;
            }

            if (actual == null && expected != null) {
                Assert.Fail($"Actual is null, but expected is {expected}");
            }

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