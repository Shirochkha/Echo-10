using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.CutScene.Config;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class CutSceneManager : IUpdatable
    {
        private GameObject _cutSceneObject;
        private ConfigCharacters _characterManager;
        private float _pauseTime;
        private float _speedText;
        private Image _dialogImage;
        private Text _textArea;

        private ConfigCutScene _configDialogLines;
        private int _currentLineIndex = 0;
        private bool _isAnimatingText = false;
        private float _timeSinceLastLetter = 0f;
        private int _letterIndex = 0;
        private string _currentDialog = "";
        private float _timeUntilNextAction;

        private bool _isWaitingForSpace = false;

        private Action _onCutSceneEnd;

        public Action OnCutSceneEnd { get => _onCutSceneEnd; set => _onCutSceneEnd = value; }

        public CutSceneManager(GameObject cutSceneObject, ConfigCharacters characterManager,
            float pauseTime, float speedText, Image dialogImage, Text textArea)
        {
            _cutSceneObject = cutSceneObject;
            _characterManager = characterManager;
            _pauseTime = pauseTime;
            _speedText = speedText;
            _dialogImage = dialogImage;
            _textArea = textArea;

            _cutSceneObject.SetActive(false);
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

        public void InitializeCutScene(int selectedLevelId, ConfigCutScene configDialogLines)
        {
            _configDialogLines = configDialogLines;

            if (_configDialogLines != null)
            {
                StartNewDialog();
            }
            else
            {
                Debug.LogError($"Cut-scene configuration for level {selectedLevelId} not found.");
            }
        }

        private void StartNewDialog()
        {
            _cutSceneObject.SetActive(true);
            _currentLineIndex = 0;
            _letterIndex = 0;
            _isAnimatingText = true;
            _currentDialog = _configDialogLines.dialogLines[_currentLineIndex].text;
            _textArea.text = string.Empty;

            SetupCurrentDialogLine();
        }

        private void SetupCurrentDialogLine()
        {
            var currentDialogLine = _configDialogLines.dialogLines[_currentLineIndex];

            _dialogImage.sprite = currentDialogLine.sprite;
            _currentDialog = currentDialogLine.text;

            int selectedCharacterIndex = currentDialogLine.characterIndex;
            var selectedCharacter = _characterManager.characters[selectedCharacterIndex];

            _textArea.alignment = selectedCharacter.alignment;
            _textArea.color = selectedCharacter.textColor;

            _isWaitingForSpace = currentDialogLine.pauseResume > 1;
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
                    _timeUntilNextAction = _configDialogLines.dialogLines[_currentLineIndex].pauseResume;
                }
            }
        }

        private void CompleteTextAnimation()
        {
            _textArea.text = _currentDialog;
            _letterIndex = _currentDialog.Length;
            _isAnimatingText = false;
        }

        private void ContinueToNextLine()
        {
            _currentLineIndex++;
            if (_currentLineIndex >= _configDialogLines.dialogLines.Length)
            {
                EndCutScene();
            }
            else
            {
                SetupCurrentDialogLine();
                _letterIndex = 0;
                _isAnimatingText = true;
                _textArea.text = string.Empty;
            }
        }
        private void EndCutScene()
        {
            _cutSceneObject.SetActive(false);

            OnCutSceneEnd?.Invoke();
        }
    }
}
