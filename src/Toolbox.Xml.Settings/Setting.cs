using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Base class for settings.
    /// </summary>
    [DebuggerDisplay("{GetType().FullName,nq}.{Name}")]
    public abstract class Setting
    {
        internal string Name { get; set; }

        /// <summary>
        /// Resets the properties to the default value, if it existis
        /// </summary>
        /// <see cref="DefaultValueAttribute"/>
        public virtual void Reset()
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(p => p.CanWrite && p.GetCustomAttribute<DefaultValueAttribute>(true) != null);

            foreach (var property in properties)
            {
                var value = property.GetCustomAttribute<DefaultValueAttribute>(true).Value;
                property.SetValue(this, value);
            }

            Resetted?.Invoke(this);
        }

        /// <summary>
        /// Fired if settings are resetted.
        /// </summary>
        /// <see cref="Reset"/>
        public event SettingHandler Resetted;

        /// <summary>
        /// Saves the setting object to disk
        /// </summary>
        public virtual void Save()
        {
            Saved?.Invoke(this);
        }

        /// <summary>
        /// Fired if settigs are saved.
        /// </summary>
        /// <see cref="Save"/>
        public event SettingHandler Saved;
    }
}
