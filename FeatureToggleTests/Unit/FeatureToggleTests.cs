namespace FeatureToggleTests.Unit
{
    using System;
    using FeatureToggle.Classes;
    using FeatureToggle.Enums;
    using FeatureToggle.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureToggleTests
    {
        [Test]
        public void TestSuccessfullParseReturnsToggleStatusActive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<bool>();
            var toggleResponse = toggle.GetToggleState(testParser, "positive");
            Assert.AreEqual(ToggleStatus.Active, toggleResponse);
        }
        [Test]
        public void TestUnSuccessfullParseReturnsToggleStatusInactive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<bool>();
            var toggleResponse = toggle.GetToggleState(testParser, "anythingElse");
            Assert.AreEqual(ToggleStatus.Inactive, toggleResponse);
        }

        [Test]
        public void TestSuccessfullFuncCallWhenToggleStatusActive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<bool>();
            Func<bool> theAction = AlwaysReturnTrue;

            var toggleResponse = toggle.ExecuteMethodIfToggleOn(theAction, testParser, "positive");

            Assert.AreEqual(true, toggleResponse);
        }
        [Test]
        public void TestUnSuccessfullFuncCallWhenToggleStatusInactive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<bool>();
            Func<bool> theAction = AlwaysReturnTrue;

            var toggleResponse = toggle.ExecuteMethodIfToggleOn(theAction, testParser, "anythingElse");
            Assert.AreEqual(false, toggleResponse);
        }

        [Test]
        public void TestSuccessfullFuncStringCallWhenToggleStatusActive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<string>();
            Func<string> theAction = AlwaysReturnFire;

            var toggleResponse = toggle.ExecuteMethodIfToggleOn(theAction, testParser, "positive");

            Assert.AreEqual("Fire", toggleResponse);
        }
        [Test]
        public void TestUnSuccessfullFuncStringCallWhenToggleStatusInactive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<string>();
            Func<string> theAction = AlwaysReturnFire;

            var toggleResponse = toggle.ExecuteMethodIfToggleOn(theAction, testParser, "anythingElse");
            Assert.AreNotEqual("Fire", toggleResponse);
        }

        [Test]
        public void TestSuccessfullFuncTestDataTypeCallWhenToggleStatusActive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<TestDataType>();
            Func<TestDataType> theAction = AlwaysReturnNewTestDataType;

            var toggleResponse = toggle.ExecuteMethodIfToggleOn(theAction, testParser, "positive");

            Assert.AreEqual(new TestDataType().HappynessIs, toggleResponse.HappynessIs);
        }
        [Test]
        public void TestUnSuccessfullFuncTestDataTypeCallWhenToggleStatusInactive()
        {
            IConfigParser testParser = new ConfigParserTestDouble();
            var toggle = new FeatureToggle<TestDataType>();
            Func<TestDataType> theAction = AlwaysReturnNewTestDataType;

            var toggleResponse = toggle.ExecuteMethodIfToggleOn(theAction, testParser, "anythingElse");
            Assert.IsNull(toggleResponse);
        }

        protected bool AlwaysReturnTrue()
        {
            return true;
        }

        protected string AlwaysReturnFire()
        {
            return "Fire";
        }

        protected TestDataType AlwaysReturnNewTestDataType()
        {
            return new TestDataType();
        }
    }

    public class TestDataType
    {
        public string HappynessIs = "Happy";
    }

    internal class ConfigParserTestDouble : IConfigParser
    {
        public bool ToggleConfigTagExists()
        {
            throw new System.NotImplementedException();
        }

        public bool GetToggleStatus(string toggle)
        {
            return toggle == "positive";
        }

        public bool ParseBoolValueFromConfig(string status)
        {
            throw new System.NotImplementedException();
        }
    }
}