using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Xml.Settings
{
    public class UserSettings : SettingStorage<UserSetting>
    {
        static UserSettings()
        {
            Folder = Environment.SpecialFolder.ApplicationData;
        }

    }
}
