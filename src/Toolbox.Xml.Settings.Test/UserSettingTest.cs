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
            var cut = UserSettings.Get<SimpleUserSetting>();

            // do nothing

            Assert.AreEqual(cut.Name, SimpleUserSetting.DefaultName);
        }

        [TestMethod]
        public void SaveDefaultAndReload()
        {
            var cut = UserSettings.Get<SimpleUserSetting>();

            cut.Name = "Calteo";

            cut.Save();

            var read = UserSettings.Get<SimpleUserSetting>(null, true);

            Assert.AreEqual(cut.Name, read.Name);
        }

    }
}
