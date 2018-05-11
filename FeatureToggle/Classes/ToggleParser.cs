namespace FeatureToggle.Classes
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using Interfaces;
    using ToggleExceptions;

    public class ToggleParser : IToggleParser
    {
        private readonly NameValueCollection _toggles;
        public ToggleParser()
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
            if (_toggles.GetValues(toggle)?[0] == null)
            {
                throw new ToggleDoesNotExistException();
            }

            return ParseBoolValueFromSource(_toggles.GetValues(toggle)?[0]);
        }

        public bool ParseBoolValueFromSource(string status)
        {
            if (status == "1" || status.ToLower() == "true")
            {
                return true;
            }
            else if (status == "0" || status.ToLower() == "false")
            {
                return false;
            }
            else
            {
                throw new ToggleParsedOutOfRangeException();
            }
            throw new ToggleDoesNotExistException();
        }
    }
}