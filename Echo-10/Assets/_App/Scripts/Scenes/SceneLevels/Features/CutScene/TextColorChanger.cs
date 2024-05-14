using _App.Scripts.Libs.Installer;
using TMPro;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class TextColorChanger : IUpdatable
    {
        private GameObject _cutSceneObject;
        private TextMeshProUGUI _textToChange;
        private Color32 _targetColor;

        private Color32 _originalColor;
        private bool _changeColors = false;

        private float _timeBetweenChanges = 0.1f;
        private float _elapsedTime = 0f;
        private int _currentLetterIndex = 0;
        private float _escapeHoldTime = 0f;

        public TextColorChanger(GameObject cutSceneObject, TextMeshProUGUI textToChange, Color32 targetColor)
        {
            _cutSceneObject = cutSceneObject;
            _textToChange = textToChange;
            _targetColor = targetColor;
            _originalColor = _textToChange.color;
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                _escapeHoldTime += Time.deltaTime;

                if (!_changeColors)
                {
                    _changeColors = true;
                    _currentLetterIndex = 0;
                    _elapsedTime = 0f;
                }

                if (_changeColors)
                {
                    _elapsedTime += Time.deltaTime;

                    if (_elapsedTime >= _timeBetweenChanges)
                    {
                        ChangeNextLetterColor();
                        _elapsedTime = 0f;
                    }
                }

                if (_currentLetterIndex >= _textToChange.textInfo.characterCount)
                {
                    if (_escapeHoldTime >= _timeBetweenChanges * _textToChange.textInfo.characterCount)
                    {
                        LoadNextScene();
                    }
                }
            }
            else
            {
                _escapeHoldTime = 0f;

                if (_changeColors)
                {
                    ResetLetterColors();
                    _changeColors = false;
                }
            }
        }

        private void ChangeNextLetterColor()
        {
            TMP_TextInfo textInfo = _textToChange.textInfo;
            if (_currentLetterIndex >= textInfo.characterCount) return;

            int materialIndex = textInfo.characterInfo[_currentLetterIndex].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[_currentLetterIndex].vertexIndex;
            Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            newVertexColors[vertexIndex + 0] = _targetColor;
            newVertexColors[vertexIndex + 1] = _targetColor;
            newVertexColors[vertexIndex + 2] = _targetColor;
            newVertexColors[vertexIndex + 3] = _targetColor;

            _textToChange.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            _currentLetterIndex++;
        }

        private void ResetLetterColors()
        {
            TMP_TextInfo textInfo = _textToChange.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                newVertexColors[vertexIndex + 0] = _originalColor;
                newVertexColors[vertexIndex + 1] = _originalColor;
                newVertexColors[vertexIndex + 2] = _originalColor;
                newVertexColors[vertexIndex + 3] = _originalColor;
            }

            _textToChange.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        private void LoadNextScene()
        {
            _cutSceneObject.SetActive(false);
        }
    }
}
    
