using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateLevelMenu : GameState
    {
        private LevelsMenuUI _levelsMenuUI;
        public StateLevelMenu(LevelsMenuUI levelsMenuUI)
        {
            _levelsMenuUI = levelsMenuUI;
        }

        public override void OnEnterState()
        {
            Debug.Log("LevelMenu");
            _levelsMenuUI.LevelsMenuPanel.SetActive(true);
            ProcessLevelMenu().Forget();
        }

        private async Task ProcessLevelMenu()
        {
            while (true)
            {
                var tcs = new TaskCompletionSource<int>();

                Action<int> handler = null;
                handler = levelId =>
                {
                    tcs.SetResult(levelId);
                    _levelsMenuUI.OnLevelButtonClicked -= handler;
                };

                _levelsMenuUI.OnLevelButtonClicked += handler;

                var selectedLevelId = await tcs.Task;

                StateMachine.ChangeState<StateLoadLevel>();
            }
        }

    }
}
