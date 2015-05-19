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
    public class ActionResult : IResult
    {
        private ActionItem ActionItem { get; set; }

        public ActionResult(IResultProvider provider, ActionItem actionItem)
        {
            ActionItem = actionItem;
            Provider = provider;
            
            ID = Hash.CreateFrom(provider.UniqueName, actionItem.Application, actionItem.Path);
            Title = ActionItem.Title;
        }

        public Hash ID { get; set; }

        public string Title { get; set; }

        public IResultProvider Provider { get; set; }

        public IEnumerable<SearchableText> SearchableText
        {
            get
            {
                yield return new SearchableText(10, ActionItem.Title);

                foreach (var tag in ActionItem.Tags)
                    yield return new SearchableText(8, tag);

                yield return new SearchableText(1, ActionItem.Path);
                yield return new SearchableText(1, ActionItem.Application);
            }
        }

        public bool Launch()
        {
            try
            {
                var info = new ProcessStartInfo(ActionItem.Path);

                if (!String.IsNullOrWhiteSpace(ActionItem.Application))
                {
                    info.FileName = ActionItem.Application;
                    info.Arguments = ActionItem.Path;
                }

                if (ActionItem.RunAsAdmin)
                {
                    info.Verb = "runas";

                    if (String.IsNullOrWhiteSpace(ActionItem.Application))
                    {
                        info.FileName = "explorer.exe";
                    }
                }

                Process.Start(info);
            }
            catch (Exception)
            {
                return false;
            }

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
