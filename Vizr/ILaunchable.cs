using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr
{
    interface ILaunchable
    {
        string Application { get; set; }
        bool Admin { get; set; }
        void Launch(string originalQuery);
    }
}
