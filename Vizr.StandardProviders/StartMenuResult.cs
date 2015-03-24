using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API;
using Vizr.StandardProviders.Extensions;

namespace Vizr.StandardProviders
{
    public class StartMenuResult : IResult
    {
        private FileInfo _file;

        public StartMenuResult(IResultProvider provider, FileInfo file)
        {
            _file = file;

            ID = Hash.CreateFrom(file.FullName);
            Title = file.GetNameWithoutExtension();
            Provider = provider;
        }

        public Hash ID { get; set; }

        public string Title { get; set; }

        public IResultProvider Provider { get; set; }

        public void Launch()
        {
            Process.Start(_file.FullName);
        }

        public void Options()
        {
        }

        public void Edit()
        {
        }

        public void Delete()
        {
        }
    }
}
