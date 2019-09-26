using DnaCompression.Lib;
using NUnit.Framework;

namespace DnaCompression.Tests
{
    public class CountingSortTests
    {
        [Test]
        public void SampleTest1()
        {
            var lines = new[] { "AAA", "AAT", "ATA", "CAA", "GAA" };

            var sort = new CountingSort();
            sort.Sort(lines, 3, 0, lines.Length, new string[lines.Length]);

            var expected = new[] { "AAA", "CAA", "GAA", "ATA", "AAT" };
            CollectionAssert.AreEqual(expected, lines);
        }
    }
}