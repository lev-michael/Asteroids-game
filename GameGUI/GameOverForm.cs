using GameLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameGUI
{
    public partial class GameOverForm : Form
    {
        public static GameOverForm CurrentForm;

        public int Score { get; set; }

        public GameOverForm()
        {
            InitializeComponent();
        }

        public GameOverForm(int score) : this()
        {
            Score = score;
            ScoreLabel.Text = $"Your score: {score}";
            saveHighScore(score);
        }

        private void saveHighScore(int score)
        {
            if (score == 0)
                return;

            FileInfo file = new FileInfo(Constants.HIGH_SCORE_FILE);
            List<int> scores = new List<int>();
            scores.Add(score);
            if (file.Exists)
            {
                string textScore;
                using (var reader = new StreamReader(Constants.HIGH_SCORE_FILE))
                {
                    int i;
                    while ((textScore = reader.ReadLine()) != null)
                    {
                        if (int.TryParse(textScore, out i))
                            scores.Add(i);
                    }
                    scores.Sort((a,b) => b - a);
                    if (scores.Count > 3)
                        scores = scores.GetRange(0, 3);
                }
            }
            using (StreamWriter writer = new StreamWriter(new FileStream(Constants.HIGH_SCORE_FILE, FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    writer.WriteLine(scores[i]);
                }
            }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        GameOverLabel.Visible = GameOverLabel.Visible ? false : true;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Hide();
            MenuForm form = new MenuForm();
            form.Show();

        }
        else if (e.KeyCode == Keys.Space)
        {
            this.Hide();
            GameForm form = new GameForm();
            form.Show();
        }
    }
}
}
