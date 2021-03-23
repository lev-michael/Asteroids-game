using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemPrace
{
    enum AsteroidSize { BIG = 50, MEDIUM = 25, SMALL = 12 }
    enum AsteroidSpeed { FAST = 3, MEDIUM = 2, SLOW = 1 }
   

    class Asteroid : Shape
    {
        private Random random;

        public Asteroid(AsteroidSize size, double screenWidth, double screenHeight, double angle, double x, double y)
        {
            setSizeAndSpeedOfAsteroid(size);
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Angle = angle;
            X = x;
            Y = y;
        }
        public Asteroid(AsteroidSize size, double screenWidth, double screenHeight)
        {
            random = new Random();
            setSizeAndSpeedOfAsteroid(size);
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            generetaRandomCoordinates();
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

        private void generetaRandomCoordinates()
        {
            int startOrEndY = random.Next(0, 2);
            int startOrEndX = random.Next(0, 2);
            if (startOrEndY == 0)
            {
                if(startOrEndX == 0)
                {
                    X = ScreenWidth;
                    Y = random.Next(10, (int)ScreenHeight-10);
                    Angle = random.NextDouble() * (Math.PI / 2);
                } else
                {
                    X = 0;
                    Y = random.Next(10, (int)ScreenHeight-10);
                    Angle = random.NextDouble() * (Math.PI/2)+((2 * Math.PI)/3);
                }

            } else
            {
                if (startOrEndX == 0)
                {
                    Y = ScreenHeight;
                    X = random.Next(10, (int)ScreenWidth-10);
                    Angle = random.NextDouble() * (Math.PI);
                } else
                {
                    Y = 0;
                    X = random.Next(10, (int)ScreenWidth-10);
                    Angle = random.NextDouble() * (Math.PI) + Math.PI;
                }
            }
        }

        public bool MoveAsteroid()
        {
            X -= Math.Cos(Angle) * Speed;
            Y -= Math.Sin(Angle) * Speed;
            if (X-Size > ScreenWidth || X+Size < 0 || Y+Size < 0 || Y-Size > ScreenHeight)
                return true;

            return false;
        }
    }
}
