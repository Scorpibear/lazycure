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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotKeysEditor));
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
            resources.ApplyResources(this.ctrlCheckBox, "ctrlCheckBox");
            this.ctrlCheckBox.Checked = true;
            this.ctrlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctrlCheckBox.Name = "ctrlCheckBox";
            this.ctrlCheckBox.UseVisualStyleBackColor = true;
            this.ctrlCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // altCheckBox
            // 
            resources.ApplyResources(this.altCheckBox, "altCheckBox");
            this.altCheckBox.Name = "altCheckBox";
            this.altCheckBox.UseVisualStyleBackColor = true;
            this.altCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // shiftCheckBox
            // 
            resources.ApplyResources(this.shiftCheckBox, "shiftCheckBox");
            this.shiftCheckBox.Name = "shiftCheckBox";
            this.shiftCheckBox.UseVisualStyleBackColor = true;
            this.shiftCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // keysBox
            // 
            resources.ApplyResources(this.keysBox, "keysBox");
            this.keysBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keysBox.FormattingEnabled = true;
            this.keysBox.Name = "keysBox";
            this.keysBox.SelectedValueChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // keysLabel
            // 
            resources.ApplyResources(this.keysLabel, "keysLabel");
            this.keysLabel.Name = "keysLabel";
            // 
            // HotKeysEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.keysLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.keysBox);
            this.Controls.Add(this.shiftCheckBox);
            this.Controls.Add(this.altCheckBox);
            this.Controls.Add(this.ctrlCheckBox);
            this.Name = "HotKeysEditor";
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