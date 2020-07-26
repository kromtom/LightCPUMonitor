namespace LightCPUMonitor
{
    partial class SettingsForm
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
            this.lblIcon1 = new System.Windows.Forms.Label();
            this.cbIcon1 = new System.Windows.Forms.ComboBox();
            this.lblIcon2 = new System.Windows.Forms.Label();
            this.cbIcon2 = new System.Windows.Forms.ComboBox();
            this.lblIcon3 = new System.Windows.Forms.Label();
            this.cbIcon3 = new System.Windows.Forms.ComboBox();
            this.lblIcon4 = new System.Windows.Forms.Label();
            this.cbIcon4 = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIcon1
            // 
            this.lblIcon1.AutoSize = true;
            this.lblIcon1.Location = new System.Drawing.Point(44, 52);
            this.lblIcon1.Name = "lblIcon1";
            this.lblIcon1.Size = new System.Drawing.Size(99, 32);
            this.lblIcon1.TabIndex = 0;
            this.lblIcon1.Text = "Icon 1:";
            // 
            // cbIcon1
            // 
            this.cbIcon1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIcon1.FormattingEnabled = true;
            this.cbIcon1.Location = new System.Drawing.Point(180, 52);
            this.cbIcon1.Name = "cbIcon1";
            this.cbIcon1.Size = new System.Drawing.Size(564, 39);
            this.cbIcon1.TabIndex = 1;
            // 
            // lblIcon2
            // 
            this.lblIcon2.AutoSize = true;
            this.lblIcon2.Location = new System.Drawing.Point(44, 127);
            this.lblIcon2.Name = "lblIcon2";
            this.lblIcon2.Size = new System.Drawing.Size(99, 32);
            this.lblIcon2.TabIndex = 0;
            this.lblIcon2.Text = "Icon 2:";
            // 
            // cbIcon2
            // 
            this.cbIcon2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIcon2.FormattingEnabled = true;
            this.cbIcon2.Location = new System.Drawing.Point(180, 127);
            this.cbIcon2.Name = "cbIcon2";
            this.cbIcon2.Size = new System.Drawing.Size(564, 39);
            this.cbIcon2.TabIndex = 1;
            // 
            // lblIcon3
            // 
            this.lblIcon3.AutoSize = true;
            this.lblIcon3.Location = new System.Drawing.Point(44, 205);
            this.lblIcon3.Name = "lblIcon3";
            this.lblIcon3.Size = new System.Drawing.Size(99, 32);
            this.lblIcon3.TabIndex = 0;
            this.lblIcon3.Text = "Icon 3:";
            // 
            // cbIcon3
            // 
            this.cbIcon3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIcon3.FormattingEnabled = true;
            this.cbIcon3.Location = new System.Drawing.Point(180, 205);
            this.cbIcon3.Name = "cbIcon3";
            this.cbIcon3.Size = new System.Drawing.Size(564, 39);
            this.cbIcon3.TabIndex = 1;
            // 
            // lblIcon4
            // 
            this.lblIcon4.AutoSize = true;
            this.lblIcon4.Location = new System.Drawing.Point(44, 287);
            this.lblIcon4.Name = "lblIcon4";
            this.lblIcon4.Size = new System.Drawing.Size(99, 32);
            this.lblIcon4.TabIndex = 0;
            this.lblIcon4.Text = "Icon 4:";
            // 
            // cbIcon4
            // 
            this.cbIcon4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIcon4.FormattingEnabled = true;
            this.cbIcon4.Location = new System.Drawing.Point(180, 287);
            this.cbIcon4.Name = "cbIcon4";
            this.cbIcon4.Size = new System.Drawing.Size(564, 39);
            this.cbIcon4.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(295, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(195, 60);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 491);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbIcon4);
            this.Controls.Add(this.lblIcon4);
            this.Controls.Add(this.cbIcon3);
            this.Controls.Add(this.lblIcon3);
            this.Controls.Add(this.cbIcon2);
            this.Controls.Add(this.lblIcon2);
            this.Controls.Add(this.cbIcon1);
            this.Controls.Add(this.lblIcon1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIcon1;
        private System.Windows.Forms.ComboBox cbIcon1;
        private System.Windows.Forms.Label lblIcon2;
        private System.Windows.Forms.ComboBox cbIcon2;
        private System.Windows.Forms.Label lblIcon3;
        private System.Windows.Forms.ComboBox cbIcon3;
        private System.Windows.Forms.Label lblIcon4;
        private System.Windows.Forms.ComboBox cbIcon4;
        private System.Windows.Forms.Button btnOK;
    }
}

