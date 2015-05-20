using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class GenericScorer : IResultScorer
    {
        private History _history = new History();

        public IEnumerable<ScoredResult> Score(string queryText, IEnumerable<IResult> results)
        {
            var scoredResults = new List<ScoredResult>();

            if (queryText == "")
            {
                var maxRecentResults = 10;
                return _history.Items
                    .GroupBy(x => x.ToString())
                    .OrderByDescending(g => g.Count())
                    .Take(maxRecentResults)
                    .Select(g => g.Key)
                    .Select((h, i) => new ScoredResult(maxRecentResults - i, results.First(r => r.ID == h)));
            }

            foreach (IResult result in results)
            {
                int score = result.SearchableText
                    .Select(x => new { Score = Score(x.Text, queryText), Weight = x.Weight })
                    .Sum(x => x.Score * x.Weight);

                scoredResults.Add(new ScoredResult(score, result));
            }

            return scoredResults
                .Where(x => x.Score > 0);
        }

        private int Score(string target, string searchText)
        {
            return (int)EvaluateCriteria(target, searchText).Sum();
        }

        private IEnumerable<double> EvaluateCriteria(string target, string searchText)
        {
            var searchWords = SplitWords(searchText.ToLower());
            var targetWords = SplitWords(target.ToLower());

            // word match
            yield return JoinWhere(targetWords, searchWords, (t, s) => t.StartsWith(s))
                .Select(x => x.Item1)
                .Count() * 3;

            // first letter match
            var targetPartials = String.Join("", targetWords.Select(x => x.Substring(0, 1)));
            var searchPartials = String.Join("", searchText.ToLower().ToCharArray().Select(x => new String(x, 1)));
            yield return targetPartials.Contains(searchPartials) ? searchPartials.Count() : 0;

            // basic starts with
            yield return target.ToLower().StartsWith(searchText.ToLower()) ? searchText.Length * 3 : 0;

            // basic contains (fallback)
            yield return target.ToLower().Contains(searchText.ToLower()) ? searchText.Length * 0.5 : 0;
        }

        /*public static IEnumerable<IEnumerable<string>> GetConsecutiveWordPermutations(string target)
        {
            if (String.IsNullOrWhiteSpace(target))
                yield break;

            var words = SplitByWordsDistinct(target);
            var lastIndex = words.Count() - 1;

            foreach (var skip in Enumerable.Range(0, lastIndex))
            {
                foreach (var take in Enumerable.Range(1, lastIndex))
                {
                    if (take + skip < words.Count())
                        yield return words.Skip(skip).Take(take);
                }
            }
        }*/

        public static IEnumerable<string> SplitWords(string target)
        {
            return Regex
                .Split(target, @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])|(\W)")
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim());

            //return target.Split(new[] { " ", "-", "@", ".", "_", "/", "\\", "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<Tuple<T, T>> JoinWhere<T>(IEnumerable<T> target, IEnumerable<T> second, Func<T, T, bool> predicate)
        {
            var str = string.Join("", target.Cast<string>());
            if ((str).Contains("manager") && str.Contains("android"))
            {
                int i = 1;
                i++;
            }

            foreach (var t in target)
                foreach (var s in second)
                    if (predicate(t, s))
                        yield return Tuple.Create(t, s);
        }
    }
}
