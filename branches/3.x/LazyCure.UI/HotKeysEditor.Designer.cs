namespace LifeIdea.LazyCure.UI
{
    partial class HotKeysEditor
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
            this.ctrlCheckBox = new System.Windows.Forms.CheckBox();
            this.altCheckBox = new System.Windows.Forms.CheckBox();
            this.shiftCheckBox = new System.Windows.Forms.CheckBox();
            this.keysBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.keysLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlCheckBox
            // 
            this.ctrlCheckBox.AutoSize = true;
            this.ctrlCheckBox.Checked = true;
            this.ctrlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctrlCheckBox.Location = new System.Drawing.Point(12, 12);
            this.ctrlCheckBox.Name = "ctrlCheckBox";
            this.ctrlCheckBox.Size = new System.Drawing.Size(41, 17);
            this.ctrlCheckBox.TabIndex = 0;
            this.ctrlCheckBox.Text = "Ctrl";
            this.ctrlCheckBox.UseVisualStyleBackColor = true;
            this.ctrlCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // altCheckBox
            // 
            this.altCheckBox.AutoSize = true;
            this.altCheckBox.Location = new System.Drawing.Point(59, 12);
            this.altCheckBox.Name = "altCheckBox";
            this.altCheckBox.Size = new System.Drawing.Size(38, 17);
            this.altCheckBox.TabIndex = 1;
            this.altCheckBox.Text = "Alt";
            this.altCheckBox.UseVisualStyleBackColor = true;
            this.altCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // shiftCheckBox
            // 
            this.shiftCheckBox.AutoSize = true;
            this.shiftCheckBox.Location = new System.Drawing.Point(103, 12);
            this.shiftCheckBox.Name = "shiftCheckBox";
            this.shiftCheckBox.Size = new System.Drawing.Size(47, 17);
            this.shiftCheckBox.TabIndex = 2;
            this.shiftCheckBox.Text = "Shift";
            this.shiftCheckBox.UseVisualStyleBackColor = true;
            this.shiftCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // keysBox
            // 
            this.keysBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keysBox.FormattingEnabled = true;
            this.keysBox.Location = new System.Drawing.Point(103, 35);
            this.keysBox.Name = "keysBox";
            this.keysBox.Size = new System.Drawing.Size(47, 21);
            this.keysBox.TabIndex = 3;
            this.keysBox.SelectedValueChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(35, 62);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(38, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(79, 62);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(57, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // hotKeyToActivateLabel
            // 
            this.keysLabel.Location = new System.Drawing.Point(1, 38);
            this.keysLabel.Name = "hotKeyToActivateLabel";
            this.keysLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.keysLabel.Size = new System.Drawing.Size(96, 16);
            this.keysLabel.TabIndex = 6;
            this.keysLabel.Text = "Ctrl+F12";
            this.keysLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // EditKeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 94);
            this.Controls.Add(this.keysLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.keysBox);
            this.Controls.Add(this.shiftCheckBox);
            this.Controls.Add(this.altCheckBox);
            this.Controls.Add(this.ctrlCheckBox);
            this.Name = "EditKeysForm";
            this.Text = "Keys";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ctrlCheckBox;
        private System.Windows.Forms.CheckBox altCheckBox;
        private System.Windows.Forms.CheckBox shiftCheckBox;
        private System.Windows.Forms.ComboBox keysBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label keysLabel;
    }
}