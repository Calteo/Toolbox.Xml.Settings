using System;

namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Manages the settings for the application.
    /// </summary>
    public class GlobalSettings : SettingStorage<GlobalSetting>
    {
        static GlobalSettings()
        {
            Folder = Environment.SpecialFolder.CommonApplicationData;
        }
    }
}
