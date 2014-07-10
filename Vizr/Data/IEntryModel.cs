using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr
{
    interface IEntryModel
    {
        EntryBase ToEntry();
    }

    interface IEntryModelList
    {
        List<EntryBase> GetAllEntries();
    }
}
