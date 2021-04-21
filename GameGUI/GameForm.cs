using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using GameLib;


namespace GameGUI
{
    enum AnimationSize { SMALL = 64, MEDIUM = 100, BIG = 150 };
    public partial class GameForm : Form
    {
        public static GameForm CurrentForm;
        private bool Paused { get; set; }
        private Game Game;
        private Random random;
       

        public GameForm()
        {
            random = new Random();
            CurrentForm = this;
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
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
            if(asteroidTimer.Interval>900)
                asteroidTimer.Interval -= 10;
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
    }
}
