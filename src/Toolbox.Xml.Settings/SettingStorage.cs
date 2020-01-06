using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Toolbox.Xml.Serialization;

namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Base class the handle the storing of <see cref="Setting"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SettingStorage<T> where T : Setting
    {
        static SettingStorage()
        {
            ApplicationName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            TypeName = $"SettingStorage<{typeof(T).Name}>";
        }

        /// <summary>
        /// Name of the class for logging
        /// </summary>
        protected static string TypeName { get; }

        protected SettingStorage()
        {
        }

        private static string _applicationName;
        private static Environment.SpecialFolder _folder = Environment.SpecialFolder.ApplicationData;

        /// <summary>
        /// Name of the application
        /// </summary>
        /// <remarks>
        /// This is used as a folder name and can be set from the programm.
        /// The default value is the name of the executable.
        /// </remarks>
        public static string ApplicationName
        {
            get => _applicationName;
            set
            {
                _applicationName = value;
                SetFolderName();
            }
        } 

        protected internal static Environment.SpecialFolder Folder
        {
            get => _folder;
            set
            {
                _folder = value;
                SetFolderName();
            }
        }

        /// <summary>
        /// Gets the name of the folder where the settings are stored.
        /// </summary>
        public static string FolderName { get; private set; }

        private static void SetFolderName()
        {
            FolderName = Path.Combine(Environment.GetFolderPath(Folder), ApplicationName);
        }

        private static Dictionary<string, T> Settings { get; } = new Dictionary<string, T>();

        private static string GetKey<TS>(string name)
        {
            return $"{typeof(TS).FullName}${name}";
        }

        private static string GetFileName(Type type, string name)
        {
            return Path.Combine(FolderName, string.Concat(type.FullName, name, ".xml"));
        }

        /// <summary>
        /// Gets a setting of give type.
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="name"></param>
        /// <param name="forced"></param>
        /// <returns></returns>
        /// <remarks>
        /// The returned setting is from cache, disc or created new - in that order.
        /// </remarks>
        public static TS Get<TS>(string name = null, bool forced = false) where TS : T, new()
        {            
            var key = GetKey<TS>(name);

            Trace.WriteLine($"get{(forced ? " forced" : "")} {key}", TypeName);

            T setting = null;

            if (forced || !Settings.TryGetValue(key, out setting))
            {
                var filename = GetFileName(typeof(TS), name);
                if (File.Exists(filename))
                {
                    Trace.WriteLine($"get file '{filename}'", TypeName);
                    try
                    {
                        var formatter = new XmlFormatter<TS>();
                        setting = formatter.Deserialize(filename);
                    }
                    catch (Exception exception)
                    {
                        Trace.WriteLine($"failed to load file. {exception}", TypeName);
                    }
                }
                if (setting == null)
                {
                    Trace.WriteLine($"new default", TypeName);
                    setting = new TS
                    {
                        Name = name
                    };
                    setting.Reset();
                }
                Settings[key] = setting;
            }
            else
            {
                Trace.WriteLine($"cache hit", TypeName);
            }
            return (TS)setting;
        }        

        /// <summary>
        /// Save a setting to disc.
        /// </summary>
        /// <param name="setting"></param>
        public static void Save(T setting) 
        {
            var type = setting.GetType();

            var filename = GetFileName(type, setting.Name);
            var directory = Path.GetDirectoryName(filename);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var formatter = new XmlFormatter(type);
            formatter.Serialize(setting, filename);
        }

        /// <summary>
        /// Removes all settings on disc and in cache
        /// </summary>
        public static void Clear()
        {
            foreach (var filename in Directory.GetFiles(FolderName, "*.xml"))
            {
                File.Delete(filename);
            }
            Settings.Clear();
        }
    }
}
