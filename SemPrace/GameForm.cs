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
        Random random = new Random();
        Rocket rocket;

        public GameForm()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            this.rocket = new Rocket(100, 100, this.gameScreen.Width, this.gameScreen.Height, 10, 30);
            timer.Tick += UpdateScreen;
            

        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            gameScreen.Invalidate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Up:
                    rocket.MoveRocket(Direction.UP);
                    break;
                case Keys.Left:
                    rocket.Rotate(-5, Direction.LEFT);
                    break;
                case Keys.Right:
                    rocket.Rotate(5, Direction.RIGHT);
                    break;
            }
        }

        private Image rotateImage(Image image, int angle)
        {
            Image img = image;
            using (Graphics g = Graphics.FromImage(img))
            {
                g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
                g.DrawImage(image, new Point(0, 0));
            }

            return img;
        }

        private void GameScreenPait(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.DrawPolygon(Pens.White, rocket.points);
        }
    }
}
