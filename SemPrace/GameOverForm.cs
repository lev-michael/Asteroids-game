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
    public partial class GameOverForm : Form
    {
        public static GameOverForm CurrentForm;
        public int Score { get; set; }

        public GameOverForm()
        {
            CurrentForm = this;
            InitializeComponent();
        }

        public GameOverForm(int score): this()
        {
            Score = score;
            ScoreLabel.Text = $"Your score: {score}";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            GameOverLabel.Visible = GameOverLabel.Visible ? false : true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Hide();
                MenuForm form = new MenuForm();
                form.Show();

            } else if (e.KeyCode == Keys.Space)
            {
                this.Hide();
                GameForm form = new GameForm();
                form.Show();
            }
        }
    }
}
