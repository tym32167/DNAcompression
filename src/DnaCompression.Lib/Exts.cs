namespace DnaCompression.Lib
{
    internal static class Exts
    {
        public static char GetChar(this Types t)
        {
            switch (t)
            {
                case Types.A: return 'A';
                case Types.C: return 'C';
                case Types.G: return 'G';
                case Types.T: return 'T';
                case Types.N: return 'N';
                case Types.V: return 'V';
                case Types.H: return 'H';
                case Types.D: return 'D';
                case Types.B: return 'B';
                case Types.Y: return 'Y';
                case Types.M: return 'M';
                case Types.K: return 'K';
                case Types.S: return 'S';
                case Types.W: return 'W';
                case Types.R: return 'R';
                default: return '?';
            }
        }

        public static Types GetTypes(this char c)
        {
            switch (c)
            {
                case 'A': return Types.A;
                case 'C': return Types.C;
                case 'G': return Types.G;
                case 'T': return Types.T;
                case 'N': return Types.N;
                case 'V': return Types.V;
                case 'H': return Types.H;
                case 'D': return Types.D;
                case 'B': return Types.B;
                case 'Y': return Types.Y;
                case 'M': return Types.M;
                case 'K': return Types.K;
                case 'S': return Types.S;
                case 'W': return Types.W;
                case 'R': return Types.R;
                default: return Types.NONE;
            }
        }
    }
}