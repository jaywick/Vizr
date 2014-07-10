using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr
{
    public interface IEntryHandler
    {
        // visual equivalent: pressing tab on item
        string Preview(EntryBase entry);

        // visual equivalent: pressing enter on item
        ExecutionResult Execute(EntryBase entry);
    }

    public enum ExecutionResult { Success, Failed }
}
