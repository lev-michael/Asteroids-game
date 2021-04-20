using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;

namespace GameLib
{
    public enum Direction { RIGHT = 2, LEFT = -2 };

    public class Rocket : Shape
    {
        private const int ROCKET_NORMAL_SPEED = 5;
        private const int ROCKET_HIGHER_SPEED = 8;
        private const int ROCKET_SIZE = 30;
        private const int CADENCE = 400;

        public PointF[] points { get; private set; }
        public int Speed { get; set; }
        private bool Reloading { get; set; }
        public double Angle { get; set; }
        private Timer ReloadingTimer = new System.Timers.Timer();
        private Timer BonusExpirationTimer = new System.Timers.Timer();


        private BonusType bonus;

        public BonusType Bonus
        {
            get { return bonus; }
            set {
                if (value == BonusType.SPEED)
                {
                    Speed = ROCKET_HIGHER_SPEED;
                } else
                {
                    Speed = ROCKET_NORMAL_SPEED;
                }
                bonus = value; 
            }
        }


        public Rocket(double x, double y)
        {
            this.X = x;
            this.Y = y;
            bonus = BonusType.NONE ;
            Angle = Constants.HALF_OF_PI;
            Speed = ROCKET_NORMAL_SPEED;
            Size = ROCKET_SIZE;
            ReloadingTimer.Interval = CADENCE;
            ReloadingTimer.Elapsed += ReloadingTimer_Elapsed;
            BonusExpirationTimer.Interval = Constants.FIFTEEN_SEC;
            BonusExpirationTimer.Elapsed += BonusExpirationTimer_Elapsed;
            EvaluatePoints();
        }

        private void BonusExpirationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Bonus = BonusType.NONE;
            BonusExpirationTimer.Stop();
        }

        private void ReloadingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Reloading = false;
            ReloadingTimer.Stop();
        }

        public void StartBonusTimer()
        {
            BonusExpirationTimer.Start();
        }

        public PointF this[int index]
        {
            get
            {
                if (index < 0 || index > 3)
                {
                    throw new IndexOutOfRangeException();
                }
                return points[index];
            }
        }

        public Shot Shoot()
        {
            if (!Reloading)
            {
                PointF p = GetTopOfTheRocket();
                Reloading = true;
                ReloadingTimer.Start();
                return new Shot(p.X, p.Y, Angle);
            }
            return null;
        }

        public Shot[] TripleShoot()
        {
            if (!Reloading)
            {
                PointF p = GetTopOfTheRocket();
                Reloading = true;
                ReloadingTimer.Start();
                return new Shot[] { 
                    new Shot(p.X, p.Y, Angle), 
                    new Shot(p.X, p.Y, Angle+Constants.QUARTER_OF_PI), 
                    new Shot(p.X, p.Y, Angle- Constants.QUARTER_OF_PI) 
                };
            }
            return null;
        }

        public void EvaluatePoints()
        {
            points = new PointF[4];
            points[0] = new PointF((float)(X - Size * Math.Cos(Angle + 0 * Constants.TWO_THIRDS_OF_PI)), (float)(Y - Size * Math.Sin(Angle + 0 * Constants.TWO_THIRDS_OF_PI)));
            points[1] = new PointF((float)(X - Size * Math.Cos(Angle + 1 * Constants.TWO_THIRDS_OF_PI)), (float)(Y - Size * Math.Sin(Angle + 1 * Constants.TWO_THIRDS_OF_PI)));
            points[2] = new PointF((float)X, (float)Y);
            points[3] = new PointF((float)(X - Size * Math.Cos(Angle + 2 * Constants.TWO_THIRDS_OF_PI)), (float)(Y - Size * Math.Sin(Angle + 2 * Constants.TWO_THIRDS_OF_PI)));
        }

        private PointF GetTopOfTheRocket()
        {
            return points[0];
        }

        public void DisableBonus()
        {
            Bonus = BonusType.NONE;
            BonusExpirationTimer.Stop();
        }

        public bool HasBonus()
        {
            return Bonus != BonusType.NONE;
        }
        public bool HasShiled()
        {
            return Bonus == BonusType.SHIELD;
        }

        public bool HasSpeed()
        {
            return Bonus == BonusType.SPEED;
        }

        public bool HasTripleShot()
        {
            return Bonus == BonusType.TRIPPLE_SHOT;
        }
    }
}
