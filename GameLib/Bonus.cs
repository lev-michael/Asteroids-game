using System;
using System.Timers;

namespace GameLib
{
    public delegate void BonusHandler();

    public class Bonus: Shape
    {

        public event BonusHandler BonusExpiredEvent;
        private Timer timer;
        private Random random = new Random();
        public BonusType Type { get; private set; }

        public Bonus(double width, double height)
        {
            Size = Constants.BONUS_SIZE;
            X = random.Next((int)Size, (int)(width - Size));
            Y = random.Next((int) Size, (int)(height - Size));
            Type = GetRandomType();
            timer = new Timer
            {
                Interval = random.Next(Constants.THIRTY_SEC, Constants.ONE_MINUTE)
            };
            timer.Start();
            timer.Elapsed += OnTimer_Elapsed;
        }

        private BonusType GetRandomType()
        {
            var bonusTypes = Enum.GetValues(typeof(BonusType));
            return (BonusType)bonusTypes.GetValue(random.Next(bonusTypes.Length));
        }

        public Bonus(BonusType type, double x, double y) : this(x,y)
        {
            Type = type;
            X = x;
            Y = y;
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
