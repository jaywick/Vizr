using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class GenericScorer : IResultScorer
    {
        public IEnumerable<ScoredResult> Score(IEnumerable<IResult> results)
        {
            return results.Select(x => new ScoredResult(1, x));
        }
    }
}
