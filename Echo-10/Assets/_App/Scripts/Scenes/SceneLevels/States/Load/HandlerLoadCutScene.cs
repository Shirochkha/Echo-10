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
        private TextColorChanger _textColorChanger;

        public HandlerLoadCutScene(ConfigLevel configLevel, ServiceLevelSelection serviceLevelSelection,
            CutSceneManager cutSceneManager, TextColorChanger textColorChanger)
        {
            _configLevel = configLevel;
            _serviceLevelSelection = serviceLevelSelection;
            _cutSceneManager = cutSceneManager;
            _textColorChanger = textColorChanger;
        }

        public async Task Process()
        {
            var selectedLevelId = _serviceLevelSelection.SelectedLevelId;
            var configDialogLines = _configLevel.levels
                .FirstOrDefault(level => level.id == selectedLevelId && selectedLevelId != null).cutScene;

            _cutSceneManager.InitializeCutScene((int)selectedLevelId, configDialogLines);

            await Task.WhenAny(WaitForCutSceneEnd(), WaitForTextColorChange());
        }

        private async Task WaitForCutSceneEnd()
        {
            var tcs = new TaskCompletionSource<object>();

            _cutSceneManager.OnCutSceneEnd = () => tcs.TrySetResult(null);

            await tcs.Task;
        }

        private async Task WaitForTextColorChange()
        {
            var tcs = new TaskCompletionSource<object>();

            _textColorChanger.OnCutSceneEnd = () => tcs.TrySetResult(null);

            await tcs.Task;
        }
    }


}