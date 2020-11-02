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
            this.btnSave.Location = new System.Drawing.Point(272, 188);
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
            this.cbStartOnBoot.Location = new System.Drawing.Point(31, 130);
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
            this.cbShowOnConnect.Location = new System.Drawing.Point(31, 153);
            this.cbShowOnConnect.Name = "cbShowOnConnect";
            this.cbShowOnConnect.Size = new System.Drawing.Size(275, 17);
            this.cbShowOnConnect.TabIndex = 5;
            this.cbShowOnConnect.Text = "Show Transfer Docs From When Device Connection";
            this.cbShowOnConnect.UseVisualStyleBackColor = true;
            //
            // tbValumeName
            //
            this.tbValumeName.Location = new System.Drawing.Point(31, 93);
            this.tbValumeName.Name = "tbValumeName";
            this.tbValumeName.Size = new System.Drawing.Size(316, 20);
            this.tbValumeName.TabIndex = 7;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "eBook Volume Label:";
            //
            // SettingForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 224);
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
    }
}