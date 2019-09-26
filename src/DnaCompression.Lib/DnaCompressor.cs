using System;
using System.Text;

namespace DnaCompression.Lib
{
    public class DnaCompressor
    {
        public void Compress(string[] lines, IProgress<int> progress)
        {
            var tmp = new string[lines.Length];

            int n = lines.Length;

            var maxpos = lines[0].Length;

            var sorter = new CountingSort();

            for (var pos = 0; pos < maxpos; pos++)
            {
                sorter.Sort(lines, lines[0].Length, pos, n, tmp);

                var ind = 0;

                while (ind < lines.Length && lines[ind] != null)
                {
                    int start = ind;
                    while (ind < (lines.Length - 1) && lines[ind + 1] != null && IsSame(lines[ind], lines[ind + 1], pos)) ind++;
                    int end = ind;

                    Types t = Types.None;
                    for (int i = start; i <= end; i++)
                        t |= lines[i][pos].GetTypes();

                    var added = 0;
                    foreach (var tt in new[]
                    {
                        Types.N, Types.V, Types.H, Types.D, Types.B, Types.Y, Types.M, Types.K, Types.S, Types.W,
                        Types.R, Types.A, Types.C, Types.G, Types.T
                    })
                    {
                        if (t == Types.None) break;

                        if ((t & tt) == tt)
                        {
                            t &= ~tt;
                            var sb = new StringBuilder(lines[start]) { [pos] = tt.GetChar() };
                            lines[start] = sb.ToString();
                            added++;
                        }
                    }

                    ind++;
                    for (var i = start + added; i <= end; i++)
                        lines[i] = null;
                }

                n = Scroll(lines, n);

                progress.Report((100 * pos) / maxpos);
            }

            progress.Report(100);
        }

        internal static int Scroll(string[] lines, int n)
        {
            int startInd = 0; int endInd = n - 1;
            while (startInd < endInd)
            {
                while (startInd < endInd && lines[startInd] != null) startInd++;
                while (startInd < endInd && lines[endInd] == null) endInd--;
                if (startInd < endInd)
                {

                    lines[startInd] = lines[endInd];
                    lines[endInd] = null;
                }

            }
            n = endInd + 1;
            return n;
        }

        internal static bool IsSame(string s1, string s2, int except)
        {
            for (var i = 0; i < s1.Length; i++)
            {
                if (i == except) continue;
                if (s1[i] != s2[i]) return false;
            }
            return true;
        }
    }
}