namespace FeatureToggle.Interfaces
{
    public interface IConfigParser
    {
        bool GetToggleStatus(string toggle);

        bool ParseBoolValueFromConfig(string status);
    }
}