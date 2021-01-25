using System.ComponentModel;
using System.Drawing;

namespace Toolbox.Xml.Settings.Test
{
    class SimpleUserSetting : UserSetting
    {
        public const string DefaultName = "Calteo";

        [DefaultValue(DefaultName)]
        public string Name { get; set; }

        public Point Location { get; set; }
    }
}
