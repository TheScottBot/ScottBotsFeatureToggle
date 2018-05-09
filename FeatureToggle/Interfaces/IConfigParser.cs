namespace FeatureToggle.Interfaces
{
    public interface IConfigParser
    {
        bool ToggleConfigTagExists();

        bool GetToggleStatus(string toggle);

        bool ParseBoolValueFromConfig(string status);
    }
}