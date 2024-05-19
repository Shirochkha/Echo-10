using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using System;
using System.Collections.Generic;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public class ServiceLevelState
    {
        private List<LevelState> _levelStates;
        private LevelStatePersistence _persistence;

        public ServiceLevelState(ConfigLevel configLevel, LevelStatePersistence persistence)
        {
            _persistence = persistence;
            _levelStates = _persistence.LoadLevelStates() ?? new List<LevelState>();

            if (_levelStates.Count == 0)
            {
                foreach (var level in configLevel.levels)
                {
                    _levelStates.Add(new LevelState { id = level.id, isWin = false });
                }
                _persistence.SaveLevelStates(_levelStates);
            }
        }

        public void SetLevelWin(int levelId)
        {
            foreach (var levelState in _levelStates)
            {
                if (levelState.id == levelId)
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
    }

    [Serializable]
    public class LevelState
    {
        public int id;
        public bool isWin;
    }
}
