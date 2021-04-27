using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Input;
using GameLib;


namespace GameGUI
{
    internal enum Backgrounds { BG1, BG2, BG3, BG4, BG5 }
    public partial class GameForm : Form
    {
        
        public static GameForm CurrentForm;
        private bool Paused { get; set; }
        private Game Game;
        private Random Random;
       

        public GameForm()
        {
            Random = new Random();
            CurrentForm = this;
            InitializeComponent();
            StartGame();
        }



        private void StartGame()
        {
            SetRandomBackground();
            Game = new Game(gameScreen.Width, gameScreen.Height);
            Game.StartGame();
            Game.EndGameEvent += OnEndGameEvent;
            timer.Tick += UpdateScreen;
            asteroidTimer.Tick += AsteroidTimer_Tick;
        }

        private void OnEndGameEvent()
        {
            this.Hide();
            GameOverForm form = new GameOverForm(Game.Score);
            form.Show();
        }


        private void AsteroidTimer_Tick(object sender, EventArgs e)
        {
            Game.GenerateAsteroid();
            if(asteroidTimer.Interval>800)
                asteroidTimer.Interval -= 50;
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            gameScreen.Invalidate();
            Game.DetectShotCollisionWithAsteroid();
            Game.DetectBonusCollisionWithRocket();
            Game.DetectRocketAsteroidCollision();
            scoreLabel.Text = $"Score: {Game.Score}";
            OnKeyDown();
        }

        private void OnKeyDown()
        {
            if (Keyboard.IsKeyDown(Key.Up))
                Game.MoveRocket();

            if (Keyboard.IsKeyDown(Key.Left))
            {
                Game.RotateRocket(Direction.LEFT);
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                Game.RotateRocket(Direction.RIGHT);
            }

            if (Keyboard.IsKeyDown(Key.Space))
                Game.Shoot();

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

        private void GameScreenPaint(object sender, PaintEventArgs e)
        {
            Game.RenderGame(e.Graphics);
        }

        private void GameForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.P)
            {
                Pause();
            }
        }

        private void SetRandomBackground()
        {
            var backgrounds = Enum.GetValues(typeof(Backgrounds));
            Backgrounds background = (Backgrounds)backgrounds.GetValue(Random.Next(backgrounds.Length));
            switch (background)
            {
                case Backgrounds.BG1:
                    gameScreen.Image = Properties.Resources.background_1;
                    return;
                case Backgrounds.BG2:
                    gameScreen.Image = Properties.Resources.background_2;
                    return;
                case Backgrounds.BG3:
                    gameScreen.Image = Properties.Resources.background_3;
                    return;
                case Backgrounds.BG4:
                    gameScreen.Image = Properties.Resources.background_4;
                    return;
                case Backgrounds.BG5:
                    gameScreen.Image = Properties.Resources.background_5;
                    return;
            }
        }
    }
}
