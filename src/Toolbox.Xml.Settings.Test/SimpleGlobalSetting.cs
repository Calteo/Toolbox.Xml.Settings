using System.ComponentModel;

namespace Toolbox.Xml.Settings.Test
{
    class SimpleGlobalSetting : GlobalSetting
    {
        public const string DefaultInformation = "Some information";

        [DefaultValue(DefaultInformation)]
        public string Information { get; set; }
    }
}
