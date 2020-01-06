using System;

namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Manages the settings for the user.
    /// </summary>
    public class UserSettings : SettingStorage<UserSetting>
    {
        static UserSettings()
        {
            Folder = Environment.SpecialFolder.ApplicationData;
        }
    }
}
