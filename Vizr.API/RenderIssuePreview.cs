using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API.Extensions;

namespace Vizr.API
{
    public class RenderIssuePreview : IPreview
    {
        public RenderIssuePreview(IResult result, Exception exception)
        {
            ParentResult = result;
            Message = String.Join(Environment.NewLine + Environment.NewLine, exception.GetDescendantExceptionsAndSelf().Select(x => x.Message));
        }

        public IResult ParentResult { get; set; }

        public string Message { get; set; }

        public string Document
        {
            get
            {
                return @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" FontSize=""16"">
	                        <Paragraph>
		                        <Run Text=""Failed to render preview"" FontSize=""20"" Foreground=""DarkRed""/>
	                        </Paragraph>

                            <Paragraph FontFamily=""Consolas"">
		                        <Run Text=""{Binding Message, Mode=OneWay}""/>
	                        </Paragraph>
                        </FlowDocument>";
            }
        }
    }
}
