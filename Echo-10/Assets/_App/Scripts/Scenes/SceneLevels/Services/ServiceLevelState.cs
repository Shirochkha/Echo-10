using Assets._App.Scripts.Infrastructure.CutScene.Config;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using System;
using System.Collections.Generic;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public class ServiceLevelState
    {
        private List<LevelState> _levelStates;
        private LevelStatePersistence _persistence;
        private ServiceLevelSelection _serviceLevelSelection;
        private ConfigLevel _configLevel;

        private ConfigObjects _configObjects;
        private ConfigCutScene _configCutScene;
        private bool _hasLevelCreate = false;
        public bool HasLevelCreate
        {
            get => _hasLevelCreate;
            set
            {
                if (_hasLevelCreate != value)
                {
                    _hasLevelCreate = value;
                    if (_hasLevelCreate)
                    {
                        LoadLevelObjects();
                    }
                }
            }
        }

        public ConfigObjects ConfigObjects => _configObjects;
        public ConfigCutScene ConfigCutScene => _configCutScene;

        public ServiceLevelState(ConfigLevel configLevel, LevelStatePersistence persistence, 
            ServiceLevelSelection serviceLevelSelection)
        {
            _persistence = persistence;
            _levelStates = _persistence.LoadLevelStates() ?? new List<LevelState>();
            _serviceLevelSelection = serviceLevelSelection;
            _configLevel = configLevel;

            if (_levelStates.Count == 0)
            {
                foreach (var level in _configLevel.levels)
                {
                    _levelStates.Add(new LevelState { id = level.id, isWin = false });
                }
                _persistence.SaveLevelStates(_levelStates);
            }
        }

        public void SetLevelWin()
        {
            foreach (var levelState in _levelStates)
            {
                if (levelState.id == _serviceLevelSelection.SelectedLevelId)
                {
                    levelState.isWin = true;
                    _persistence.SaveLevelStates(_levelStates);
                    return;
                }
            }
        }

        public bool IsLevelWin(int levelId)
        {
            foreach (var levelState in _levelStates)
            {
                if (levelState.id == levelId)
                {
                    return levelState.isWin;
                }
            }
            return false;
        }

        private void LoadLevelObjects()
        {
            if (_serviceLevelSelection.SelectedLevelId > 0)
            {
                foreach (var level in _configLevel.levels)
                {
                    if (level.id == _serviceLevelSelection.SelectedLevelId)
                    {
                        _configObjects = level.configObjects;
                        _configCutScene = level.cutScene;
                        break;
                    }
                }
            }
        }
    }

    [Serializable]
    public class LevelState
    {
        public int id;
        public bool isWin;
    }
}
