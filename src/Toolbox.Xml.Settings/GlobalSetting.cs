namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Represents a application setting
    /// </summary>
    public class GlobalSetting : Setting
    {
        /// <summary>
        /// Saves the setting in the application wide folder
        /// </summary>
        public override void Save() => GlobalSettings.Save(this);
    }
}
