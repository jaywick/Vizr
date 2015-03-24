using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class ScoredResult
    {
        public ScoredResult(int score, IResult result)
        {
            Score = score;
            Result = result;
        }

        /// <summary>
        /// Relevance score of result after processing it in a IResultScorer
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Result associated with score
        /// </summary>
        public IResult Result { get; set; }
    }
}
