using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Vizr
{
    public class Commands
    {
        private CommandsList commands = new CommandsList();

        #region Serialization

        public Commands()
        {
            Load();
        }

        ~Commands()
        {
            Save();
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(CommandsList));
            using (var stream = File.OpenWrite(Common.CommandsFile))
            {
                serializer.Serialize(stream, commands);
            }
        }

        private void saveDefault()
        {
            commands.Items.Clear();

            // launch website
            commands.Items.Add(new Command()
            {
                Pattern = "Example",
                Title = "Opens example.com",
                Target = "http://example.com",
            });

            // google
            commands.Items.Add(new Request()
            {
                Pattern = @"\?(.+)",
                Title = "Google for '{0}'",
                Target = "https://www.google.com/search?q={0}",
            });

            // google IFL
            commands.Items.Add(new Request()
            {
                Pattern = @"\?(.+)",
                Title = "I'm feeling lucky '{0}'",
                Target = "https://www.google.com/search?q={0}&btnI",
            });

            // find on pc
            /// see more: http://msdn.microsoft.com/en-us/library/ff684385.aspx
            commands.Items.Add(new Request()
            {
                Pattern = @"\?(.+)",
                Title = "Search PC for '{0}'",
                Target = "search-ms:query={0}&",
            });

            Save();
        }

        public void Load()
        {
            if (!File.Exists(Common.CommandsFile))
                saveDefault();

            try
            {
                var serializer = new XmlSerializer(typeof(CommandsList));
                using (var stream = File.OpenRead(Common.CommandsFile))
                {
                    commands = (CommandsList)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                var message = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;

                System.Windows.MessageBox.Show("Saved commands could not be loaded. This is not unusual as this software is pre-alpha.\n\n" +
                                               "Try editing the file and opening Vizr again or delete the file to restore default commands.\n\n" +
                                               "Exception information:\n  " + message, "Vizr",
                                               System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", Common.CommandsFile));

                Environment.Exit(1);
            }
        }

        #endregion

        public IEnumerable<Command> Query(string text)
        {
            var results = commands.AllItems.Where(c => c.Match(text));

            return results;
        }
    }
}