using System.Drawing;

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
        public void ResetWithEvent()
        {
            const string name = nameof(ResetWithEvent);
            var cut = UserSettings.Get<SimpleUserSetting>(name);

            cut.Name = "SomeTest";
            cut.Save();

            var fired = 0;

            cut.Resetted += s =>
            {
                Assert.AreSame(cut, s);
                fired++;
            };

            cut.Reset();
            
            Assert.AreEqual(1, fired);
        }


        [TestMethod]
        public void SaveWithEvents()
        {
            const string name = nameof(SaveWithEvents);

            var cut = UserSettings.Get<SimpleUserSetting>(name);

            var globalFired = 0;
            var fired = 0;

            UserSettings.Saved += s => 
            {
                Assert.AreSame(cut, s);
                globalFired++;
            };

            cut.Saved += s =>
            {
                Assert.AreSame(cut, s);
                fired++;
            };

            cut.Name = "SomeTest";
            cut.Save();

            Assert.AreEqual(1, globalFired);
            Assert.AreEqual(1, fired);
        }

        [TestMethod]
        public void TestClear()
        {
            var settingName = nameof(TestClear);
            var cut = UserSettings.Get<SimpleUserSetting>(settingName);

            cut.Name = "Some Name";
            cut.Save();
            
            UserSettings.Clear();

            var read = UserSettings.Get<SimpleUserSetting>(settingName);

            Assert.AreNotSame(cut, read);
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

        [TestMethod]
        public void SaveLoadObfuscatedSettings()
        {
            var setting = UserSettings.Get<LogonSetting>(nameof(SaveLoadObfuscatedSettings));

            const string User = "TestUser";
            const string Password = "TestUserPassword";

            setting.User = User;
            setting.Password = Password;

            setting.Save();

            var cut = UserSettings.Get<LogonSetting>(nameof(SaveLoadObfuscatedSettings), true);

            Assert.AreEqual(User, cut.User);
            Assert.AreEqual(Password, cut.Password);
        }

    }
}
