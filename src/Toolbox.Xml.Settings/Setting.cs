using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Toolbox.Xml.Settings
{
    /// <summary>
    /// Base class for settings.
    /// </summary>
    [DebuggerDisplay("{GetType().Fullname,nq}.{Name}")]
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
        }

        /// <summary>
        /// Saves the setting object to disk
        /// </summary>
        public abstract void Save();
    }
}
