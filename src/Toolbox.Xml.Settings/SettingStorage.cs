using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Xml.Serialization;

namespace Toolbox.Xml.Settings
{
    public class SettingStorage<T> where T : Setting
    {
        static SettingStorage()
        {
            ApplicationName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
        }

        protected SettingStorage()
        {
        }

        private static string _applicationName;
        private static Environment.SpecialFolder _folder = Environment.SpecialFolder.ApplicationData;

        public static string ApplicationName
        {
            get => _applicationName;
            set
            {
                _applicationName = value;
                SetFolderName();
            }
        } 

        public static Environment.SpecialFolder Folder
        {
            get => _folder;
            set
            {
                _folder = value;
                SetFolderName();
            }
        }

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

        public static TS Get<TS>(string name = null, bool forced = false) where TS : T, new()
        {
            var key = GetKey<TS>(name);
            T setting = null;

            if (forced || !Settings.TryGetValue(key, out setting))
            {
                var filename = GetFileName(typeof(TS), name);
                if (File.Exists(filename))
                {
                    try
                    {
                        var formatter = new XmlFormatter<TS>();
                        setting = formatter.Deserialize(filename);
                    }
                    catch (Exception exception)
                    {
                        Trace.WriteLine($"Failed to load setting from '{filename}'. {exception}", $"{typeof(SettingStorage<>).Namespace}");
                    }
                }
                if (setting == null)
                {
                    setting = new TS
                    {
                        Name = name
                    };
                    setting.Reset();
                }
                Settings[key] = setting;
            }
            return (TS)setting;
        }        

        public static void Save(T setting) 
        {
            var type = setting.GetType();

            var filename = GetFileName(type.GetType(), setting.Name);
            var directory = Path.GetDirectoryName(filename);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var formatter = new XmlFormatter(type);
            formatter.Serialize(setting, filename);
        }
    }
}
