using System;
using System.Collections.Generic;
using System.Linq;

namespace DnaCompression.Lib
{
    public class DnaCompressorValidator
    {
        public ValidationResult Validate(string[] source, string[] compressed)
        {
            var s = source.OrderBy(x => x).ToArray();
            var d = Generate(compressed).OrderBy(x => x).ToArray();

            if (s.Length != d.Length)
            {
                return new ValidationResult(false, $"source len={s.Length}, generated len={d.Length}");
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != d[i]) return new ValidationResult(false, $"difference on index {i}");
            }

            return new ValidationResult(true, string.Empty);
        }

        private static IEnumerable<string> Generate(string[] compressed)
        {
            var state = new Char[compressed[0].Length];
            return compressed.SelectMany(s => Generate(s, 0, state));
        }

        private static Types[] _decom = new[] { Types.A, Types.C, Types.G, Types.T };
        private static IEnumerable<string> Generate(string compressed, int index, char[] state)
        {
            if (index >= compressed.Length) yield return new string(state);
            else
            {
                var curr = compressed[index].GetTypes();

                foreach (var tt in _decom)
                {
                    if (curr == Types.None) break;

                    if ((curr & tt) == tt)
                    {
                        curr &= ~tt;
                        state[index] = tt.GetChar();
                        foreach (var g in Generate(compressed, index + 1, state))
                            yield return g;
                    }
                }
            }
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; }
        public string Message { get; }

        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }
}