using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Shot : Shape
    {
        public double EndX { get; private set; }
        public double EndY { get; private set; }
        public int Speed { get; set; }

        public Shot(double x, double y, double screenWidth, double screenHeight, int speed, double angle)
        {
            X = x;
            Y = y;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Speed = speed;
            Angle = angle;
            EndX =  X - Math.Cos(Angle)*Speed;
            EndY = Y - Math.Sin(Angle)*Speed;
        }

        public bool MoveShot()
        {
            X -= Math.Cos(Angle) * Speed;
            Y -= Math.Sin(Angle) * Speed;
            EndX = X - Math.Cos(Angle) * Speed;
            EndY = Y - Math.Sin(Angle) * Speed;
            if (EndX > ScreenWidth || EndX < 0 || EndY < 0 || EndY > ScreenHeight)
                return true;

            return false;
        }
    }
}
