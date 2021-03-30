using System;
using System.Collections.Generic;
using System.Drawing;


namespace GameLib
{   public class Game
    {
        private Random rand;
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public int Score { get; private set; }

        public Rocket Rocket { get; private set; }
        private List<Shot> Shots;
        private List<Asteroid> Asteroids;

        public Game(double width, double height)
        {
            rand = new Random();
            ScreenHeight = height;
            ScreenWidth = width;
            Asteroids = new List<Asteroid>();
            Shots = new List<Shot>();
        }

        public void StartGame()
        {
            Score = 0;
            this.Rocket = new Rocket(ScreenWidth, ScreenHeight);
        }

        public void detectShotCollision()
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
                            if (a.Size == (double)AsteroidSize.MEDIUM)
                            {
                                Asteroids.Add(new Asteroid(AsteroidSize.SMALL, ScreenWidth, ScreenHeight, a.Angle + Math.PI / 2, a.X, a.Y));
                                Asteroids.Add(new Asteroid(AsteroidSize.SMALL, ScreenWidth, ScreenHeight, a.Angle - Math.PI / 2, a.X, a.Y));
                                break;
                            }
                            if (a.Size == (double)AsteroidSize.BIG)
                            {
                                Asteroids.Add(new Asteroid(AsteroidSize.MEDIUM, ScreenWidth, ScreenHeight, a.Angle + Math.PI / 2, a.X, a.Y));
                                Asteroids.Add(new Asteroid(AsteroidSize.MEDIUM, ScreenWidth, ScreenHeight, a.Angle - Math.PI / 2, a.X, a.Y));
                                break;
                            }
                        }
                    }
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
                        EndGame();
                        return true;
                    }
                }
            }
            return false;
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

                if (IsVertexOfRocketIntersectingAsteroid(a.X, a.Y, a.Size, startPoint.X, startPoint.Y))
                    return true;

                if (IsEdgeOfRocketIntersectingAsteroid(a.X, a.Y, a.Size, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y))
                    return true;
                
                startPoint = endPoint;
            }
            return false;
        }

        private bool IsVertexOfRocketIntersectingAsteroid(double cirlceCenterX, double cirlceCenterY, double radius, double pointX, double pointY)
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

        private bool IsEdgeOfRocketIntersectingAsteroid(double cirlceCenterX, double cirlceCenterY, double radius, double startPointX, double startPointY, double endPointX, double endPointY)
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
                    if (Math.Pow(c1x, 2) + Math.Pow(c1y, 2) - Math.Pow(k,2) <= radius)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MoveRocket()
        {
            Rocket.MoveRocket();
        }

        public void Shoot()
        {
            Shots.Add(Rocket.Shoot());
        }

        public void RotateRocket(Direction direction)
        {
            Rocket.Rotate(direction);
        }
        public void EndGame()
        {
            Asteroids.Clear();
            Shots.Clear();
        }

        public void GenerateAsteroid()
        {
            AsteroidSize size;
            int randNum = rand.Next(0, 8);
            if (randNum < 2)
            {
                size = AsteroidSize.SMALL;
            }
            else if (randNum <= 5)
            {
                size = AsteroidSize.MEDIUM;
            }
            else
            {
                size = AsteroidSize.BIG;
            }

            Asteroids.Add(new Asteroid(size, ScreenWidth, ScreenHeight));

        }

        public void RenderGame(Graphics canvas)
        {
            if (Rocket == null)
                throw new NullReferenceException();

            canvas.DrawPolygon(Pens.White, Rocket.points);

            if (Shots.Count > 0)
            {
                Shot shot;
                for (int i = 0; i < Shots.Count; i++)
                {
                    shot = Shots[i];
                    canvas.DrawLine(new Pen(Color.Red, 5), (float)shot.X, (float)shot.Y, (float)shot.EndX, (float)shot.EndY);
                    if (shot.MoveShot())
                        Shots.Remove(shot);
                }
            }

            if (Asteroids.Count > 0)
            {
                Asteroid a;
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    a = Asteroids[i];
                    canvas.FillEllipse(Brushes.White, (float)(a.X - a.Size), (float)(a.Y - a.Size), (float)(2 * a.Size), (float)(2 * a.Size));
                    if (a.MoveAsteroid())
                        Asteroids.Remove(a);
                }
            }
        }
    }
}
