namespace UtilsJP.WinForm
{
    partial class FrmDebug : System.Windows.Forms.Form
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
            btnSplash = new Button();
            btnDonate = new Button();
            btnInfoError = new Button();
            btnLicense = new Button();
            btnAbout = new Button();
            SuspendLayout();
            // 
            // btnSplash
            // 
            btnSplash.Location = new Point(12, 12);
            btnSplash.Name = "btnSplash";
            btnSplash.Size = new Size(75, 23);
            btnSplash.TabIndex = 0;
            btnSplash.Text = "Splash";
            btnSplash.UseVisualStyleBackColor = true;
            btnSplash.Click += btnSplash_Click;
            // 
            // btnDonate
            // 
            btnDonate.Location = new Point(93, 12);
            btnDonate.Name = "btnDonate";
            btnDonate.Size = new Size(75, 23);
            btnDonate.TabIndex = 1;
            btnDonate.Text = "Donate";
            btnDonate.UseVisualStyleBackColor = true;
            btnDonate.Click += btnDonate_Click;
            // 
            // btnInfoError
            // 
            btnInfoError.Location = new Point(174, 12);
            btnInfoError.Name = "btnInfoError";
            btnInfoError.Size = new Size(75, 23);
            btnInfoError.TabIndex = 2;
            btnInfoError.Text = "Info Error";
            btnInfoError.UseVisualStyleBackColor = true;
            btnInfoError.Click += btnInfoError_Click;
            // 
            // btnLicense
            // 
            btnLicense.Location = new Point(255, 12);
            btnLicense.Name = "btnLicense";
            btnLicense.Size = new Size(75, 23);
            btnLicense.TabIndex = 3;
            btnLicense.Text = "License";
            btnLicense.UseVisualStyleBackColor = true;
            btnLicense.Click += btnLicense_Click;
            // 
            // btnAbout
            // 
            btnAbout.Location = new Point(336, 12);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(75, 23);
            btnAbout.TabIndex = 4;
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // FrmDebug
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAbout);
            Controls.Add(btnLicense);
            Controls.Add(btnInfoError);
            Controls.Add(btnDonate);
            Controls.Add(btnSplash);
            Name = "FrmDebug";
            Text = "FrmDebug";
            ResumeLayout(false);
        }

        #endregion

        private Button btnSplash;
        private Button btnDonate;
        private Button btnInfoError;
        private Button btnLicense;
        private Button btnAbout;
    }
}