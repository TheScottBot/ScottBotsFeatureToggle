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
            var configParser = new ToggleParser();
            Assert.IsTrue(configParser.GetToggleStatus("ButtonToggle"));
        }

        [Test]
        public void TestFalseIsReturnedWhenParsingAnItemThatIsToggledOff()
        {
            var configParser = new ToggleParser();
            Assert.IsFalse(configParser.GetToggleStatus("NotFinished"));
        }

        [Test]
        public void TestOutOfRangeExceptionIsReturnedWhenParsingAnItemThatIsToggledAsdf()
        {
            var configParser = new ToggleParser();
            Assert.Throws<ArgumentOutOfRangeException>(() => configParser.GetToggleStatus("asdf"));
        }

        [Test]
        public void TestNullReferenceExceptionIsReturnedWhenParsingAnItemThatDoesNotExist()
        {
            var configParser = new ToggleParser();
            Assert.Throws<NullReferenceException>(() => configParser.GetToggleStatus("wewewewewewewewe"));
        }
    }
}
