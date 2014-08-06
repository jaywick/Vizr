using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr
{
    interface IEntryModel
    {
        TEntry ToEntry<TEntry>() where TEntry : EntryBase;
    }

    interface IEntryModelList
    {
        List<TEntry> GetAllEntries<TEntry>() where TEntry : EntryBase;
    }
}
