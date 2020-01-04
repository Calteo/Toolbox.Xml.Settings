using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Toolbox.Xml.Settings.Test
{
    [TestClass]
    public class GlobalSettingTest
    {
        [TestMethod]
        public void GetDefault()
        {
            var cut = GlobalSettings.Get<SimpleGlobalSetting>();

            Assert.AreEqual(cut.Information, SimpleGlobalSetting.DefaultInformation);
        }
    }
}
