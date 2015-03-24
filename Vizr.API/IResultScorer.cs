using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public interface IResultScorer
    {
        IEnumerable<ScoredResult> Score(IEnumerable<IResult> results);
    }
}
