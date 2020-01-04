using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Xml.Settings
{
    public class GlobalSettings : SettingStorage<GlobalSetting>
    {
        static GlobalSettings()
        {
            Folder = Environment.SpecialFolder.CommonApplicationData;
        }
    }
}
