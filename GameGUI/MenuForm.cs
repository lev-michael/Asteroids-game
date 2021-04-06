using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace GameGUI
{
    public partial class MenuForm : Form
    {
        public static MenuForm CurrentForm;

        public MenuForm()
        {
            CurrentForm = this;
            InitializeComponent();
            Keyboard.IsKeyDown(Key.P);  // Kvůli sjednocení velikostí, při prvním použití Keyboard se restartuje DPI auto scaling
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
           
            HighScoreForm form = new HighScoreForm();
            form.Show();
            this.Hide();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show();
            this.Hide();
        }
    }
}
