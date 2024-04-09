using _App.Scripts.Infrastructure.ProjectSettings.Config;
using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace _App.Scripts.Infrastructure.ProjectSettings
{
    public class ControllerSetupProjectSettings : IInitializable
    {
        private readonly ConfigProjectSettings _configProjectSettings;

        public ControllerSetupProjectSettings(ConfigProjectSettings configProjectSettings)
        {
            _configProjectSettings = configProjectSettings;
        }

        public void Init()
        {
            Application.targetFrameRate = _configProjectSettings.TargetFps;
        }
    }
}