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
        private const int ROCKET_SPEED = 5;
        private const int ROCKET_SIZE = 30;
        private const int CADENCE = 400;

        public PointF[] points { get; private set; }
        public int Speed { get; set; }
        private bool Reloading { get; set; }

        private Timer ReloadingTimer = new System.Timers.Timer();

        public Rocket(double screenWidth, double screenHeight)
        {
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            X = screenWidth / 2;
            Y = screenHeight / 2;
            Speed = ROCKET_SPEED;
            Size = ROCKET_SIZE;
            Angle = Constants.HALF_OF_PI;
            ReloadingTimer.Interval = CADENCE;
            ReloadingTimer.Elapsed += ReloadingTimer_Elapsed;
            EvaluatePoints(); 
        }

        private void ReloadingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Reloading = false;
            ReloadingTimer.Stop();
        }

        public Rocket(double x, double y, double screenWidth, double screenHeight, int speed, int size)
        {
            this.X = x;
            this.Y = y;
            Size = size;
            Angle = Constants.HALF_OF_PI;
            Speed = speed;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            ReloadingTimer.Interval = CADENCE;
            EvaluatePoints();
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
    }
}
