using DnaCompression.Lib;
using NUnit.Framework;
using System;

namespace DnaCompression.Tests
{
    [TestFixture]
    public class DnaCompressorInMemoryTests
    {
        private readonly IProgress<int> _progress = new Progress<int>();

        [Test]
        public void SampleTest1()
        {
            var lines = new[] { "AA", "AC", "AT", "AG", "CA", "CC", "CT", "CG", "TA", "TC", "TT", };
            var compressor = new DnaCompressor();
            var expected = new[] { "MG", "HH", null, null, null, null, null, null, null, null, null };

            compressor.Compress(lines, _progress);

            CollectionAssert.AreEquivalent(expected, lines);
        }


        [Test]
        public void SampleTest2()
        {
            var lines = new[] { "CA", "AC", "AT", "CA", "CC", "CT", "TA", "TC", "TT", };
            var compressor = new DnaCompressor();
            var expected = new[] { "YA", "HY", null, null, null, null, null, null, null };

            compressor.Compress(lines, _progress);

            CollectionAssert.AreEquivalent(expected, lines);
        }

        [Test]
        public void SampleTest3()
        {
            var lines = new[] { "AA", "AC", "AT", "CA", "CC", "CT", "TA", "TC", "TT", };
            var compressor = new DnaCompressor();
            var expected = new[] { "HH", null, null, null, null, null, null, null, null };

            compressor.Compress(lines, _progress);

            CollectionAssert.AreEquivalent(expected, lines);
        }

        [Test]
        public void SampleTest4()
        {
            var lines = new[] { "AAGCTT", "AAGCTC", "GAGCTT" };
            var compressor = new DnaCompressor();
            var expected = new[] { "AAGCTC", "RAGCTT", null };

            compressor.Compress(lines, _progress);

            CollectionAssert.AreEquivalent(expected, lines);
        }
    }
}