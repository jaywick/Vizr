using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class GenericScorer : IResultScorer
    {
        public IEnumerable<ScoredResult> Score(string queryText, IEnumerable<IResult> results)
        {
            var scoredResults = new List<ScoredResult>();

            foreach (IResult result in results)
            {
                int score = result.SearchableText
                    .Where(x => x.Text.ToLower().StartsWith(queryText.ToLower()))
                    .Sum(x => x.Value);

                scoredResults.Add(new ScoredResult(score, result));
            }

            return scoredResults
                .Where(x => x.Score > 0);
        }
    }
}
