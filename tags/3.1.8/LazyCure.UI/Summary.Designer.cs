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
            this.activityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spentColumnForActivitySummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskColumnForActivitySummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.activities = new System.Windows.Forms.TabPage();
            this.tasks = new System.Windows.Forms.TabPage();
            this.tasksSummary = new System.Windows.Forms.DataGridView();
            this.taskColumnForTasksSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spentColumnForTasksSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allActivitiesTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statisticsGroup = new System.Windows.Forms.GroupBox();
            this.workingActivitiesTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).BeginInit();
            this.tabControl.SuspendLayout();
            this.activities.SuspendLayout();
            this.tasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksSummary)).BeginInit();
            this.statisticsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // activitiesSummary
            // 
            this.activitiesSummary.AllowUserToAddRows = false;
            this.activitiesSummary.AllowUserToDeleteRows = false;
            this.activitiesSummary.AllowUserToOrderColumns = true;
            this.activitiesSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activitiesSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.activityColumn,
            this.spentColumnForActivitySummary,
            this.taskColumnForActivitySummary});
            this.activitiesSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activitiesSummary.Location = new System.Drawing.Point(3, 3);
            this.activitiesSummary.Name = "activitiesSummary";
            this.activitiesSummary.ReadOnly = true;
            this.activitiesSummary.Size = new System.Drawing.Size(555, 256);
            this.activitiesSummary.TabIndex = 0;
            this.activitiesSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.activitiesSummary_CellDoubleClick);
            // 
            // activityColumn
            // 
            this.activityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.activityColumn.DataPropertyName = "Activity";
            this.activityColumn.HeaderText = "Activity";
            this.activityColumn.Name = "activityColumn";
            this.activityColumn.ReadOnly = true;
            // 
            // spentColumnForActivitySummary
            // 
            this.spentColumnForActivitySummary.DataPropertyName = "Spent";
            this.spentColumnForActivitySummary.HeaderText = "Spent";
            this.spentColumnForActivitySummary.Name = "spentColumnForActivitySummary";
            this.spentColumnForActivitySummary.ReadOnly = true;
            this.spentColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spentColumnForActivitySummary.Width = 50;
            // 
            // taskColumnForActivitySummary
            // 
            this.taskColumnForActivitySummary.DataPropertyName = "Task";
            this.taskColumnForActivitySummary.HeaderText = "Task";
            this.taskColumnForActivitySummary.Name = "taskColumnForActivitySummary";
            this.taskColumnForActivitySummary.ReadOnly = true;
            this.taskColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.taskColumnForActivitySummary.Width = 150;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.activities);
            this.tabControl.Controls.Add(this.tasks);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(569, 288);
            this.tabControl.TabIndex = 1;
            // 
            // activities
            // 
            this.activities.Controls.Add(this.activitiesSummary);
            this.activities.Location = new System.Drawing.Point(4, 22);
            this.activities.Name = "activities";
            this.activities.Padding = new System.Windows.Forms.Padding(3);
            this.activities.Size = new System.Drawing.Size(561, 262);
            this.activities.TabIndex = 0;
            this.activities.Text = "Activities";
            this.activities.UseVisualStyleBackColor = true;
            // 
            // tasks
            // 
            this.tasks.Controls.Add(this.tasksSummary);
            this.tasks.Location = new System.Drawing.Point(4, 22);
            this.tasks.Name = "tasks";
            this.tasks.Size = new System.Drawing.Size(561, 262);
            this.tasks.TabIndex = 1;
            this.tasks.Text = "Tasks";
            this.tasks.UseVisualStyleBackColor = true;
            // 
            // tasksSummary
            // 
            this.tasksSummary.AllowUserToAddRows = false;
            this.tasksSummary.AllowUserToDeleteRows = false;
            this.tasksSummary.AllowUserToOrderColumns = true;
            this.tasksSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tasksSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskColumnForTasksSummary,
            this.spentColumnForTasksSummary});
            this.tasksSummary.Location = new System.Drawing.Point(3, 3);
            this.tasksSummary.Name = "tasksSummary";
            this.tasksSummary.ReadOnly = true;
            this.tasksSummary.Size = new System.Drawing.Size(555, 256);
            this.tasksSummary.TabIndex = 0;
            // 
            // taskColumnForTasksSummary
            // 
            this.taskColumnForTasksSummary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.taskColumnForTasksSummary.DataPropertyName = "Task";
            this.taskColumnForTasksSummary.HeaderText = "Task";
            this.taskColumnForTasksSummary.Name = "taskColumnForTasksSummary";
            this.taskColumnForTasksSummary.ReadOnly = true;
            // 
            // spentColumnForTasksSummary
            // 
            this.spentColumnForTasksSummary.DataPropertyName = "Spent";
            this.spentColumnForTasksSummary.HeaderText = "Spent";
            this.spentColumnForTasksSummary.MinimumWidth = 50;
            this.spentColumnForTasksSummary.Name = "spentColumnForTasksSummary";
            this.spentColumnForTasksSummary.ReadOnly = true;
            this.spentColumnForTasksSummary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spentColumnForTasksSummary.Width = 50;
            // 
            // allActivitiesTime
            // 
            this.allActivitiesTime.Location = new System.Drawing.Point(140, 13);
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
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "All activities (h:mm):";
            // 
            // statisticsGroup
            // 
            this.statisticsGroup.Controls.Add(this.workingActivitiesTime);
            this.statisticsGroup.Controls.Add(this.label2);
            this.statisticsGroup.Controls.Add(this.allActivitiesTime);
            this.statisticsGroup.Controls.Add(this.label1);
            this.statisticsGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statisticsGroup.Location = new System.Drawing.Point(0, 294);
            this.statisticsGroup.Name = "statisticsGroup";
            this.statisticsGroup.Size = new System.Drawing.Size(569, 57);
            this.statisticsGroup.TabIndex = 2;
            this.statisticsGroup.TabStop = false;
            this.statisticsGroup.Text = "Statistics";
            // 
            // workingActivitiesTime
            // 
            this.workingActivitiesTime.Location = new System.Drawing.Point(140, 32);
            this.workingActivitiesTime.Name = "workingActivitiesTime";
            this.workingActivitiesTime.ReadOnly = true;
            this.workingActivitiesTime.Size = new System.Drawing.Size(34, 20);
            this.workingActivitiesTime.TabIndex = 4;
            this.workingActivitiesTime.Text = "12:34";
            this.workingActivitiesTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Working activities (h:mm):";
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 351);
            this.Controls.Add(this.statisticsGroup);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Summary";
            this.Text = "Summary";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.UpdateStatistics);
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.activities.ResumeLayout(false);
            this.tasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tasksSummary)).EndInit();
            this.statisticsGroup.ResumeLayout(false);
            this.statisticsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView activitiesSummary;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage activities;
        private System.Windows.Forms.TextBox allActivitiesTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tasks;
        private System.Windows.Forms.DataGridView tasksSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn activityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn spentColumnForActivitySummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumnForActivitySummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumnForTasksSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn spentColumnForTasksSummary;
        private System.Windows.Forms.GroupBox statisticsGroup;
        private System.Windows.Forms.TextBox workingActivitiesTime;
        private System.Windows.Forms.Label label2;
    }
}