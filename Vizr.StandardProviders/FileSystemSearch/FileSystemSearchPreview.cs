using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API;

namespace Vizr.StandardProviders
{
    public class FileSystemSearchPreview : IPreview
    {
        public FileSystemSearchPreview(IResult result)
        {
            ParentResult = result;
        }

        public IResult ParentResult { get; set; }

        public FileSystemSearchResult ActualResult
        {
            get { return (FileSystemSearchResult)ParentResult; }
        }

        public string Document
        {
            get
            {
                return @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" FontSize=""16"">
	                        <Paragraph FontSize=""20"">
		                        <Bold><Run Text=""{Binding Type, Mode=OneWay}""/></Bold>
	                        </Paragraph>

                            <Paragraph FontSize=""20"">
                                <Run Text=""{Binding Name, Mode=OneWay}""/>
	                        </Paragraph>

                            <Paragraph>
		                        <Run Text=""{Binding Path, Mode=OneWay}""/>
	                        </Paragraph>
                        </FlowDocument>";
            }
        }

        public string Name
        {
            get { return ActualResult.Name; }
        }

        public string Type
        {
            get { return ActualResult.Type.ToString(); }
        }

        public string Path
        {
            get { return ActualResult.Path; }
        }
    }
}
