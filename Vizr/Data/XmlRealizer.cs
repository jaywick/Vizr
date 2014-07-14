﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class XmlRealizer
    {
        public static TModel Realize<TModel>(string path) where TModel : class
        {
            return Realize<TModel>(new FileInfo(path));
        }

        public static TModel Realize<TModel>(FileInfo file) where TModel : class
        {
            TModel dataObject;

            using (var stream = file.OpenRead())
            {
                try
                {
                    dataObject = new XmlSerializer(typeof(TModel)).Deserialize(stream) as TModel;
                }
                catch (Exception ex)
                {
                    throw new InvalidFormatException(file, typeof(TModel), ex);
                }
            }

            return dataObject;
        }
    }

    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(FileInfo sourceFile, Type destinationType, Exception innerException)
            : base(formatMessage(sourceFile, destinationType), innerException)
        {
        }

        private static string formatMessage(FileInfo sourceFile, Type destinationType)
        {
            return String.Format("Invalid XML format {0} cannot be realized to {1}",
                                            sourceFile.Name,
                                            destinationType.ToString());
        }
    }
}
