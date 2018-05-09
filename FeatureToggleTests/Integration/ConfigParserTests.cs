namespace FeatureToggleTests.Integration
{
    using System;
    using FeatureToggle.Classes;
    using NUnit.Framework;

    [TestFixture]
    public class ConfigParserTests
    {
        [Test]
        public void TestTrueIsReturnedWhenParsingAnItemThatIsToggledOn()
        {
            var configParser = new ConfigParser();
            Assert.IsTrue(configParser.GetToggleStatus("ButtonToggle"));
        }

        [Test]
        public void TestFalseIsReturnedWhenParsingAnItemThatIsToggledOff()
        {
            var configParser = new ConfigParser();
            Assert.IsFalse(configParser.GetToggleStatus("NotFinished"));
        }

        [Test]
        public void TestOutOfRangeExceptionIsReturnedWhenParsingAnItemThatIsToggledAsdf()
        {
            var configParser = new ConfigParser();
            Assert.Throws<ArgumentOutOfRangeException>(() => configParser.GetToggleStatus("asdf"));
        }

        [Test]
        public void TestNullReferenceExceptionIsReturnedWhenParsingAnItemThatDoesNotExist()
        {
            var configParser = new ConfigParser();
            Assert.Throws<NullReferenceException>(() => configParser.GetToggleStatus("wewewewewewewewe"));
        }
    }
}
