namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class MuteEchoPlayerDecorator : TimerPlayerDecorator
    {
        public MuteEchoPlayerDecorator(IPlayer player, float bonusDurationSeconds) : base(player, bonusDurationSeconds)
        {
            _player.IsEchoWorking = false;
        }

        protected override void RemoveBoost()
        {
            _player.IsEchoWorking = true;
        }
    }
}