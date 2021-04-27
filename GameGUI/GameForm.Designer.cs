namespace GameGUI
{
    partial class GameForm
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.gameScreen = new System.Windows.Forms.PictureBox();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.asteroidTimer = new System.Windows.Forms.Timer(this.components);
            this.GamePausedLabel = new System.Windows.Forms.Label();
            this.GamePausedHintLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gameScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 5;
            // 
            // gameScreen
            // 
            this.gameScreen.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gameScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameScreen.Location = new System.Drawing.Point(0, 0);
            this.gameScreen.Margin = new System.Windows.Forms.Padding(4);
            this.gameScreen.Name = "gameScreen";
            this.gameScreen.Size = new System.Drawing.Size(1258, 664);
            this.gameScreen.TabIndex = 0;
            this.gameScreen.TabStop = false;
            this.gameScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreenPaint);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Silkscreen", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scoreLabel.Location = new System.Drawing.Point(15, 11);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(169, 28);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Score: 0";
            // 
            // asteroidTimer
            // 
            this.asteroidTimer.Enabled = true;
            this.asteroidTimer.Interval = 3000;
            // 
            // GamePausedLabel
            // 
            this.GamePausedLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GamePausedLabel.AutoSize = true;
            this.GamePausedLabel.Font = new System.Drawing.Font("Silkscreen", 48F);
            this.GamePausedLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GamePausedLabel.Location = new System.Drawing.Point(261, 217);
            this.GamePausedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GamePausedLabel.Name = "GamePausedLabel";
            this.GamePausedLabel.Size = new System.Drawing.Size(758, 84);
            this.GamePausedLabel.TabIndex = 1;
            this.GamePausedLabel.Text = "Game paused";
            this.GamePausedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GamePausedLabel.Visible = false;
            // 
            // GamePausedHintLabel
            // 
            this.GamePausedHintLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GamePausedHintLabel.AutoSize = true;
            this.GamePausedHintLabel.Font = new System.Drawing.Font("Silkscreen", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamePausedHintLabel.ForeColor = System.Drawing.Color.Snow;
            this.GamePausedHintLabel.Location = new System.Drawing.Point(423, 367);
            this.GamePausedHintLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GamePausedHintLabel.Name = "GamePausedHintLabel";
            this.GamePausedHintLabel.Size = new System.Drawing.Size(439, 124);
            this.GamePausedHintLabel.TabIndex = 2;
            this.GamePausedHintLabel.Text = "Press P for resume\r\n\r\n\r\n\r\n";
            this.GamePausedHintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GamePausedHintLabel.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1258, 664);
            this.Controls.Add(this.GamePausedLabel);
            this.Controls.Add(this.GamePausedHintLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.gameScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asteroids";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gameScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox gameScreen;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Timer asteroidTimer;
        private System.Windows.Forms.Label GamePausedLabel;
        private System.Windows.Forms.Label GamePausedHintLabel;

    }
}