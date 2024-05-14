using Assets._App.Scripts.Scenes.SceneLevels.Features;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemAddCoin
    {
        private int _coinCount;
        private int _coinMaxCount;
        private CoinUI _coinUI;

        public int CoinCount { get => _coinCount; set => _coinCount = value; }
        public int CoinMaxCount { get => _coinMaxCount; set => _coinMaxCount = value; }

        public SystemAddCoin(int coinCount, int coinMaxCount, CoinUI coinUI)
        {
            CoinCount = coinCount;
            CoinMaxCount = coinMaxCount;
            _coinUI = coinUI;
            _coinUI.UpdateCoinCountUI(CoinCount, coinMaxCount);
        }

        public void AddCoins(int amount)
        {
            CoinCount = (CoinCount < CoinMaxCount) ? CoinCount + amount : CoinMaxCount;
            _coinUI.UpdateCoinCountUI(CoinCount, CoinMaxCount);
        }
    }
}
