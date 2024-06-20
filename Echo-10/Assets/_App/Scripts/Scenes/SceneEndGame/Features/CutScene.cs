using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;
using Assets._App.Scripts.Infrastructure.CutScene.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneEndGame.Features
{
    public class CutScene : IUpdatable
    {
        private ConfigCharacters _characterManager;
        private float _pauseTime;
        private float _speedText;
        private Image _dialogImage;
        private Text _textArea;
        private ConfigCutScene _configCutScene;
        private ConfigScenes _scenes;

        private int _currentLineIndex = 0;
        private DialogLine _dialogLine;
        private bool _isAnimatingText = false;
        private float _timeSinceLastLetter = 0f;
        private int _letterIndex = 0;
        private string _currentDialog = "";
        private float _timeUntilNextAction;

        private bool _isWaitingForSpace = false;

        public CutScene(ConfigCharacters characterManager, float pauseTime, float speedText, Image dialogImage,
            Text textArea, ConfigCutScene configCutScene, ConfigScenes scenes)
        {
            _characterManager = characterManager;
            _pauseTime = pauseTime;
            _speedText = speedText;
            _dialogImage = dialogImage;
            _textArea = textArea;
            _configCutScene = configCutScene;
            _scenes = scenes;

            StartNewDialog();
        }

        public void Update()
        {
            if (_timeUntilNextAction > 0)
            {
                _timeUntilNextAction -= Time.deltaTime;
                if (_timeUntilNextAction <= 0)
                {
                    ContinueToNextLine();
                    _timeUntilNextAction = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isAnimatingText)
                {
                    CompleteTextAnimation();
                    return;
                }
                else if (_isWaitingForSpace)
                {
                    _isWaitingForSpace = false;
                    ContinueToNextLine();
                    return;
                }
            }

            if (_isAnimatingText)
            {
                _timeSinceLastLetter += Time.deltaTime;

                if (_timeSinceLastLetter >= _speedText)
                {
                    AddNextLetter();
                    _timeSinceLastLetter = 0f;
                }
            }
        }

        public void StartNewDialog()
        {
            _currentLineIndex = 0;
            SetupCurrentDialogLine();
        }

        private void SetupCurrentDialogLine()
        {
            if (_currentLineIndex >= _configCutScene.dialogLines.Length)
            {
                EndCutScene();
                return;
            }

            _dialogLine = _configCutScene.dialogLines[_currentLineIndex];
            _letterIndex = 0;
            _isAnimatingText = true;
            _currentDialog = _dialogLine.text;
            _textArea.text = string.Empty;

            _dialogImage.sprite = _dialogLine.sprite;
            int selectedCharacterIndex = _dialogLine.characterIndex;
            var selectedCharacter = _characterManager.characters[selectedCharacterIndex];

            _textArea.alignment = selectedCharacter.alignment;
            _textArea.color = selectedCharacter.textColor;

            _isWaitingForSpace = _dialogLine.pauseResume > 1;
            _timeUntilNextAction = _isWaitingForSpace ? 0 : _dialogLine.pauseResume;

            Debug.Log($"Setting up dialog line {_currentLineIndex}. Waiting for space: {_isWaitingForSpace}");
        }

        private void AddNextLetter()
        {
            if (_letterIndex < _currentDialog.Length)
            {
                char letter = _currentDialog[_letterIndex];
                _textArea.text += letter;
                _letterIndex++;

                if (letter == '.' || letter == ',' || letter == '!' || letter == '?')
                {
                    _timeSinceLastLetter = -_pauseTime;
                }
            }
            else
            {
                _isAnimatingText = false;

                if (!_isWaitingForSpace)
                {
                    _timeUntilNextAction = _dialogLine.pauseResume;
                }
            }
        }

        private void CompleteTextAnimation()
        {
            _textArea.text = _currentDialog;
            _letterIndex = _currentDialog.Length;
            _isAnimatingText = false;

            if (_isWaitingForSpace)
            {
                Debug.Log("Waiting for space to continue.");
            }
            else
            {
                _timeUntilNextAction = _dialogLine.pauseResume;
                if (_timeUntilNextAction > 0)
                {
                    Debug.Log($"Waiting for {_timeUntilNextAction} seconds to continue.");
                }
            }
        }

        private void ContinueToNextLine()
        {
            _currentLineIndex++;
            SetupCurrentDialogLine();
        }

        private void EndCutScene()
        {
            foreach (var scene in GetAvailableSwitchScenes())
            {
                if (scene.SceneViewName == "MainMenu")
                {
                    LoadScene(scene.SceneKey);
                    return;
                }
            }
        }

        private void LoadScene(string sceneId)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
        }

        public List<SceneInfo> GetAvailableSwitchScenes()
        {
            var currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            var result = new List<SceneInfo>();

            foreach (var sceneInfo in _scenes.AvailableScenes)
            {
                if (sceneInfo.SceneKey != currentSceneName)
                {
                    result.Add(sceneInfo);
                }
            }

            return result;
        }
    }
}
