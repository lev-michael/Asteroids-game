using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemPrace
{
    public partial class GameForm : Form
    {
        public static GameForm CurrentForm;

        private int Score { get; set; }
        private bool Paused { get; set; }

        private Rocket Rocket;
        private List<Shot> Shots;
        private List<Asteroid> Asteroids;


        public GameForm()
        {
            CurrentForm = this;
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            Asteroids = new List<Asteroid>();
            Shots = new List<Shot>();
            Paused = false;
            this.Rocket = new Rocket(100, 100, this.gameScreen.Width, this.gameScreen.Height, 10, 30);
            // asteroid = new Asteroid(AsteroidSize.BIG, this.gameScreen.Width, this.gameScreen.Height);
            Score = 0;
            timer.Tick += UpdateScreen;
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            gameScreen.Invalidate();
            detectShotCollision();
        }

        private void detectShotCollision()
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
                            scoreLabel.Text = $"Score: {Score += 10}";
                            Asteroids.Remove(a);
                            if (a.Size == (double)AsteroidSize.MEDIUM)
                            {
                                Asteroids.Add(new Asteroid(AsteroidSize.SMALL, gameScreen.Width, gameScreen.Height, a.Angle + Math.PI / 2, a.X, a.Y));
                                Asteroids.Add(new Asteroid(AsteroidSize.SMALL, gameScreen.Width, gameScreen.Height, a.Angle - Math.PI / 2, a.X, a.Y));
                                break;
                            }
                            if (a.Size == (double)AsteroidSize.BIG)
                            {
                                Asteroids.Add(new Asteroid(AsteroidSize.MEDIUM, gameScreen.Width, gameScreen.Height, a.Angle + Math.PI / 2, a.X, a.Y));
                                Asteroids.Add(new Asteroid(AsteroidSize.MEDIUM, gameScreen.Width, gameScreen.Height, a.Angle - Math.PI / 2, a.X, a.Y));
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Rocket.MoveRocket();
                    break;
                case Keys.Left:
                    Rocket.Rotate(-5);
                    break;
                case Keys.Right:
                    Rocket.Rotate(5);
                    break;
                case Keys.Space:
                    Shots.Add(Rocket.Shoot());
                    break;
                case Keys.Escape:
                    EndGame();
                    break;
                case Keys.P:
                    Pause();
                    break;
            }
        }

        private void Pause()
        {
            if (Paused)
            {
                Paused = false;
                timer.Start();
                asteroidTimer.Start();
                GamePausedHintLabel.Visible = false;
                GamePausedLabel.Visible = false;
            }
            else
            {
                Paused = true;
                timer.Stop();
                asteroidTimer.Stop();
                GamePausedHintLabel.Visible = true;
                GamePausedLabel.Visible = true;
            }
        } 


        private void EndGame()
        {
            Rocket = null;
            Asteroids.Clear();
            Shots.Clear();
            this.Hide();
            GameOverForm form = new GameOverForm(Score);
            form.Show();
        }

        private void GameScreenPaint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if(Rocket != null)
            {
                canvas.DrawPolygon(Pens.White, Rocket.points);
            }
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

        private void GameScreenResized(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (Rocket != null)
            {
                Rocket.ScreenHeight = c.Size.Height;
                Rocket.ScreenWidth = c.Size.Width;
            }
        }

        private void generateAsteroid(object sender, EventArgs e)
        {
            Asteroids.Add(new Asteroid(AsteroidSize.BIG, gameScreen.Width, gameScreen.Height));
            if (asteroidTimer.Interval > 1000) 
                asteroidTimer.Interval -= 20;
            else if(asteroidTimer.Interval > 500)
                asteroidTimer.Interval -= 10;
        }
    }
}
