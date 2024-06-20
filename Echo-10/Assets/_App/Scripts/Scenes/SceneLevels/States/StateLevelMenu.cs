using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Libs.SoundsManager;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateLevelMenu : GameState
    {
        private LevelsMenuUI _levelsMenuUI;
        private ConfigLevel _configLevel;
        private ServiceLevelState _serviceLevelState;

        private AudioClip _musicLevelMenu;
        public StateLevelMenu(LevelsMenuUI levelsMenuUI, ConfigLevel configLevel, ServiceLevelState serviceLevelState,
            AudioClip musicLevelMenu)
        {
            _levelsMenuUI = levelsMenuUI;
            _configLevel = configLevel;
            _serviceLevelState = serviceLevelState;
            _musicLevelMenu = musicLevelMenu;
        }

        public override void OnEnterState()
        {
            Debug.Log("LevelMenu");
            if(SoundMusicManager.instance != null)
                SoundMusicManager.instance.PlayMusicClip(_musicLevelMenu);

            foreach (var level in _configLevel.levels)
            {
                foreach (var objectData in level.configObjects.objects)
                {
                    if (objectData.objectReference != null)
                        GameObject.Destroy(objectData.objectReference);
                }
            }

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
                _serviceLevelState.HasLevelCreate = false;
                StateMachine.ChangeState<StateLoadLevel>();
            }
        }

    }
}
