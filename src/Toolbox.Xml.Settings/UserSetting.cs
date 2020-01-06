namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Represetns a user setting
    /// </summary>
    public class UserSetting : Setting
    {
        public override void Save()
        {
            UserSettings.Save(this);
        }
    }
}
