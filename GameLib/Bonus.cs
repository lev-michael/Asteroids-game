using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace GameLib
{
    public delegate void BonusHandler();

    public enum BonusType { NONE=0, SPEED=1, SHIELD=2, TRIPPLE_SHOT=3}
    public class Bonus: Shape
    {

        public event BonusHandler BonusExpiredEvent;

        private Timer timer;
        public BonusType Type { get; private set; }

        public Bonus(BonusType type, double x, double y)
        {
            X = x;
            Y = y;
            Type = type;
            Size = Constants.BONUS_SIZE;
            timer  = new System.Timers.Timer();
            timer.Interval = Constants.THIRTY_SEC;
            timer.Start();
            timer.Elapsed += OnTimer_Elapsed;
        }

        private void OnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BonusExpiredEvent?.Invoke();
        }

        public bool IsBonusActive()
        {
            return Type != BonusType.NONE;
        }

        public void DisableBonus()
        {
            Type = BonusType.NONE;
        }

    }
}
