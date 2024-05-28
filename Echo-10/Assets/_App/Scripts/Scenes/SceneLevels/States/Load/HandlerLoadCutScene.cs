using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using System.Threading.Tasks;

namespace Assets._App.Scripts.Scenes.SceneLevels.States.Load
{
    public class HandlerLoadCutScene : IHandlerLoadLevel
    {
        private CutSceneManager _cutSceneManager;
        private TextColorChanger _textColorChanger;

        public HandlerLoadCutScene(CutSceneManager cutSceneManager, TextColorChanger textColorChanger)
        {
            _cutSceneManager = cutSceneManager;
            _textColorChanger = textColorChanger;
        }

        public async Task Process()
        {
            _cutSceneManager.StartNewDialog();

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