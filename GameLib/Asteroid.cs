using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    internal enum Position { TOP, BOTTOM, LEFT, RIGHT }

    public class Asteroid : Shape
    {
        private Random random = new Random();
        public int Speed { get; set; }
        public double Angle { get; set; }

        public SizeType Radius { get; private set; }

        public Asteroid(SizeType radius, double angle, double x, double y)
        {
            Radius = radius;
            SetSizeAndSpeedOfAsteroid(radius);
            Angle = angle;
            X = x;
            Y = y;
        }

        public Asteroid(double width, double height)
        {
            var asteroidSizes = Enum.GetValues(typeof(SizeType));
            Radius = (SizeType) asteroidSizes.GetValue(random.Next(asteroidSizes.Length));
            SetSizeAndSpeedOfAsteroid(Radius);
            GenerateRandomAngleAndOnEdgesOfScreen((int)width, (int)height);
        }

        private void SetSizeAndSpeedOfAsteroid(SizeType size)
        {
            switch (size)
            {
                case SizeType.BIG:
                    Size = (int)SizeType.BIG;
                    Speed = (int)SpeedType.SLOW;
                    break;
                case SizeType.MEDIUM:
                    Size = (int)SizeType.MEDIUM;
                    Speed = (int)SpeedType.MEDIUM;
                    break;
                case SizeType.SMALL:
                    Size = (int)SizeType.SMALL;
                    Speed = (int)SpeedType.FAST;
                    break;
            }
        }
        
        private void GenerateRandomAngleAndOnEdgesOfScreen(int width, int height)
        {
            var positiones = Enum.GetValues(typeof(Position));
            Position position = (Position)positiones.GetValue(random.Next(positiones.Length));
            switch (position)
            {
                case Position.BOTTOM:
                    Y = height + (int)Size;
                    X = random.Next((int)Size, width - (int)Size);
                    if (X < width / 2)
                    {
                        Angle = random.NextDouble() + 1.5;
                    }
                    else
                    {
                        Angle = random.NextDouble() + 0.5;
                    }
                    break;
                case Position.LEFT:
                    X = -(int)Size;
                    Y = random.Next((int)Size, height - (int)Size);
                    if (Y < height / 2)
                    {
                        Angle = random.NextDouble() * 0.5 + 3;
                    }
                    else
                    {
                        Angle = random.NextDouble() * 0.5 + 2.5;
                    }
                    break;
                case Position.RIGHT:
                    X = width + (int)Size;
                    Y = random.Next((int)Size, height - (int)Size);
                    if (Y < height / 2)
                    {
                        Angle = random.NextDouble() - 1;
                    }
                    else
                    {
                        Angle = random.NextDouble();
                    }
                    break;
                case Position.TOP:
                    Y = -(int)Size;
                    X = random.Next((int)Size, width - (int)Size);
                    if (X < height / 2)
                    {
                        Angle = random.NextDouble() + 3.7;
                    }
                    else
                    {
                        Angle = random.NextDouble() + 5;
                    }
                    break;
            }  
        }
    }
}
