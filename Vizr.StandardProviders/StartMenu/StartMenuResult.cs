﻿using System;
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
            Description = file.Directory.FullName;
            Provider = provider;
        }

        public Hash ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IResultProvider Provider { get; set; }

        public IEnumerable<SearchableText> SearchableText
        {
            get
            {
                yield return new SearchableText(1, _file.Name);
            }
        }

        public bool Launch()
        {
            Process.Start(_file.FullName);
            return true;
        }

        public IPreview Preview { get; set; }

        public void Edit()
        {
        }

        public void Delete()
        {
        }
    }
}
