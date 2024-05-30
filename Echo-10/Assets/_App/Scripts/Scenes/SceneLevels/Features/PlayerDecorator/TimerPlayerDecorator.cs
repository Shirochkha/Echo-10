using System;
using System.Timers;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public abstract class TimerPlayerDecorator : BasePlayerDecorator
    {
        protected readonly Timer _boostTimer;

        protected TimerPlayerDecorator(IPlayer player, float bonusDurationSeconds) : base(player)
        {
            _boostTimer = new Timer(bonusDurationSeconds * 1000);
            _boostTimer.Elapsed += BoostTimer_Elapsed;
            _boostTimer.Start();
        }

        private void BoostTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RemoveBoost();
            _boostTimer.Stop();
        }

        protected abstract void RemoveBoost();
    }
}