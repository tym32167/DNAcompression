using System;

namespace DnaCompression.Lib
{
    internal class CountingSort
    {
        public void sort(String[] a, int w, int shift, int n, string[] aux)
        {
            int R = 21;   // extend ASCII alphabet size	

            for (int d = 0; d < w; d++)
            {
                var ind = (shift + d) % w;

                // sort by key-indexed counting on dth character

                // compute frequency counts
                int[] count = new int[R + 1];
                for (int i = 0; i < n; i++)
                    count[GetInd(a[i], ind) + 1]++;

                // compute cumulates
                for (int r = 0; r < R; r++)
                    count[r + 1] += count[r];

                // move data
                for (int i = 0; i < n; i++)
                    aux[count[GetInd(a[i], ind)]++] = a[i];

                // copy back
                for (int i = 0; i < n; i++)
                    a[i] = aux[i];
            }
        }


        private static int GetInd(string s, int p)
        {
            if (s == null) return 20;
            var c = s[p];

            switch (c)
            {
                case 'A': return 1;
                case 'C': return 2;
                case 'G': return 3;
                case 'T': return 4;

                case 'N': return 15;
                case 'V': return 14;
                case 'H': return 13;
                case 'D': return 12;
                case 'B': return 11;
                case 'Y': return 10;
                case 'M': return 9;
                case 'K': return 8;
                case 'S': return 7;
                case 'W': return 6;
                case 'R': return 5;
            }

            return 0;
        }

    }
}