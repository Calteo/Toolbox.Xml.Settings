using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Toolbox.Xml.Settings.Test
{
    [TestClass]
    public class UserSettingTest
    {
        [TestMethod]
        public void GetDefault()
        {
            UserSettings.Clear();

            var cut = UserSettings.Get<SimpleUserSetting>();

            // do nothing

            Assert.AreEqual(cut.Name, SimpleUserSetting.DefaultName);
        }

        [TestMethod]
        public void SaveDefaultAndReload()
        {
            var cut = UserSettings.Get<SimpleUserSetting>();

            cut.Name = "Some Name";
            cut.Save();

            var read = UserSettings.Get<SimpleUserSetting>(null, true);

            Assert.AreEqual(cut.Name, read.Name);
        }

        [TestMethod]
        public void SaveNamedAndReload()
        {
            const string name = nameof(SaveNamedAndReload);
            var cut = UserSettings.Get<SimpleUserSetting>(name);

            cut.Name = "SomeTest";
            cut.Save();

            var read = UserSettings.Get<SimpleUserSetting>(name, true);

            Assert.AreEqual(cut.Name, read.Name);
        }

        [TestMethod]
        public void TestClear()
        {
            var cut = UserSettings.Get<SimpleUserSetting>();

            cut.Name = "Some Name";
            cut.Save();
            UserSettings.Clear();

            var read = UserSettings.Get<SimpleUserSetting>();
            
            Assert.AreNotEqual(cut.Name, read.Name);
        }
    }
}
