//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml.Linq;
//using System.Xml.Serialization;
//using Vizr.Models;

//namespace Vizr.Sources
//{
//    public class ActionsSource : SourceBase
//    {
//        private readonly string ActionsFileName = "Actions.xml";

//        private List<ActionEntry> actions;
        
//        public ActionsSource()
//            : base()
//        {
//            Handler = new ActionsHandler();
//        }

//        public override void Update()
//        {
//            var actionsFile = Workspace.GetFile(ActionsFileName);

//            if (!actionsFile.Exists)
//            {
//                actions = new List<ActionEntry>();
//                return;
//            }
            
//            var actionsListXml = XmlRealizer.Realize<ActionsList>(actionsFile);
//            actions = actionsListXml.GetAllEntries<ActionEntry>();

//            actions.ForEach(a => a.ParentSource = this);
//        }

//        public override void Query(string text)
//        {
//            foreach (var action in actions)
//            {
//                action.Relevance = TextCompare.Score(text, action.Title, action.Tags);
//            }

//            Results = actions;
//        }
//    }

//    public class ActionsHandler : IEntryHandler
//    {
//        public string Preview(EntryBase entry)
//        {
//            return entry.Title;
//        }

//        public ExecutionResult Execute(EntryBase entry)
//        {
//            var action = entry as ActionEntry;

//            try
//            {
//                var info = new ProcessStartInfo(action.Target);

//                if (!action.Application.IsNullOrEmpty())
//                {
//                    info.FileName = action.Application;
//                    info.Arguments = action.Target;
//                }

//                if (action.IsAdminRequired)
//                {
//                    info.Verb = "runas";

//                    if (action.Application.IsNullOrEmpty())
//                    {
//                        info.FileName = "explorer.exe";
//                    }
//                }

//                Process.Start(info);

//                return ExecutionResult.Success;
//            }
//            catch (Exception)
//            {
//                return ExecutionResult.Failed;
//            }
//        }
//    }
//}
