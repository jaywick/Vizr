using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vizr.API;

namespace Vizr.StandardProviders
{
    public class GeneratePasswordProviderPrefs : API.IProviderPreferences
    {
        public GeneratePasswordProviderPrefs()
        {
            PasswordSize = 10;
            IncludeUppercase = true;
            IncludeLowercase = true;
            IncludeNumbers = true;
            IncludePunctuation = true;
        }

        public void Validate()
        {
            if (PasswordSize > 20)
                throw new ValidationException("PasswordSize cannot be > 20");
            
            if (PasswordSize < 5)
                throw new ValidationException("PasswordSize cannot be < 5");
        }

        public int PasswordSize { get; set; }
        public bool IncludeUppercase { get; set; }
        public bool IncludeLowercase { get; set; }
        public bool IncludeNumbers { get; set; }
        public bool IncludePunctuation { get; set; }
    }
}
