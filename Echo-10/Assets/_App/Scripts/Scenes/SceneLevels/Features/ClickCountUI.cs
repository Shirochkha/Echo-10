using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class ClickCountUI : IUpdatable
    {
        private Text _clickCountText;
        private SystemColliderRadiusChange _colliderRadiusChange;

        public ClickCountUI(Text clickCountText, SystemColliderRadiusChange colliderRadiusChange)
        {
            _clickCountText = clickCountText;
            _colliderRadiusChange = colliderRadiusChange;
        }

        public void Update()
        {
            _clickCountText.text = _colliderRadiusChange.ClickCount.ToString();
        }
    }

}
