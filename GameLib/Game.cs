using GameLib.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GameLib
{
    public class Game
    {
        private Random random;
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public int Score { get; private set; }

        public Rocket Rocket { get; private set; }
        private List<Shot> Shots;
        private List<Asteroid> Asteroids;
        private List<Explosion> Explosions;
        private Bonus Bonus;

        public Game(double width, double height)
        {
            random = new Random();
            ScreenHeight = height;
            ScreenWidth = width;
            Asteroids = new List<Asteroid>();
            Shots = new List<Shot>();
            Explosions = new List<Explosion>();
            Bonus = new Bonus(BonusType.NONE, 0, 0);
            Bonus.BonusExpiredEvent += Bonus_BonusExpiredEvent;
        }

        private void Bonus_BonusExpiredEvent()
        {
            if (IsBonusSpawned())
            {
                Bonus.DisableBonus();
            } else
            {
                SpawnBonus();
            }
        }

        public void StartGame()
        {
            Score = 0;
            this.Rocket = new Rocket(ScreenWidth / 2, ScreenHeight / 2);
        }

        public void DetectShotCollisionWithAsteroid()
        {
            if (Shots.Count > 0 && Asteroids.Count > 0)
            {
                for (int i = 0; i < Shots.Count; i++)
                {
                    Shot shot = Shots[i];
                    Asteroid a;
                    for (int j = 0; j < Asteroids.Count; j++)
                    {
                        a = Asteroids[j];
                        if (Math.Pow(shot.EndX - a.X, 2) + Math.Pow(shot.EndY - a.Y, 2) <= Math.Pow(a.Size, 2))
                        {
                            Shots.Remove(shot);
                            Score += 10;
                            Asteroids.Remove(a);
                            Explosions.Add(new Explosion(a.X, a.Y, (int)a.Size));
                            if (a.Size == (double)AsteroidSize.MEDIUM)
                            {
                                Asteroids.Add(new Asteroid(AsteroidSize.SMALL, a.Angle + Constants.QUARTER_OF_PI, a.X, a.Y));
                                Asteroids.Add(new Asteroid(AsteroidSize.SMALL, a.Angle - Constants.QUARTER_OF_PI, a.X, a.Y));
                            }
                            else if (a.Size == (double)AsteroidSize.BIG)
                            {
                                Asteroids.Add(new Asteroid(AsteroidSize.MEDIUM, a.Angle + Constants.QUARTER_OF_PI, a.X, a.Y));
                                Asteroids.Add(new Asteroid(AsteroidSize.MEDIUM, a.Angle - Constants.QUARTER_OF_PI, a.X, a.Y));
                            }
                        }
                    }
                }
            }
        }

        public void DetectBonusCollisionWithRocket()
        {
            if(Rocket!= null && Bonus.IsBonusActive())
            {
                if (IsVertexOfRocketIntersectingCircle(Bonus.X, Bonus.Y, Bonus.Size, Rocket.X, Rocket.Y))
                {
                    Rocket.Bonus = Bonus.Type;
                    Bonus.DisableBonus();
                    Rocket.StartBonusTimer();
                }
            }
        }

        public bool IsRocketCollisionDetected()
        {
            if (Rocket != null && Asteroids.Count > 0)
            {
                Asteroid a;
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    a = Asteroids[i];
                    if (IsRocketColiedWithAsteroid(a, Rocket.points))
                    {
                        if (Rocket.HasShiled())
                        {
                            Asteroids.Remove(a);
                            Rocket.DisableBonus();
                            return false;
                        }
                        EndGame();
                        return true;
                    }
                }
            }
            return false;
        }

        public void SpawnBonus()
        {
            int randType = random.Next(1, 4);
            int randX = random.Next((int)Bonus.Size, (int) (ScreenWidth - Bonus.Size));
            int randY = random.Next((int)Bonus.Size, (int)(ScreenHeight- Bonus.Size));
            BonusType type = BonusType.NONE;
            switch (randType)
            {
                case 1:
                    type = BonusType.SHIELD;
                    break;
                case 2:
                    type = BonusType.SPEED;
                    break;
                case 3:
                    type = BonusType.TRIPPLE_SHOT;
                    break;
            }
            Bonus = new Bonus(type, randX, randY);
        }

        private bool IsRocketColiedWithAsteroid(Asteroid a, PointF[] rocketPoints)
        {
            PointF startPoint = rocketPoints[rocketPoints.Length - 1];
            PointF endPoint;
            for (int i = 0; i < rocketPoints.Length; i++)
            {
                if (i == 2)
                    continue;

                endPoint = rocketPoints[i];

                if (IsVertexOfRocketIntersectingCircle(a.X, a.Y, a.Size, startPoint.X, startPoint.Y))
                    return true;

                if (IsEdgeOfRocketIntersectingCircle(a.X, a.Y, a.Size, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y))
                    return true;

                startPoint = endPoint;
            }
            return false;
        }

        private bool IsVertexOfRocketIntersectingCircle(double cirlceCenterX, double cirlceCenterY, double radius, double pointX, double pointY)
        {
            double c1x = cirlceCenterX - pointX;
            double c1y = cirlceCenterY - pointY;
            double test = Math.Pow(c1x, 2) + Math.Pow(c1y, 2) - Math.Pow(radius, 2);
            if (test <= 0)
            {
                return true;
            }
            return false;
        }

        private bool IsEdgeOfRocketIntersectingCircle(double cirlceCenterX, double cirlceCenterY, double radius, double startPointX, double startPointY, double endPointX, double endPointY)
        {
            double c1x = cirlceCenterX - startPointX;
            double c1y = cirlceCenterY - startPointY;
            double e1x = endPointX - startPointX;
            double e1y = endPointY - startPointY;
            double k = c1x * e1x + c1y * e1y;

            if (k > 0)
            {
                double len = Math.Sqrt(Math.Pow(e1x, 2) + Math.Pow(e1y, 2));
                k /= len;

                if (k < len)
                {
                    if (Math.Pow(c1x, 2) + Math.Pow(c1y, 2) - Math.Pow(k, 2) <= radius)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MoveRocket()
        {
            if (Rocket != null)
            {
                double directionX = Math.Cos(Rocket.Angle);
                double directionY = Math.Sin(Rocket.Angle);
                double velocityX = directionX * Rocket.Speed;
                double velocityY = directionY * Rocket.Speed;
                Rocket.X -= velocityX;
                Rocket.Y -= velocityY;
                if (Rocket.Y - Rocket.Size < 0)
                {
                    Rocket.Y += velocityY;
                }
                if (Rocket.X - Rocket.Size < 0)
                {
                    Rocket.X += velocityX;
                }
                if (Rocket.Y + Rocket.Size > ScreenHeight)
                {
                    Rocket.Y += velocityY;
                }
                if (Rocket.X + Rocket.Size > ScreenWidth)
                {
                    Rocket.X += velocityX;
                }
                Rocket.EvaluatePoints();
            }
        }

        public void Shoot()
        {
            if (Rocket != null)
            {
                if (Rocket.HasTripleShot())
                {
                    Shot[] s = Rocket.TripleShoot();
                    if (s != null)
                        Shots.AddRange(s);
                } else
                {
                    Shot s = Rocket.Shoot();
                    if (s != null)
                        Shots.Add(s);
                }
                
                
            }

        }


        public void RotateRocket(Direction direction)
        {
            if (Rocket != null)
            {
                double radAngle = ToRadians((double)direction);
                Rocket.Angle += radAngle;
                PointF p;
                for (int i = 0; i < Rocket.points.Length; i++)
                {
                    if (i == 2)
                        continue;

                    p = Rocket.points[i];
                    p.X = (float)(((p.X - Rocket.X) * Math.Cos(radAngle)) - ((p.Y - Rocket.Y) * Math.Sin(radAngle)) + Rocket.X);
                    p.Y = (float)((p.Y - Rocket.Y) * Math.Cos(radAngle) + (p.X - Rocket.X) * Math.Sin(radAngle) + Rocket.Y);
                }
                Rocket.EvaluatePoints();
            }

        }
        public void EndGame()
        {
            Asteroids.Clear();
            Shots.Clear();
            Rocket = null;
        }

        public void GenerateAsteroid()
        {
            AsteroidSize size;
            double asteroidX, asteroidY, asteroidAngle;

            int randNum = random.Next(0, 12);
            if (randNum < 2)
            {
                size = AsteroidSize.SMALL;
            }
            else if (randNum < 5)
            {
                size = AsteroidSize.MEDIUM;
            }
            else
            {
                size = AsteroidSize.BIG;
            }

            if (randNum < 3)
            {
                asteroidX = ScreenWidth + (int)size;
                asteroidY = random.Next((int)size, (int)ScreenHeight - (int)size);
                if (asteroidY < ScreenHeight / 2)
                {
                    asteroidAngle = random.NextDouble() - 1;
                }
                else
                {
                    asteroidAngle = random.NextDouble();
                }

            }
            else if (randNum < 6)
            {
                asteroidX = -(int)size;
                asteroidY = random.Next((int)size, (int)ScreenHeight - (int)size);
                if (asteroidY < ScreenHeight / 2)
                {
                    asteroidAngle = random.NextDouble() * 0.5 + 3;
                }
                else
                {
                    asteroidAngle = random.NextDouble() * 0.5 + 2.5;
                }
            }
            else if (randNum < 9)
            {
                asteroidY = ScreenHeight + (int)size;
                asteroidX = random.Next((int)size, (int)ScreenWidth - (int)size);
                if (asteroidX < ScreenWidth / 2)
                {
                    asteroidAngle = random.NextDouble() + 1.5;
                }
                else
                {
                    asteroidAngle = random.NextDouble() + 0.5;
                }
            }
            else
            {
                asteroidY = -(int)size;
                asteroidX = random.Next((int)size, (int)ScreenWidth - (int)size);
                if (asteroidX < ScreenHeight / 2)
                {
                    asteroidAngle = random.NextDouble() + 3.7;
                }
                else
                {
                    asteroidAngle = random.NextDouble() + 5;
                }
            }
            Asteroids.Add(new Asteroid(size, asteroidAngle, asteroidX, asteroidY));

        }




        public void RenderGame(Graphics canvas)
        {
            if (Rocket == null)
                throw new NullReferenceException();

            canvas.DrawPolygon(Pens.White, Rocket.points);

            if (Rocket.HasShiled())
            {
                canvas.DrawEllipse(Pens.Aqua, (float)(Rocket.X - Rocket.Size), (float)(Rocket.Y - Rocket.Size), (float)Rocket.Size * 2, (float)Rocket.Size * 2);
            }

            if (Shots.Count > 0)
            {
                Shot shot;
                for (int i = 0; i < Shots.Count; i++)
                {
                    shot = Shots[i];
                    canvas.DrawLine(new Pen(Color.Red, 5), (float)shot.X, (float)shot.Y, (float)shot.EndX, (float)shot.EndY);
                    Shots[i] = MoveShot(shot);

                    if (shot.EndX > ScreenWidth || shot.EndX < 0 || shot.EndY < 0 || shot.EndY > ScreenHeight)
                        Shots.Remove(shot);
                }
            }
            if (Explosions.Count > 0)
            {
                Explosion e;
                for (int i = 0; i < Explosions.Count; i++)
                {
                    e = Explosions[i];
                    Image image;
                    switch (e.Counter++)
                    {
                        case 0:
                            using (MemoryStream ms = new MemoryStream(Resources.explosion1))
                            {
                                image = Image.FromStream(ms);
                            }
                            canvas.DrawImage(image, (int)e.X, (int)e.Y);
                            break;
                        case 1:
                            using (MemoryStream ms = new MemoryStream(Resources.explosion2))
                            {
                                image = Image.FromStream(ms);
                            }
                            canvas.DrawImage(image, (int)e.X, (int)e.Y);
                            break;
                        case 2:
                            using (MemoryStream ms = new MemoryStream(Resources.explosion3))
                            {
                                image = Image.FromStream(ms);
                            }
                            canvas.DrawImage(image, (int)e.X, (int)e.Y);
                            Explosions.Remove(e);
                            break;
                    }
                }
            }

            if (Asteroids.Count > 0)
            {
                Asteroid a;
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    a = Asteroids[i];
                    canvas.DrawEllipse(Pens.White, (float)(a.X - a.Size), (float)(a.Y - a.Size), (float)(2 * a.Size), (float)(2 * a.Size));

                    Asteroids[i] = MoveAsteroid(a);
                    if (a.X - a.Size > ScreenWidth || a.X + a.Size < 0 || a.Y + a.Size < 0 || a.Y - a.Size > ScreenHeight)
                        Asteroids.Remove(a);
                }
            }
            if(Bonus.IsBonusActive())
            {
                Image img = null;
                switch (Bonus.Type)
                {
                    case BonusType.SHIELD:
                        using (MemoryStream ms = new MemoryStream(Resources.shield))
                        {
                            img = Image.FromStream(ms);
                        }
                        break;
                    case BonusType.SPEED:
                        using (MemoryStream ms = new MemoryStream(Resources.lighting))
                        {
                            img = Image.FromStream(ms);
                        }
                        break;
                    case BonusType.TRIPPLE_SHOT:
                        using (MemoryStream ms = new MemoryStream(Resources.sword))
                        {
                            img = Image.FromStream(ms);
                        }
                        break;
                }
                canvas.DrawImage(img, (int) (Bonus.X - Bonus.Size/2), (int) (Bonus.Y - Bonus.Size/2));
                Pen pen = new Pen(Color.Yellow, 4);
                canvas.DrawEllipse(pen, (float)(Bonus.X - Bonus.Size/2)-10, (float)(Bonus.Y - Bonus.Size/2)-10, (float)Bonus.Size*2, (float)Bonus.Size*2);
            }
        }

        public bool IsBonusSpawned()
        {
            return Bonus.IsBonusActive();
        }


        private Shot MoveShot(Shot shot)
        {
            shot.X -= Math.Cos(shot.Angle) * shot.Speed;
            shot.Y -= Math.Sin(shot.Angle) * shot.Speed;
            shot.EndX = shot.X - Math.Cos(shot.Angle) * shot.Speed;
            shot.EndY = shot.Y - Math.Sin(shot.Angle) * shot.Speed;
            return shot;
        }

        private Asteroid MoveAsteroid(Asteroid a)
        {
            a.X -= Math.Cos(a.Angle) * a.Speed;
            a.Y -= Math.Sin(a.Angle) * a.Speed;
            return a;
        }

        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
