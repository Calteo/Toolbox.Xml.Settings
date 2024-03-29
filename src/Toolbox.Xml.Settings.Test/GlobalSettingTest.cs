﻿namespace Toolbox.Xml.Settings.Test
{
    [TestClass]
    public class GlobalSettingTest
    {
        [TestMethod]
        public void GetDefault()
        {
            GlobalSettings.Clear();

            var cut = GlobalSettings.Get<SimpleGlobalSetting>();

            Assert.AreEqual(cut.Information, SimpleGlobalSetting.DefaultInformation);
        }

        [TestMethod]
        public void TestClear()
        {
            var cut = GlobalSettings.Get<SimpleGlobalSetting>();

            cut.Information = "My information";
            cut.Save();
            GlobalSettings.Clear();
            var read = GlobalSettings.Get<SimpleGlobalSetting>();

            Assert.AreNotEqual(cut.Information, read.Information);
        }

    }
}
