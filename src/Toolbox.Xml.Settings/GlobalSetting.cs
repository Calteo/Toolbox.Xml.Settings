using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Xml.Settings
{
    public class GlobalSetting : Setting
    {
        public override void Save()
        {
            GlobalSettings.Save(this);
        }
    }
}
