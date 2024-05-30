namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class SpeedPlayerDecorator : TimerPlayerDecorator
    {
        private readonly float _bonusForwardSpeed;

        public SpeedPlayerDecorator(IPlayer player, float bonusDurationSeconds, float bonusForwardSpeed) : base(player, bonusDurationSeconds)
        {
            _bonusForwardSpeed = bonusForwardSpeed;
            player.ForwardSpeed += _bonusForwardSpeed;
        }

        protected override void RemoveBoost()
        {
            _player.ForwardSpeed -= _bonusForwardSpeed;
        }
    }
}