namespace Donate
{
    partial class DonateScreen
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonateScreen));
            lblFrame = new Panel();
            lblTime = new Label();
            txtText = new TextBox();
            picLoad = new PictureBox();
            tmrTimer = new System.Windows.Forms.Timer(components);
            lblFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLoad).BeginInit();
            SuspendLayout();
            // 
            // lblFrame
            // 
            lblFrame.BorderStyle = BorderStyle.FixedSingle;
            lblFrame.Controls.Add(lblTime);
            lblFrame.Controls.Add(txtText);
            lblFrame.Controls.Add(picLoad);
            lblFrame.Location = new Point(2, 2);
            lblFrame.Margin = new Padding(4, 3, 4, 3);
            lblFrame.Name = "lblFrame";
            lblFrame.Size = new Size(516, 196);
            lblFrame.TabIndex = 7;
            // 
            // lblTime
            // 
            lblTime.Location = new Point(10, 170);
            lblTime.Margin = new Padding(4, 0, 4, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(83, 15);
            lblTime.TabIndex = 7;
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtText
            // 
            txtText.Location = new Point(100, 9);
            txtText.Multiline = true;
            txtText.Name = "txtText";
            txtText.ReadOnly = true;
            txtText.Size = new Size(405, 176);
            txtText.TabIndex = 6;
            txtText.TabStop = false;
            txtText.MouseClick += txtText_MouseClick;
            txtText.MouseDown += txtText_MouseDown;
            // 
            // picLoad
            // 
            picLoad.ErrorImage = null;
            picLoad.Image = (Image)resources.GetObject("picLoad.Image");
            picLoad.Location = new Point(10, 67);
            picLoad.Margin = new Padding(4, 3, 4, 3);
            picLoad.Name = "picLoad";
            picLoad.Size = new Size(83, 60);
            picLoad.SizeMode = PictureBoxSizeMode.CenterImage;
            picLoad.TabIndex = 0;
            picLoad.TabStop = false;
            // 
            // tmrTimer
            // 
            tmrTimer.Tick += tmrTimer_Tick;
            // 
            // DonateScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(520, 200);
            ControlBox = false;
            Controls.Add(lblFrame);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DonateScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Splash";
            lblFrame.ResumeLayout(false);
            lblFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLoad).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel lblFrame;
        private System.Windows.Forms.PictureBox picLoad;
        private System.Windows.Forms.Timer tmrTimer;
        private TextBox txtText;
        public Label lblTime;
    }
}