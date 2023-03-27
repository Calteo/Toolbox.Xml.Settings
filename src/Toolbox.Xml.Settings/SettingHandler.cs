namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Handler for <see cref="Setting"/> events.
    /// </summary>
    /// <param name="setting"></param>
    public delegate void SettingHandler(Setting setting);

    /// <summary>
    /// Handler for typed <see cref="Setting"/> events.
    /// </summary>
    /// <param name="setting"></param>
    public delegate void SettingHandler<T>(T setting) where T : Setting;
}
