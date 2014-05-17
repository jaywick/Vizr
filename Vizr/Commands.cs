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
    public class Commands
    {
        private CommandsList commands;
        private string path;

        public Commands()
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "commands.xml");
            commands = new CommandsList(path);

            Load();
        }

        ~Commands()
        {
            Save();
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(CommandsList));
            using (var stream = File.OpenWrite(path))
            {
                serializer.Serialize(stream, commands);
            }
        }

        private void saveDefault()
        {
            commands.Items.Clear();

            // example
            commands.Items.Add(new Command()
            {
                Name = "Example",
                Subtitle = "Opens example.com",
                CommandName = "explorer.exe",
                Arguments = "https://example.com",
                HitCount = 1
            });

            Save();
        }

        public void Load()
        {
            if (!File.Exists(path))
                saveDefault();

            var serializer = new XmlSerializer(typeof(CommandsList));
            using (var stream = File.OpenRead(path))
            {
                commands = (CommandsList)serializer.Deserialize(stream);
            }
        }

        public IEnumerable<Command> Query(string text)
        {
            var results = commands.AllItems.Where(c => c.Name.ToLower().StartsWith(text.ToLower().Trim()) || c.Name.Contains(" " + text.ToLower().Trim()));
            return results;
        }

        [XmlRoot("Items")]
        public class CommandsList
        {
            private string filePath;

            public CommandsList()
            {
                Items = new List<Command>();
                MetaItems = new List<Command>();
                addMetaCommands();
            }

            public CommandsList(string filePath)
                : this()
            {
                this.filePath = filePath;
            }

            private void addMetaCommands()
            {
                // about app
                MetaItems.Add(new Command()
                {
                    Name = "About",
                    Subtitle = "About this app",
                    CommandName = "explorer.exe",
                    Arguments = "https://github.com/jaywick/vizr",
                    HitCount = 1
                });

                // edit commands
                MetaItems.Add(new Command()
                {
                    Name = "Edit",
                    Subtitle = "Edit commands",
                    CommandName = "explorer.exe",
                    Arguments = filePath,
                    HitCount = 1
                });
            }

            [XmlElement("Command")]
            public List<Command> Items { get; set; }

            [XmlIgnore]
            public List<Command> MetaItems { get; set; }

            [XmlIgnore]
            public List<Command> AllItems
            {
                get
                {
                    return Items.Union(MetaItems).ToList();
                }
            }
        }
    }
}