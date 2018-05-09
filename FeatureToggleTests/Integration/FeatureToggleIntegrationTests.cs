namespace FeatureToggleTests.Integration
{
    using System;
    using FeatureToggle.Classes;
    using FeatureToggle.Enums;
    using FeatureToggle.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureToggleIntegrationTests
    {
        [Test]
        public void TestToggleStatusActiveIsReturnedWhenParsingAnItemThatIsToggledOn()
        {
            IConfigParser configParser = new ConfigParser();
            IFeatureToggle<bool> featureToggle = new FeatureToggle<bool>();

            var toggleStatus = featureToggle.GetToggleState(configParser, "ButtonToggle");
            Assert.AreEqual(ToggleStatus.Active, toggleStatus);
        }

        [Test]
        public void TestToggleStatusInactiveIsReturnedWhenParsingAnItemThatIsToggledOff()
        {
            IConfigParser configParser = new ConfigParser();
            IFeatureToggle<bool> featureToggle = new FeatureToggle<bool>();

            var toggleStatus = featureToggle.GetToggleState(configParser, "NotFinished");
            Assert.AreEqual(ToggleStatus.Inactive, toggleStatus);
        }

        [Test]
        public void TestOutOfRangeExceptionIsReturnedWhenParsingAnItemThatIsToggledAsdf()
        {
            var configParser = new ConfigParser();
            IFeatureToggle<bool> featureToggle = new FeatureToggle<bool>();
            Assert.Throws<ArgumentOutOfRangeException>(() => featureToggle.GetToggleState(configParser, "asdf"));
        }

        [Test]
        public void TestNullReferenceExceptionIsReturnedWhenParsingAnItemThatDoesNotExist()
        {
            var configParser = new ConfigParser();
            IFeatureToggle<bool> featureToggle = new FeatureToggle<bool>();
            Assert.Throws<NullReferenceException>(() => featureToggle.GetToggleState(configParser, "wewewewewewewewe"));
        }

        [Test]
        public void TestFakeMethodWillNotChangeValueIfConfigItemIsToggledToFalse()
        {
            var changeMe = "Unchanged";

            FakeMethod("FakeFalse", out changeMe);

            Assert.AreEqual("Unchanged", changeMe);
        }

        [Test]
        public void TestFakeMethodWillChangeValueIfConfigItemIsToggledToTrue()
        {
            var changeMe = "Unchanged";

            FakeMethod("FakeTrue", out changeMe);

            Assert.AreEqual("has been changed", changeMe);
        }

        [Test]
        public void TestActionFakeMethodThatReturnsTrueWillReturnTrueIfConfigItemIsToggledToTrue()
        {
            IFeatureToggle<bool> featureToggler = new FeatureToggle<bool>();

            var result = featureToggler.ExecuteMethodIfToggleOn(FakeMethodThatReturnsTrue, "FakeTrue");

            Assert.IsTrue(result);
        }

        [Test]
        public void TestActionFakeMethodThatReturnsTrueWillReturnFalseIfConfigItemIsToggledToFalse()
        {
            IFeatureToggle<bool> featureToggler = new FeatureToggle<bool>();

            var result = featureToggler.ExecuteMethodIfToggleOn(FakeMethodThatReturnsTrue, "FakeFalse");

            Assert.IsFalse(result);
        }

        protected void FakeMethod(string keyName, out string changeMe)
        {
                IConfigParser configParser = new ConfigParser();
                IFeatureToggle<bool> featureToggle = new FeatureToggle<bool>();

                var response = featureToggle.GetToggleState(configParser, keyName);
                if (response == ToggleStatus.Active)
                {
                    changeMe = "has been changed";
                    return;
                }
                changeMe = "Unchanged";
        }
        protected bool FakeMethodThatReturnsTrue()
        {
            return true;
        }
    }
}