using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SemPrace
{
    class Rocket : Shape
    {
        public PointF[] points;

        public Rocket()
        {
            evaluatePoints();
        }

        public Rocket(double x, double y, double screenWidth, double screenHeight, int speed, int size)
        {
            this.X = x;
            this.Y = y;
            Size = size;
            Angle = Math.PI / 2;
            Speed = speed;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            evaluatePoints();

        }

        private void evaluatePoints()
        {
            points = new PointF[4];
            points[0] = new PointF((float)(X - Size * Math.Cos(Angle + 0 * 2 * Math.PI / 3)), (float)(Y - Size * Math.Sin(Angle + 0 * 2 * Math.PI / 3)));
            points[1] = new PointF((float)(X - Size * Math.Cos(Angle + 1 * 2 * Math.PI / 3)), (float)(Y - Size * Math.Sin(Angle + 1 * 2 * Math.PI / 3)));
            points[2] = new PointF((float)X, (float)Y);
            points[3] = new PointF((float)(X - Size * Math.Cos(Angle + 2 * 2 * Math.PI / 3)), (float)(Y - Size * Math.Sin(Angle + 2 * 2 * Math.PI / 3)));
        }

        public void Rotate(int angle)
        {
            Angle += ToRadians(angle);
            double radAngle = ToRadians(angle);
            float x1 = (float)(((points[0].X - X) * Math.Cos(radAngle)) - ((points[0].Y - Y) * Math.Sin(radAngle)) + X);
            float x2 = (float)(((points[1].X - X) * Math.Cos(radAngle)) - ((points[1].Y - Y) * Math.Sin(radAngle)) + X);
            float x3 = (float)(((points[3].X - X) * Math.Cos(radAngle)) - ((points[3].Y - Y) * Math.Sin(radAngle)) + X);
            points[0].Y = (float)((points[0].Y - Y) * Math.Cos(radAngle) + (points[0].X - X) * Math.Sin(radAngle) + Y);
            points[1].Y = (float)((points[1].Y - Y) * Math.Cos(radAngle) + (points[1].X - X) * Math.Sin(radAngle) + Y);
            points[2].Y = (float)this.Y;
            points[3].Y = (float)((points[3].Y - Y) * Math.Cos(radAngle) + (points[3].X - X) * Math.Sin(radAngle) + Y);
            points[0].X = x1;
            points[1].X = x2;
            points[2].X = (float)this.X;
            points[3].X = x3;
        }

        public void MoveRocket()
        {
            double directionX = Math.Cos(Angle);
            double directionY = Math.Sin(Angle);
            double velocityX = directionX * Speed;
            double velocityY = directionY * Speed;
            X -= velocityX;
            Y -= velocityY;
            if (Y - Size < 0)
            {
                Y += velocityY;
            }
            if (X - Size < 0)
            {
                X += velocityX;
            }
            if (Y + Size > ScreenHeight)
            {
                Y += velocityY;
            }
            if (X + Size > ScreenWidth)
            {
                X += velocityX;
            }
            evaluatePoints();
        }

        public Shot Shoot()
        {
            PointF p = getTopOfTheRocket();
            return new Shot(p.X, p.Y, ScreenWidth, ScreenHeight, 12, Angle);
        }


        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private PointF getTopOfTheRocket()
        {
            return points[0];
        }
    }
}
