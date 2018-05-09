namespace FeatureToggle.Classes
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using Interfaces;

    public class ConfigParser : IConfigParser
    {
        private readonly NameValueCollection _toggles;
        public ConfigParser()
        {
            if (ToggleConfigTagExists())
            {
                _toggles = ConfigurationManager.GetSection("Toggles") as NameValueCollection;
            }
        }
        public bool ToggleConfigTagExists()
        {
            var toggleSection = ConfigurationManager.GetSection("Toggles");
            return toggleSection != null;
        }

        public bool GetToggleStatus(string toggle)
        {
            return ParseBoolValueFromConfig(_toggles.GetValues(toggle)?[0]);
        }

        public bool ParseBoolValueFromConfig(string status)
        {
            if (status == "1" || status.ToLower() == "true")
            {
                return true;
            }
            if (status == "0" || status.ToLower() == "false")
            {
                return false;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}