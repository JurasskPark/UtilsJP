namespace LicenseaAgreement
{
    partial class FrmLicenseaAgreement
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
            txtLicenseaAgreement = new TextBox();
            btnYes = new Button();
            btnNo = new Button();
            SuspendLayout();
            // 
            // txtLicenseaAgreement
            // 
            txtLicenseaAgreement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLicenseaAgreement.Location = new Point(12, 12);
            txtLicenseaAgreement.Multiline = true;
            txtLicenseaAgreement.Name = "txtLicenseaAgreement";
            txtLicenseaAgreement.ScrollBars = ScrollBars.Vertical;
            txtLicenseaAgreement.Size = new Size(605, 584);
            txtLicenseaAgreement.TabIndex = 0;
            // 
            // btnYes
            // 
            btnYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnYes.Location = new Point(456, 602);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(75, 23);
            btnYes.TabIndex = 1;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            btnNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNo.Location = new Point(542, 602);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(75, 23);
            btnNo.TabIndex = 2;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // FrmLicenseaAgreement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 631);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(txtLicenseaAgreement);
            Name = "FrmLicenseaAgreement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Licensea Agreement";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLicenseaAgreement;
        private Button btnYes;
        private Button btnNo;
    }
}