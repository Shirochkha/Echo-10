﻿using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States.Load
{
    public class HandlerLoadCutScene : IHandlerLoadLevel
    {
        private CutSceneManager _cutSceneManager;
        private TextColorChanger _textColorChanger;
        private ServiceLevelState _serviceLevelState;

        public HandlerLoadCutScene(CutSceneManager cutSceneManager, TextColorChanger textColorChanger,
            ServiceLevelState serviceLevelState)
        {
            _cutSceneManager = cutSceneManager;
            _textColorChanger = textColorChanger;
            _serviceLevelState = serviceLevelState;
        }

        public async Task Process()
        {
            if (_serviceLevelState.ConfigCutScene == null || _serviceLevelState.IsCutSceneLook())
            {
                Debug.Log("No cut scene to process.");
                _cutSceneManager.OnCutSceneEnd?.Invoke();
                _textColorChanger.OnCutSceneEnd?.Invoke();
                return;
            }

            _cutSceneManager.StartNewDialog();
            await Task.WhenAny(WaitForCutSceneEnd(), WaitForTextColorChange());
            _serviceLevelState.SetCutSceneLook();
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
