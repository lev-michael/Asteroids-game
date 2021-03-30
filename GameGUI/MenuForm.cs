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
    public partial class MenuForm : Form
    {
        public static MenuForm CurrentForm;

        public MenuForm()
        {
            CurrentForm = this;
            InitializeComponent();
        }

        private void Menu_Form_Load(object sender, EventArgs e)
        {

        }


        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }

        private void HighScoreButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            HighScoreForm form = new HighScoreForm();
            form.ShowDialog();
            this.Show();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show();
            this.Hide();
        }
    }
}
