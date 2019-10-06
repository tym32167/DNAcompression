namespace DnaCompression.Lib
{
    public class CountingSort
    {
        public void Sort(string[] a, int w, int shift, int n, string[] aux)
        {
            int R = 21;

            for (int d = 0; d < w; d++)
            {
                var ind = (shift + d) % w;

                int[] count = new int[R + 1];
                for (int i = 0; i < n; i++)
                    count[GetInd(a[i], ind) + 1]++;

                for (int r = 0; r < R; r++)
                    count[r + 1] += count[r];

                for (int i = 0; i < n; i++)
                    aux[count[GetInd(a[i], ind)]++] = a[i];

                for (int i = 0; i < n; i++)
                    a[i] = aux[i];
            }
        }

        public void SortOnce(string[] a, int w, int d, int n, string[] aux)
        {
            int R = 21;

            var ind = d % w;

            int[] count = new int[R + 1];
            for (int i = 0; i < n; i++)
                count[GetInd(a[i], ind) + 1]++;

            for (int r = 0; r < R; r++)
                count[r + 1] += count[r];

            for (int i = 0; i < n; i++)
                aux[count[GetInd(a[i], ind)]++] = a[i];

            for (int i = 0; i < n; i++)
                a[i] = aux[i];
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