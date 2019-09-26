using System;

namespace DnaCompression.Lib
{
    [Flags]
    public enum Types
    {
        None = 0,
        A = 1,
        C = 1 << 1,
        G = 1 << 2,
        T = 1 << 3,

        N = A | C | G | T,
        V = A | C | G,
        H = A | C | T,
        D = A | G | T,
        B = C | G | T,
        Y = T | C,
        M = A | C,
        K = G | T,
        S = G | C,
        W = A | T,
        R = A | G
    }
}