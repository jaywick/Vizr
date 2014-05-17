﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Vizr
{
    public class Repository
    {
        private VizrPackage commands = new VizrPackage();

        #region Serialization

        public Repository()
        {
            Load();
        }

        ~Repository()
        {
            Save();
        }

        public void Save()
        {
            var settings = new XmlWriterSettings()
            {
                IndentChars = "\t",
                Indent = true,
            };

            using (var stream = File.OpenWrite(Common.CommandsFile))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                var serializer = new XmlSerializer(typeof(VizrPackage));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(writer, commands, namespaces);
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
                using (var stream = File.OpenRead(Common.CommandsFile))
                {
                    commands = new XmlSerializer(typeof(VizrPackage)).Deserialize(stream) as VizrPackage;
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