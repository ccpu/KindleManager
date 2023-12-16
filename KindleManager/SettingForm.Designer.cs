namespace KindleManager
{
    partial class SettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.fbdDataDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.tbDataDirectory = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbStartOnBoot = new System.Windows.Forms.CheckBox();
            this.cbShowOnConnect = new System.Windows.Forms.CheckBox();
            this.tbValumeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEmailFrom = new System.Windows.Forms.TextBox();
            this.tbEmailFromPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbKindleEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCalibreFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAutoSendEmail = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Directory:";
            // 
            // tbDataDirectory
            // 
            this.tbDataDirectory.Location = new System.Drawing.Point(31, 48);
            this.tbDataDirectory.Name = "tbDataDirectory";
            this.tbDataDirectory.Size = new System.Drawing.Size(316, 20);
            this.tbDataDirectory.TabIndex = 1;
            this.tbDataDirectory.Click += new System.EventHandler(this.tbDataDirectory_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(272, 406);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbStartOnBoot
            // 
            this.cbStartOnBoot.AutoSize = true;
            this.cbStartOnBoot.Location = new System.Drawing.Point(31, 315);
            this.cbStartOnBoot.Name = "cbStartOnBoot";
            this.cbStartOnBoot.Size = new System.Drawing.Size(90, 17);
            this.cbStartOnBoot.TabIndex = 4;
            this.cbStartOnBoot.Text = "Start On Boot";
            this.cbStartOnBoot.UseVisualStyleBackColor = true;
            this.cbStartOnBoot.CheckedChanged += new System.EventHandler(this.cbStartOnBoot_CheckedChanged);
            // 
            // cbShowOnConnect
            // 
            this.cbShowOnConnect.AutoSize = true;
            this.cbShowOnConnect.Location = new System.Drawing.Point(31, 361);
            this.cbShowOnConnect.Name = "cbShowOnConnect";
            this.cbShowOnConnect.Size = new System.Drawing.Size(275, 17);
            this.cbShowOnConnect.TabIndex = 5;
            this.cbShowOnConnect.Text = "Show Transfer Docs From When Device Connection";
            this.cbShowOnConnect.UseVisualStyleBackColor = true;
            // 
            // tbValumeName
            // 
            this.tbValumeName.Location = new System.Drawing.Point(31, 138);
            this.tbValumeName.Name = "tbValumeName";
            this.tbValumeName.Size = new System.Drawing.Size(316, 20);
            this.tbValumeName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "eBook Volume Label:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Email Sender:";
            // 
            // tbEmailFrom
            // 
            this.tbEmailFrom.Location = new System.Drawing.Point(31, 186);
            this.tbEmailFrom.Name = "tbEmailFrom";
            this.tbEmailFrom.Size = new System.Drawing.Size(316, 20);
            this.tbEmailFrom.TabIndex = 9;
            // 
            // tbEmailFromPass
            // 
            this.tbEmailFromPass.Location = new System.Drawing.Point(31, 233);
            this.tbEmailFromPass.Name = "tbEmailFromPass";
            this.tbEmailFromPass.Size = new System.Drawing.Size(316, 20);
            this.tbEmailFromPass.TabIndex = 11;
            this.tbEmailFromPass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Email Sender Password:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbKindleEmail
            // 
            this.tbKindleEmail.Location = new System.Drawing.Point(31, 280);
            this.tbKindleEmail.Name = "tbKindleEmail";
            this.tbKindleEmail.Size = new System.Drawing.Size(316, 20);
            this.tbKindleEmail.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Kindle Email:";
            // 
            // tbCalibreFilePath
            // 
            this.tbCalibreFilePath.Location = new System.Drawing.Point(31, 92);
            this.tbCalibreFilePath.Name = "tbCalibreFilePath";
            this.tbCalibreFilePath.Size = new System.Drawing.Size(316, 20);
            this.tbCalibreFilePath.TabIndex = 15;
            this.tbCalibreFilePath.Click += new System.EventHandler(this.tbCalibreFilePath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Calibre File Path (ebook-convert.exe) :";
            // 
            // chkAutoSendEmail
            // 
            this.chkAutoSendEmail.AutoSize = true;
            this.chkAutoSendEmail.Location = new System.Drawing.Point(31, 338);
            this.chkAutoSendEmail.Name = "chkAutoSendEmail";
            this.chkAutoSendEmail.Size = new System.Drawing.Size(331, 17);
            this.chkAutoSendEmail.TabIndex = 16;
            this.chkAutoSendEmail.Text = "Watch docs folder and send email when file added (Must Restat)";
            this.chkAutoSendEmail.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 441);
            this.Controls.Add(this.chkAutoSendEmail);
            this.Controls.Add(this.tbCalibreFilePath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbKindleEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbEmailFromPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbEmailFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbValumeName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbShowOnConnect);
            this.Controls.Add(this.cbStartOnBoot);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbDataDirectory);
            this.Controls.Add(this.label1);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingForm";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbdDataDirectory;
        private System.Windows.Forms.TextBox tbDataDirectory;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbStartOnBoot;
        private System.Windows.Forms.CheckBox cbShowOnConnect;
        private System.Windows.Forms.TextBox tbValumeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEmailFrom;
        private System.Windows.Forms.TextBox tbEmailFromPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbKindleEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCalibreFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAutoSendEmail;
    }
}