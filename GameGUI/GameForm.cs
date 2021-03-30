using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLib;


namespace SemPrace
{

    public partial class GameForm : Form
    {
        public static GameForm CurrentForm;
        private bool Paused { get; set; }
        private Game Game;


        public GameForm()
        {
            CurrentForm = this;
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            Game = new Game(gameScreen.Width, gameScreen.Height);
            Game.StartGame();
            timer.Tick += UpdateScreen;
            asteroidTimer.Tick +=  AsteroidTimer_Tick;
       
        }

        private void AsteroidTimer_Tick(object sender, EventArgs e)
        {
            Game.GenerateAsteroid();
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            gameScreen.Invalidate();
            Game.detectShotCollision();
            scoreLabel.Text = $"Score: {Game.Score}";
            if (Game.IsRocketCollisionDetected())
            {
                EndGame();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Game.MoveRocket();
                    break;
                case Keys.Left:
                    Game.RotateRocket(Direction.LEFT);
                    break;
                case Keys.Right:
                    Game.RotateRocket(Direction.RIGHT);
                    break;
                case Keys.Space:
                    Game.Shoot();
                    break;
                case Keys.Escape:
                    Game.EndGame();
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
            this.Hide();
            GameOverForm form = new GameOverForm(Game.Score);
            form.Show();
        }

        private void GameScreenPaint(object sender, PaintEventArgs e)
        {
            Game.RenderGame(e.Graphics);
        }
    }
}
