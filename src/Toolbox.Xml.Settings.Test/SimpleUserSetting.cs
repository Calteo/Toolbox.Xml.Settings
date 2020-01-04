using System.ComponentModel;

namespace Toolbox.Xml.Settings.Test
{
    class SimpleUserSetting : UserSetting
    {
        public const string DefaultName = "Calteo";

        [DefaultValue(DefaultName)]
        public string Name { get; set; }
    }
}
