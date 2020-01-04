using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Xml.Settings
{
    public class UserSetting : Setting
    {
        public override void Save()
        {
            UserSettings.Save(this);
        }
    }
}
