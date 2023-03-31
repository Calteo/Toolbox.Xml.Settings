using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Xml.Serialization;

namespace Toolbox.Xml.Settings.Test
{
    internal class LogonSetting : UserSetting
    {
        [Obfuscate]
        public string User { get; set; } = "";

        [Obfuscate]
        public string Password { get; set; } = "";
    }
}
