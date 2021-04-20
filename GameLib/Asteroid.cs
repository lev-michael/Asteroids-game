using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public enum AsteroidSize { BIG = 120, MEDIUM = 60, SMALL = 30 }
    public enum AsteroidSpeed { FAST = 5, MEDIUM = 3, SLOW = 2 }
   

    public class Asteroid : Shape
    {     
        public int Speed { get; set; }
        public double Angle { get; set; }

        public AsteroidSize Radius { get; private set; }

        public Asteroid(AsteroidSize radius, double angle, double x, double y)
        {
            Radius = radius;
            setSizeAndSpeedOfAsteroid(radius);
            Angle = angle;
            X = x;
            Y = y;
        }

        private void setSizeAndSpeedOfAsteroid(AsteroidSize size)
        {
            switch (size)
            {
                case AsteroidSize.BIG:
                    Size = (int)AsteroidSize.BIG;
                    Speed = (int)AsteroidSpeed.SLOW;
                    break;
                case AsteroidSize.MEDIUM:
                    Size = (int)AsteroidSize.MEDIUM;
                    Speed = (int)AsteroidSpeed.MEDIUM;
                    break;
                case AsteroidSize.SMALL:
                    Size = (int)AsteroidSize.SMALL;
                    Speed = (int)AsteroidSpeed.FAST;
                    break;
            }
        }   
    }
}
