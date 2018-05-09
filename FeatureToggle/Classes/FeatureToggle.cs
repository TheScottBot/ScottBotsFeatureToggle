namespace FeatureToggle.Classes
{
    using System;
    using Enums;
    using Interfaces;
    public class FeatureToggle <T> : IFeatureToggle <T>
    {
        private ToggleStatus Status(bool active)
        {
            return active ? ToggleStatus.Active : ToggleStatus.Inactive;
        }

        public ToggleStatus GetToggleState(IConfigParser parser, string toggleKey)
        {
            return Status(parser.GetToggleStatus(toggleKey));
        }


        public void ExecuteMethodIfToggleOn(Action methodToRun, IConfigParser configParser, string keyName)
        {
            var response = GetToggleState(configParser, keyName);
            if (response == ToggleStatus.Active)
            {
                methodToRun();
            }
        }

        public void ExecuteMethodIfToggleOn(Action methodToRun, string keyName)
        {
            IConfigParser configParser = new ConfigParser();
            ExecuteMethodIfToggleOn(methodToRun, configParser, keyName);
        }

        public T ExecuteMethodIfToggleOn(Func<T> methodToRun, string keyName)
        {
            IConfigParser configParser = new ConfigParser();
            return ExecuteMethodIfToggleOn(methodToRun, configParser, keyName);
        }

        public T ExecuteMethodIfToggleOn(Func<T> methodToRun, IConfigParser configParser,  string keyName)
        {
            var response = GetToggleState(configParser, keyName);
            if (response == ToggleStatus.Active)
            {
                return methodToRun();
            }
            return default(T);
        }
    }
}
