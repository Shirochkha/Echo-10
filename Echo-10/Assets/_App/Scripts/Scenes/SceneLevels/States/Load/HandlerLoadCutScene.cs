using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System.Linq;
using System.Threading.Tasks;

namespace Assets._App.Scripts.Scenes.SceneLevels.States.Load
{
    public class HandlerLoadCutScene : IHandlerLoadLevel
    {
        private ConfigLevel _configLevel;
        private ServiceLevelSelection _serviceLevelSelection;
        private CutSceneManager _cutSceneManager;

        public HandlerLoadCutScene(ConfigLevel configLevel, ServiceLevelSelection serviceLevelSelection,
            CutSceneManager cutSceneManager)
        {
            _configLevel = configLevel;
            _serviceLevelSelection = serviceLevelSelection;
            _cutSceneManager = cutSceneManager;
        }


        public Task Process()
        {
            var selectedLevelId = _serviceLevelSelection.SelectedLevelId;
            var configDialogLines = _configLevel.levels
                .FirstOrDefault(level => level.id == selectedLevelId && selectedLevelId != null).cutScene;

            _cutSceneManager.InitializeCutScene((int)selectedLevelId, configDialogLines);

            return Task.CompletedTask;
        }
    }
}