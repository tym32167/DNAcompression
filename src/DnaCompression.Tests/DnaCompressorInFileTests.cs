using DnaCompression.Lib;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DnaCompression.Tests
{
    [TestFixture]
    public class DnaCompressorInFileTests
    {
        [Test]
        public void SampleTest1()
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\94x10_input.txt"));

            var compressor = new DnaCompressor();
            var expected = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\94x10_output_14.txt"));

            compressor.Compress(lines);

            CollectionAssert.AreEquivalent(expected, lines.Where(l => l != null));
        }

        [Test]
        public void SampleTest2()
        {
            var lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\615x10_input.txt"));

            var compressor = new DnaCompressor();
            var expected = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\615x10_output_17.txt"));

            compressor.Compress(lines);

            CollectionAssert.AreEquivalent(expected, lines.Where(l => l != null));
        }
    }
}