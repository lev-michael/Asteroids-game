namespace GameGUI
{
    partial class GameOverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameOverLabel = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.TooltipLabel = new System.Windows.Forms.Label();
            this.TooltipLabel2 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // GameOverLabel
            // 
            this.GameOverLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GameOverLabel.AutoSize = true;
            this.GameOverLabel.Font = new System.Drawing.Font("Silkscreen", 82F);
            this.GameOverLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GameOverLabel.Location = new System.Drawing.Point(46, 57);
            this.GameOverLabel.Name = "GameOverLabel";
            this.GameOverLabel.Padding = new System.Windows.Forms.Padding(50);
            this.GameOverLabel.Size = new System.Drawing.Size(1178, 245);
            this.GameOverLabel.TabIndex = 0;
            this.GameOverLabel.Text = "Game Over";
            this.GameOverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Silkscreen", 26F);
            this.ScoreLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ScoreLabel.Location = new System.Drawing.Point(334, 334);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(415, 46);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.Text = "Your score: ";
            // 
            // TooltipLabel
            // 
            this.TooltipLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TooltipLabel.AutoSize = true;
            this.TooltipLabel.Font = new System.Drawing.Font("Silkscreen", 22F);
            this.TooltipLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TooltipLabel.Location = new System.Drawing.Point(286, 465);
            this.TooltipLabel.Name = "TooltipLabel";
            this.TooltipLabel.Size = new System.Drawing.Size(729, 39);
            this.TooltipLabel.TabIndex = 2;
            this.TooltipLabel.Text = "Press Space to play again";
            // 
            // TooltipLabel2
            // 
            this.TooltipLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TooltipLabel2.AutoSize = true;
            this.TooltipLabel2.Font = new System.Drawing.Font("Silkscreen", 22F);
            this.TooltipLabel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TooltipLabel2.Location = new System.Drawing.Point(317, 541);
            this.TooltipLabel2.Name = "TooltipLabel2";
            this.TooltipLabel2.Size = new System.Drawing.Size(679, 39);
            this.TooltipLabel2.TabIndex = 3;
            this.TooltipLabel2.Text = "Press ESC for main menu";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 600;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // GameOverForm
            // 
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1258, 664);
            this.Controls.Add(this.TooltipLabel2);
            this.Controls.Add(this.TooltipLabel);
            this.Controls.Add(this.GameOverLabel);
            this.Controls.Add(this.ScoreLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameOverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameOverForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameOverLabel;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label TooltipLabel;
        private System.Windows.Forms.Label TooltipLabel2;
        private System.Windows.Forms.Timer timer;
    }
}