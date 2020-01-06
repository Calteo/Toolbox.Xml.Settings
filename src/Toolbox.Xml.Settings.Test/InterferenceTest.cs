using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Toolbox.Xml.Settings.Test
{
    [TestClass]
    public class InterferenceTest
    {
        [TestMethod]
        public void GetUserAndClearGlobal()
        {
            var cut = UserSettings.Get<SimpleUserSetting>();

            cut.Name = "Some Name";
            cut.Save();

            GlobalSettings.Clear();

            var read = UserSettings.Get<SimpleUserSetting>();

            Assert.AreEqual(cut.Name, read.Name);
        }
    }
}
