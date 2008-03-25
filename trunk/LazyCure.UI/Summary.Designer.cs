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
            this.allActivitiesTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tasks = new System.Windows.Forms.TabPage();
            this.tasksSummary = new System.Windows.Forms.DataGridView();
            this.taskColumnForTasksSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spentColumnForTasksSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).BeginInit();
            this.tabControl.SuspendLayout();
            this.activities.SuspendLayout();
            this.tasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksSummary)).BeginInit();
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
            this.activitiesSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.activitiesSummary.Location = new System.Drawing.Point(3, 3);
            this.activitiesSummary.Name = "activitiesSummary";
            this.activitiesSummary.Size = new System.Drawing.Size(532, 276);
            this.activitiesSummary.TabIndex = 0;
            this.activitiesSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.activitiesSummary_CellDoubleClick);
            // 
            // activityColumn
            // 
            this.activityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.activityColumn.DataPropertyName = "Activity";
            this.activityColumn.HeaderText = "Activity";
            this.activityColumn.Name = "activityColumn";
            // 
            // spentColumnForActivitySummary
            // 
            this.spentColumnForActivitySummary.DataPropertyName = "Spent";
            this.spentColumnForActivitySummary.HeaderText = "Spent";
            this.spentColumnForActivitySummary.Name = "spentColumnForActivitySummary";
            this.spentColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spentColumnForActivitySummary.Width = 50;
            // 
            // taskColumnForActivitySummary
            // 
            this.taskColumnForActivitySummary.DataPropertyName = "Task";
            this.taskColumnForActivitySummary.HeaderText = "Task";
            this.taskColumnForActivitySummary.Name = "taskColumnForActivitySummary";
            this.taskColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.taskColumnForActivitySummary.Width = 150;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.activities);
            this.tabControl.Controls.Add(this.tasks);
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
            // tasks
            // 
            this.tasks.Controls.Add(this.tasksSummary);
            this.tasks.Location = new System.Drawing.Point(4, 22);
            this.tasks.Name = "tasks";
            this.tasks.Size = new System.Drawing.Size(538, 325);
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
            this.tasksSummary.Size = new System.Drawing.Size(532, 276);
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
            this.tasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tasksSummary)).EndInit();
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
    }
}