namespace LifeIdea.LazyCure.UI
{
    partial class Summary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Summary));
            this.activitiesSummary = new System.Windows.Forms.DataGridView();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.activities = new System.Windows.Forms.TabPage();
            this.allActivitiesTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).BeginInit();
            this.tabControl.SuspendLayout();
            this.activities.SuspendLayout();
            this.SuspendLayout();
            // 
            // activitiesSummary
            // 
            this.activitiesSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activitiesSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Activity,
            this.Spent,
            this.Task});
            this.activitiesSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.activitiesSummary.Location = new System.Drawing.Point(3, 3);
            this.activitiesSummary.Name = "activitiesSummary";
            this.activitiesSummary.Size = new System.Drawing.Size(532, 288);
            this.activitiesSummary.TabIndex = 0;
            this.activitiesSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.activitiesSummary_CellDoubleClick);
            // 
            // Activity
            // 
            this.Activity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Activity.DataPropertyName = "Activity";
            this.Activity.HeaderText = "Activity";
            this.Activity.Name = "Activity";
            this.Activity.ReadOnly = true;
            // 
            // Spent
            // 
            this.Spent.DataPropertyName = "Spent";
            this.Spent.HeaderText = "Spent";
            this.Spent.Name = "Spent";
            this.Spent.ReadOnly = true;
            this.Spent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Spent.Width = 50;
            // 
            // Task
            // 
            this.Task.DataPropertyName = "Task";
            this.Task.HeaderText = "Task";
            this.Task.Name = "Task";
            this.Task.ReadOnly = true;
            this.Task.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Task.Width = 150;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.activities);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(546, 351);
            this.tabControl.TabIndex = 1;
            // 
            // activities
            // 
            this.activities.Controls.Add(this.allActivitiesTime);
            this.activities.Controls.Add(this.label1);
            this.activities.Controls.Add(this.activitiesSummary);
            this.activities.Location = new System.Drawing.Point(4, 22);
            this.activities.Name = "activities";
            this.activities.Padding = new System.Windows.Forms.Padding(3);
            this.activities.Size = new System.Drawing.Size(538, 325);
            this.activities.TabIndex = 0;
            this.activities.Text = "Activities";
            this.activities.UseVisualStyleBackColor = true;
            // 
            // allActivitiesTime
            // 
            this.allActivitiesTime.Location = new System.Drawing.Point(186, 297);
            this.allActivitiesTime.Name = "allActivitiesTime";
            this.allActivitiesTime.ReadOnly = true;
            this.allActivitiesTime.Size = new System.Drawing.Size(34, 20);
            this.allActivitiesTime.TabIndex = 2;
            this.allActivitiesTime.Text = "12:34";
            this.allActivitiesTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Time, spent on all activities (h:mm):";
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 351);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Summary";
            this.Text = "Summary";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.activities.ResumeLayout(false);
            this.activities.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView activitiesSummary;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage activities;
        private System.Windows.Forms.TextBox allActivitiesTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task;
    }
}