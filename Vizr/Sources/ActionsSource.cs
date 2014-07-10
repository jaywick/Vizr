using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Vizr.Sources
{
    public class ActionsSource : SourceBase
    {
        private readonly string ActionsFileName = "actions.xml";

        private List<EntryBase> actions;

        public ActionsSource()
            : base()
        {
            Handler = new ActionsHandler();
        }

        public override void Update()
        {
            var actionsFile = Workspace.GetFile(ActionsFileName);

            if (!actionsFile.Exists)
            {
                actions = new List<EntryBase>();
                return;
            }

            using (var stream = actionsFile.OpenRead())
            {
                var actionsListXml = new XmlSerializer(typeof(UserActionListXml)).Deserialize(stream) as UserActionListXml;
                actions = actionsListXml.GetAllEntries();
            }

            actions.ForEach(a => a.ParentSource = this);
        }

        public override void Query(string text)
        {
            Results = actions.Where(a => match(a as Action, text));
        }

        private bool match(Action action, string text)
        {
            return action.Title.ToLower().ContainsPartialsOf(text.ToLower())
                || action.Title.ToLower().StartsWith(text.ToLower())
                || action.Tags.ToLower().Split(",").Any(t => t.StartsWith(text.ToLower()));
        }
    }

    public class ActionsHandler : IEntryHandler
    {
        public string Preview(EntryBase entry)
        {
            return entry.Title;
        }

        public ExecutionResult Execute(EntryBase entry)
        {
            var action = entry as Action;

            try
            {
                var info = new ProcessStartInfo(action.Target);

                if (!action.Application.IsNullOrEmpty())
                {
                    info.FileName = action.Application;
                    info.Arguments = action.Target;
                }

                if (action.IsAdminRequired)
                    info.Verb = "runas";

                Process.Start(info);

                return ExecutionResult.Success;
            }
            catch (Exception)
            {
                return ExecutionResult.Failed;
            }
        }
    }
}
