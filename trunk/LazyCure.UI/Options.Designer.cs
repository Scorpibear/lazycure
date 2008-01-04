namespace LifeIdea.LazyCure.UI
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.saveAfterDone = new System.Windows.Forms.CheckBox();
            this.maxActivitiesInHistory = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectTimeLogsFolder = new System.Windows.Forms.Button();
            this.timeLogFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.reminderTime = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maxActivitiesInHistory)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveAfterDone
            // 
            this.saveAfterDone.AutoSize = true;
            this.saveAfterDone.Location = new System.Drawing.Point(3, 3);
            this.saveAfterDone.Name = "saveAfterDone";
            this.saveAfterDone.Size = new System.Drawing.Size(262, 17);
            this.saveAfterDone.TabIndex = 0;
            this.saveAfterDone.Text = "Save Time Log everytime \'Done\' button is pressed";
            this.saveAfterDone.UseVisualStyleBackColor = true;
            // 
            // maxActivitiesInHistory
            // 
            this.maxActivitiesInHistory.Location = new System.Drawing.Point(229, 26);
            this.maxActivitiesInHistory.Name = "maxActivitiesInHistory";
            this.maxActivitiesInHistory.Size = new System.Drawing.Size(36, 20);
            this.maxActivitiesInHistory.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Maximum number of activities stored in history:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.reminderTime);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.selectTimeLogsFolder);
            this.panel1.Controls.Add(this.timeLogFolder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.saveAfterDone);
            this.panel1.Controls.Add(this.maxActivitiesInHistory);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 103);
            this.panel1.TabIndex = 3;
            // 
            // selectTimeLogsFolder
            // 
            this.selectTimeLogsFolder.Location = new System.Drawing.Point(305, 50);
            this.selectTimeLogsFolder.Name = "selectTimeLogsFolder";
            this.selectTimeLogsFolder.Size = new System.Drawing.Size(25, 23);
            this.selectTimeLogsFolder.TabIndex = 5;
            this.selectTimeLogsFolder.Text = "...";
            this.selectTimeLogsFolder.UseVisualStyleBackColor = true;
            this.selectTimeLogsFolder.Click += new System.EventHandler(this.selectTimeLogsFolder_Click);
            // 
            // timeLogFolder
            // 
            this.timeLogFolder.Location = new System.Drawing.Point(114, 52);
            this.timeLogFolder.Name = "timeLogFolder";
            this.timeLogFolder.ReadOnly = true;
            this.timeLogFolder.Size = new System.Drawing.Size(185, 20);
            this.timeLogFolder.TabIndex = 4;
            this.timeLogFolder.Text = "D:\\Program Files\\LazyCure\\TimeLogs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Folder with time logs:";
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(197, 121);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 4;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(278, 121);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Remind after";
            // 
            // reminderTime
            // 
            this.reminderTime.Location = new System.Drawing.Point(70, 76);
            this.reminderTime.Mask = "00:00";
            this.reminderTime.Name = "reminderTime";
            this.reminderTime.Size = new System.Drawing.Size(38, 20);
            this.reminderTime.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "(hh:mm) of inactivity";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 156);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.maxActivitiesInHistory)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox saveAfterDone;
        private System.Windows.Forms.NumericUpDown maxActivitiesInHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox timeLogFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectTimeLogsFolder;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.MaskedTextBox reminderTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}