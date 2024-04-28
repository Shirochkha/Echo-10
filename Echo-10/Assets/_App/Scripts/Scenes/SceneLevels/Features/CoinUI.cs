using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class CoinUI
    {
        private Text _coinCountText;

        public CoinUI(Text coinCountText)
        {
            _coinCountText = coinCountText;
        }

        public void UpdateCoinCountUI(int coinCount, int coinMaxCount)
        {
            _coinCountText.text = "Мошкара: " + coinCount.ToString() + "/" + coinMaxCount;
        }
    }
}
