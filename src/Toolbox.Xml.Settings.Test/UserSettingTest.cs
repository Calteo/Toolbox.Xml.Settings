using System;
using System.Drawing;
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

        [TestMethod]
        public void TestValueType()
        {
            var settingName = nameof(TestValueType);
            var cut = UserSettings.Get<SimpleUserSetting>(settingName);

            var point = new Point(5, 6);

            cut.Name = "Calteo";
            cut.Location = point;
            cut.Save();
            
            var read = UserSettings.Get<SimpleUserSetting>(settingName, true); // forced reload

            Assert.AreEqual(cut.Name, read.Name);
            Assert.AreEqual(cut.Location, read.Location);
        }

    }
}
