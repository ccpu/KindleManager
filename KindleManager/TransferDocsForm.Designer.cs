namespace KindleManager
{
    partial class TransferDocsForm
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
            this.usbListBox = new System.Windows.Forms.ListBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // usbListBox
            //
            this.usbListBox.FormattingEnabled = true;
            this.usbListBox.Location = new System.Drawing.Point(47, 53);
            this.usbListBox.Name = "usbListBox";
            this.usbListBox.Size = new System.Drawing.Size(285, 95);
            this.usbListBox.TabIndex = 0;
            //
            // btnTransfer
            //
            this.btnTransfer.Location = new System.Drawing.Point(259, 186);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(107, 23);
            this.btnTransfer.TabIndex = 1;
            this.btnTransfer.Text = "Transfer Docs";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            //
            // lblMsg
            //
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(44, 165);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 2;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "USB Devices";
            //
            // TransferDocsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 220);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.usbListBox);
            this.Name = "TransferDocsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransferDocsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransferDocsForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TransferDocsForm_FormClosed);
            this.Load += new System.EventHandler(this.TransferDocsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox usbListBox;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label1;
    }
}