using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class DefaultPreview : IPreview
    {
        public DefaultPreview(IResult result)
        {
            ParentResult = result;
        }

        public IResult ParentResult { get; set; }

        public string Document
        {
            get
            {
                return @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" FontSize=""16"">
	                        <Paragraph>
		                        <Run Text=""{Binding ParentResult.Title}"" FontSize=""20""/>
	                        </Paragraph>

                            <Paragraph>
		                        <Run Text=""{Binding ParentResult.Provider.UniqueName}""/>
	                        </Paragraph>
                        </FlowDocument>";
            }
        }
    }
}
