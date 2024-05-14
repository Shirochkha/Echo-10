using _App.Scripts.Infrastructure.GameCore.States;
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
            ProcessLevelMenu().Forget();
        }

        private async Task ProcessLevelMenu()
        {
            var tcs = new TaskCompletionSource<int>();
            _levelsMenuUI.OnLevelButtonClicked += levelId =>
            {
                tcs.SetResult(levelId);
            };

            var selectedLevelId = await tcs.Task;

            StateMachine.ChangeState<StateLoadLevel>();
        }
    }
}
