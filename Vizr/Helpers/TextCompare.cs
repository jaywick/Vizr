using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr
{
    static class TextCompare
    {
        public static List<Criterion> Criteria { get; set; }

        static TextCompare()
        {
            Criteria = new List<Criterion>
            {
                new Criterion(100, (a, b) => a == b),
                new Criterion(90, (a, b) => b.StartsWith(a)),
                new Criterion(90, (a, b) => b.ContainsPartialsOf(a)),
                new Criterion(1, (a, b) => b.Contains(a)),
            };
        }

        public static int Score(string input, string existing)
        {
            var normalisedInput = input.ToLower().Trim();
            var normalisedExisting = existing.ToLower().Trim();

            return Criteria.Where(c => c.Comparison(normalisedInput, normalisedExisting))
                           .Sum(c => c.Score);
        }

        public static int Score(string input, params string[] existing)
        {
            return existing.Where(e => !e.IsNullOrEmpty())
                           .Select(e => Score(input, e))
                           .Sum();
        }

        public class Criterion
        {
            public int Score { get; set; }
            public Func<string, string, bool> Comparison { get; set; }

            public Criterion (int score, Func<string, string, bool> comparison)
	        {
                Comparison = comparison;
                Score = score;
	        }
        }
    }
}
