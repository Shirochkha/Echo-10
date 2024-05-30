namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class AddEchoPlayerDecorator : BasePlayerDecorator
    {
        public AddEchoPlayerDecorator(IPlayer player, int count) : base(player)
        {
            _player.PlayerStateOnLevel.EchoCount += count;
        }
    }
}