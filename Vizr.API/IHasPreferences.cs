using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr.API
{
    public interface IHasPreferences<TPrefs> where TPrefs : IProviderPreferences
    {
        TPrefs Preferences { get; set; }
    }
}
