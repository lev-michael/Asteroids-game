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
        private int Score { get; set; }
        private Rocket rocket;
        private Shot shot;
        private List<Asteroid> asteroids;
        private Asteroid asteroid;

        public static int width;

        public GameForm()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            this.rocket = new Rocket(100, 100, this.gameScreen.Width, this.gameScreen.Height, 10, 30);
            asteroid = new Asteroid(AsteroidSize.BIG, this.gameScreen.Width, this.gameScreen.Height);
            Score = 0;
            timer.Tick += UpdateScreen;
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            scoreLabel.Text = $"Score: {Score++}";
            gameScreen.Invalidate();
            detectCollision();
        }

        private void detectCollision()
        {
            if (shot != null && asteroid != null)
            {
                if (Math.Pow(shot.EndX - asteroid.X, 2) + Math.Pow(shot.EndY - asteroid.Y, 2) <= Math.Pow(asteroid.Size, 2))
                {
                    asteroid = null;
                }
            }


        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Up:
                    rocket.MoveRocket();
                    break;
                case Keys.Left:
                    rocket.Rotate(-5);
                    break;
                case Keys.Right:
                    rocket.Rotate(5);
                    break;
                case Keys.Space:
                    shot = rocket.Shoot();
                    break;
            }
        }

        private void GameScreenPaint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.DrawPolygon(Pens.White, rocket.points);
            if (shot != null)
            {
                canvas.DrawLine(new Pen(Color.Red, 5), (float)shot.X, (float)shot.Y, (float)shot.EndX, (float)shot.EndY);
                if (shot.MoveShot())
                    shot = null;
            }
            if(asteroid != null)
            {
                canvas.DrawEllipse(Pens.White, (float)(asteroid.X - asteroid.Size), (float)(asteroid.Y - asteroid.Size),
                   (float)(2*asteroid.Size), (float)(2*asteroid.Size));
                if (asteroid.MoveAsteroid())
                    asteroid = new Asteroid(AsteroidSize.BIG, this.gameScreen.Width, this.gameScreen.Height);
            }
        }

        private void GameScreenResized(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (rocket != null)
            {
                rocket.ScreenHeight = c.Size.Height;
                rocket.ScreenWidth = c.Size.Width;
            }
            if (shot != null)
            {
                shot.ScreenHeight = c.Size.Height;
                shot.ScreenWidth = c.Size.Width;
            }
            
        }
    }
}
