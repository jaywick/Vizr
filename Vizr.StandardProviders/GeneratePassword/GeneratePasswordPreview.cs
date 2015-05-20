using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API;

namespace Vizr.StandardProviders
{
    public class GeneratePasswordPreview : IPreview
    {
        public GeneratePasswordPreview(IResult result, GeneratePasswordProviderPrefs prefs)
        {
            ParentResult = result;
            Preferences = prefs;
        }

        public IResult ParentResult { get; set; }

        public GeneratePasswordProviderPrefs Preferences { get; set; }

        public string Document
        {
            get
            {
                return @"<FlowDocument xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" FontSize=""16"">
	                        <Paragraph FontSize=""35"" Background=""LightGray"" Padding=""10,5"">
                                <Run Text=""{Binding Password, Mode=OneWay}"" FontFamily=""Consolas""/>
                            </Paragraph>

	                        <Paragraph>
		                        <Run Text=""{Binding GeneratedMessageText, Mode=OneWay}""/>
	                        </Paragraph>
                        </FlowDocument>";
            }
        }

        public string Password
        {
            get { return RandomString(Preferences.PasswordSize); }
        }

        public string GeneratedMessageText
        {
            get { return String.Format("Generated a password with {0} character{1}!", Preferences.PasswordSize, Preferences.PasswordSize == 1 ? "" : "s"); }
        }

        #region "Private Methods"

        private readonly Random _rng = new Random();
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string Punctuation = "~!@#$%^&*()_+`-=[]\\{}|;':\",./<>?";

        private IEnumerable<char> AllowedCharacters
        {
            get
            {
                if (Preferences.IncludeUppercase)
                {
                    foreach (var chr in Uppercase.ToCharArray())
                    {
                        yield return chr;
                    }
                }

                if (Preferences.IncludeLowercase)
                {
                    foreach (var chr in Lowercase.ToCharArray())
                    {
                        yield return chr;
                    }
                }

                if (Preferences.IncludeNumbers)
                {
                    foreach (var chr in Numbers.ToCharArray())
                    {
                        yield return chr;
                    }
                }

                if (Preferences.IncludePunctuation)
                {
                    foreach (var chr in Punctuation.ToCharArray())
                    {
                        yield return chr;
                    }
                }
            }
        }

        private string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = AllowedCharacters.ElementAt(_rng.Next(AllowedCharacters.Count()));
            }
            return new string(buffer);
        }

        #endregion
    }
}
