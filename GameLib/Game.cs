using GameLib.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GameLib
{
    public delegate void EndGameHandler();

    public class Game
    {
        public event EndGameHandler EndGameEvent;

        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public int Score { get; private set; }

        public Rocket Rocket { get; private set; }
        private IList<Shot> Shots;
        private IList<Asteroid> Asteroids;
        private IList<Explosion> Explosions;
        private Bonus Bonus;

        public Game(double width, double height)
        {
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
                    Asteroid asteroid;
                    for (int j = 0; j < Asteroids.Count; j++)
                    {
                        asteroid = Asteroids[j];
                        if (Math.Pow(shot.EndX - asteroid.X, 2) + Math.Pow(shot.EndY - asteroid.Y, 2) <= Math.Pow(asteroid.Size, 2))
                        {
                            Shots.Remove(shot);
                            Score += 10;
                            Asteroids.Remove(asteroid);
                            Explosions.Add(new Explosion(asteroid.X, asteroid.Y, (int)asteroid.Size));
                            if (asteroid.Size == (double)SizeType.MEDIUM)
                            {
                                Asteroids.Add(new Asteroid(SizeType.SMALL, asteroid.Angle + Constants.QUARTER_OF_PI, asteroid.X, asteroid.Y));
                                Asteroids.Add(new Asteroid(SizeType.SMALL, asteroid.Angle - Constants.QUARTER_OF_PI, asteroid.X, asteroid.Y));
                            }
                            else if (asteroid.Size == (double)SizeType.BIG)
                            {
                                Asteroids.Add(new Asteroid(SizeType.MEDIUM, asteroid.Angle + Constants.QUARTER_OF_PI, asteroid.X, asteroid.Y));
                                Asteroids.Add(new Asteroid(SizeType.MEDIUM, asteroid.Angle - Constants.QUARTER_OF_PI, asteroid.X, asteroid.Y));
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

        public void DetectRocketAsteroidCollision()
        {
            if (Rocket != null && Asteroids.Count > 0)
            {
                Asteroid asteroid;
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    asteroid = Asteroids[i];
                    if (IsRocketColiedWithAsteroid(asteroid, Rocket.points))
                    {
                        if (Rocket.HasShiled)
                        {
                            Asteroids.Remove(asteroid);
                            Rocket.DisableBonus();
                            return;
                        }
                        EndGame();
                    }
                }
            }
        }

        public void SpawnBonus()
        {
            Bonus = new Bonus(ScreenWidth, ScreenHeight);
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
                if (Rocket.HasTripleShot)
                {
                    Shot[] shots = Rocket.TripleShoot();
                    if (shots != null)
                    {
                        for (int i = 0; i < shots.Length; i++)
                        {
                            Shots.Add(shots[i]);
                        }
                    }
                } else
                {
                    Shot shot = Rocket.Shoot();
                    if (shot != null)
                        Shots.Add(shot);
                }  
            }
        }


        public void RotateRocket(Direction direction)
        {
            if (Rocket != null)
            {
                double radAngle = ToRadians((double)direction);
                Rocket.Angle += radAngle;
                PointF point;
                for (int i = 0; i < Rocket.points.Length; i++)
                {
                    if (i == 2)
                        continue;

                    point = Rocket.points[i];
                    point.X = (float)(((point.X - Rocket.X) * Math.Cos(radAngle)) - ((point.Y - Rocket.Y) * Math.Sin(radAngle)) + Rocket.X);
                    point.Y = (float)((point.Y - Rocket.Y) * Math.Cos(radAngle) + (point.X - Rocket.X) * Math.Sin(radAngle) + Rocket.Y);
                }
                Rocket.EvaluatePoints();
            }

        }
        public void EndGame()
        {
            Asteroids.Clear();
            Shots.Clear();
            Rocket = null;
            EndGameEvent?.Invoke();
        }

        public void GenerateAsteroid()
        {
            Asteroids.Add(new Asteroid(ScreenWidth, ScreenHeight));
        }


        public void RenderGame(Graphics canvas)
        {
            if (Rocket == null)
                throw new NullReferenceException();

            canvas.DrawPolygon(Pens.White, Rocket.points);

            if (Rocket.HasShiled)
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
                Explosion explosion;
                for (int i = 0; i < Explosions.Count; i++)
                {
                    explosion = Explosions[i];
                    Image image;
                    switch (explosion.Counter++)
                    {
                        case 0:
                            using (MemoryStream ms = new MemoryStream(Resources.explosion1))
                            {
                                image = Image.FromStream(ms);
                            }
                            canvas.DrawImage(image, (int)explosion.X, (int)explosion.Y);
                            break;
                        case 1:
                            using (MemoryStream ms = new MemoryStream(Resources.explosion2))
                            {
                                image = Image.FromStream(ms);
                            }
                            canvas.DrawImage(image, (int)explosion.X, (int)explosion.Y);
                            break;
                        case 2:
                            using (MemoryStream ms = new MemoryStream(Resources.explosion3))
                            {
                                image = Image.FromStream(ms);
                            }
                            canvas.DrawImage(image, (int)explosion.X, (int)explosion.Y);
                            Explosions.Remove(explosion);
                            break;
                    }
                }
            }

            if (Asteroids.Count > 0)
            {
                Asteroid asteroid;
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    asteroid = Asteroids[i];
                    canvas.DrawEllipse(Pens.White, (float)(asteroid.X - asteroid.Size), (float)(asteroid.Y - asteroid.Size), (float)(2 * asteroid.Size), (float)(2 * asteroid.Size));

                    Asteroids[i] = MoveAsteroid(asteroid);
                    if (asteroid.X - asteroid.Size > ScreenWidth || asteroid.X + asteroid.Size < 0 || asteroid.Y + asteroid.Size < 0 || asteroid.Y - asteroid.Size > ScreenHeight)
                        Asteroids.Remove(asteroid);
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

        private Asteroid MoveAsteroid(Asteroid asteroid)
        {
            asteroid.X -= Math.Cos(asteroid.Angle) * asteroid.Speed;
            asteroid.Y -= Math.Sin(asteroid.Angle) * asteroid.Speed;
            return asteroid;
        }

        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
