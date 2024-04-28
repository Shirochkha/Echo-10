using Assets._App.Scripts.Scenes.SceneLevels.Features;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemAddCoin
    {
        private int _coinCount;
        private int _coinMaxCount;
        private CoinUI _coinUI;

        public SystemAddCoin(int coinCount, int coinMaxCount, CoinUI coinUI)
        {
            _coinCount = coinCount;
            _coinMaxCount = coinMaxCount;
            _coinUI = coinUI;
            _coinUI.UpdateCoinCountUI(_coinCount, coinMaxCount);
        }
            
        public void AddCoins(int amount)
        {
            _coinCount = (_coinCount < _coinMaxCount) ? _coinCount + amount : _coinMaxCount;
            _coinUI.UpdateCoinCountUI(_coinCount, _coinMaxCount);
        }
    }
}
