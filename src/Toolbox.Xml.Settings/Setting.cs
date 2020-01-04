using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Xml.Settings
{
    public abstract class Setting
    {
        internal string Name { get; set; }

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

        public abstract void Save();
    }
}
