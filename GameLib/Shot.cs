using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Shot : Shape
    {
        private const int SHOT_SPEED = 20;
      
        public double EndX { get; set; }
        public double EndY { get; set; }
        public int Speed { get; set; }
        public double Angle { get; set; }


        public Shot(double x, double y, double movingAngle)
        {
            X = x;
            Y = y;
            Speed = SHOT_SPEED;
            Angle = movingAngle;
            EndX =  X - Math.Cos(Angle)*Speed;
            EndY = Y - Math.Sin(Angle)*Speed;
        }
    }
}
