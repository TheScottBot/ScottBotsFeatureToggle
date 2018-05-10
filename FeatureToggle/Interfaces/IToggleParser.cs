namespace FeatureToggle.Interfaces
{
    public interface IToggleParser
    {
        bool GetToggleStatus(string toggle);

        bool ParseBoolValueFromSource(string status);
    }
}