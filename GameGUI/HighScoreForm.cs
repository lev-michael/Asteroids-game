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
    public partial class HighScoreForm : Form
    {
        private List<String> HighScoreResults;
        public HighScoreForm()
        {
            InitializeComponent();
            HighScoreResults = new List<String>();
            loadData();
            displayData();
        }

        private void displayData()
        {
            HighScoreList.Text = "";
            if (HighScoreResults.Count > 0)
            {
                
                for (int i = 1; i <= HighScoreResults.Count; i++)
                {
                    HighScoreList.Text += $"{i}. {HighScoreResults[i-1]} \n";
                }
            } else
            {
                HighScoreList.Text = "No high score yet.";
            }

        }

        private void loadData()
        {
            using (var reader = new StreamReader(Constants.HIGH_SCORE_FILE))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    HighScoreResults.Add(line);
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                MenuForm form = new MenuForm();
                form.Show();
                this.Hide();

            }
        }
    }
}
