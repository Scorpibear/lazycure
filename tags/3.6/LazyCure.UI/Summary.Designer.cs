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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.efficiency = new System.Windows.Forms.TabPage();
            this.intervalsDefinitionGroupBox = new System.Windows.Forms.GroupBox();
            this.workingTimeIntervalsGrid = new System.Windows.Forms.DataGridView();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxRestDurationTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.manuallyRadioButton = new System.Windows.Forms.RadioButton();
            this.automaticallyRadioButton = new System.Windows.Forms.RadioButton();
            this.timeOnWorkTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.efficiencyTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.allActivitiesTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statisticsGroup = new System.Windows.Forms.GroupBox();
            this.selectedRowsTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.workingActivitiesTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.showTimeLogButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).BeginInit();
            this.tabControl.SuspendLayout();
            this.activities.SuspendLayout();
            this.tasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksSummary)).BeginInit();
            this.efficiency.SuspendLayout();
            this.intervalsDefinitionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workingTimeIntervalsGrid)).BeginInit();
            this.statisticsGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.activitiesSummary.Size = new System.Drawing.Size(549, 253);
            this.activitiesSummary.TabIndex = 0;
            this.activitiesSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.activitiesSummary_CellDoubleClick);
            // 
            // activityColumn
            // 
            this.activityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.activityColumn.DataPropertyName = "Activity";
            this.activityColumn.FillWeight = 65F;
            this.activityColumn.HeaderText = "Activity";
            this.activityColumn.Name = "activityColumn";
            this.activityColumn.ReadOnly = true;
            // 
            // spentColumnForActivitySummary
            // 
            this.spentColumnForActivitySummary.DataPropertyName = "Spent";
            this.spentColumnForActivitySummary.FillWeight = 10F;
            this.spentColumnForActivitySummary.HeaderText = "Spent";
            this.spentColumnForActivitySummary.Name = "spentColumnForActivitySummary";
            this.spentColumnForActivitySummary.ReadOnly = true;
            this.spentColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spentColumnForActivitySummary.Width = 50;
            // 
            // taskColumnForActivitySummary
            // 
            this.taskColumnForActivitySummary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.taskColumnForActivitySummary.DataPropertyName = "Task";
            this.taskColumnForActivitySummary.FillWeight = 35F;
            this.taskColumnForActivitySummary.HeaderText = "Task";
            this.taskColumnForActivitySummary.Name = "taskColumnForActivitySummary";
            this.taskColumnForActivitySummary.ReadOnly = true;
            this.taskColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.activities);
            this.tabControl.Controls.Add(this.tasks);
            this.tabControl.Controls.Add(this.efficiency);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(563, 285);
            this.tabControl.TabIndex = 1;
            // 
            // activities
            // 
            this.activities.Controls.Add(this.activitiesSummary);
            this.activities.Location = new System.Drawing.Point(4, 22);
            this.activities.Name = "activities";
            this.activities.Padding = new System.Windows.Forms.Padding(3);
            this.activities.Size = new System.Drawing.Size(555, 259);
            this.activities.TabIndex = 0;
            this.activities.Text = "Activities";
            this.activities.UseVisualStyleBackColor = true;
            // 
            // tasks
            // 
            this.tasks.Controls.Add(this.tasksSummary);
            this.tasks.Location = new System.Drawing.Point(4, 22);
            this.tasks.Name = "tasks";
            this.tasks.Padding = new System.Windows.Forms.Padding(3);
            this.tasks.Size = new System.Drawing.Size(555, 259);
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
            this.tasksSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tasksSummary.Location = new System.Drawing.Point(3, 3);
            this.tasksSummary.Name = "tasksSummary";
            this.tasksSummary.ReadOnly = true;
            this.tasksSummary.Size = new System.Drawing.Size(549, 253);
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
            // efficiency
            // 
            this.efficiency.Controls.Add(this.intervalsDefinitionGroupBox);
            this.efficiency.Controls.Add(this.timeOnWorkTextBox);
            this.efficiency.Controls.Add(this.label5);
            this.efficiency.Controls.Add(this.efficiencyTextBox);
            this.efficiency.Controls.Add(this.label4);
            this.efficiency.Location = new System.Drawing.Point(4, 22);
            this.efficiency.Name = "efficiency";
            this.efficiency.Padding = new System.Windows.Forms.Padding(3);
            this.efficiency.Size = new System.Drawing.Size(555, 259);
            this.efficiency.TabIndex = 2;
            this.efficiency.Text = "Efficiency";
            this.efficiency.UseVisualStyleBackColor = true;
            // 
            // intervalsDefinitionGroupBox
            // 
            this.intervalsDefinitionGroupBox.Controls.Add(this.showTimeLogButton);
            this.intervalsDefinitionGroupBox.Controls.Add(this.workingTimeIntervalsGrid);
            this.intervalsDefinitionGroupBox.Controls.Add(this.maxRestDurationTextBox);
            this.intervalsDefinitionGroupBox.Controls.Add(this.label7);
            this.intervalsDefinitionGroupBox.Controls.Add(this.manuallyRadioButton);
            this.intervalsDefinitionGroupBox.Controls.Add(this.automaticallyRadioButton);
            this.intervalsDefinitionGroupBox.Location = new System.Drawing.Point(11, 48);
            this.intervalsDefinitionGroupBox.Name = "intervalsDefinitionGroupBox";
            this.intervalsDefinitionGroupBox.Size = new System.Drawing.Size(269, 205);
            this.intervalsDefinitionGroupBox.TabIndex = 5;
            this.intervalsDefinitionGroupBox.TabStop = false;
            this.intervalsDefinitionGroupBox.Text = "Time on work definition";
            // 
            // workingTimeIntervalsGrid
            // 
            this.workingTimeIntervalsGrid.ColumnHeadersVisible = false;
            this.workingTimeIntervalsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.End});
            this.workingTimeIntervalsGrid.Location = new System.Drawing.Point(26, 81);
            this.workingTimeIntervalsGrid.Name = "workingTimeIntervalsGrid";
            this.workingTimeIntervalsGrid.RowHeadersWidth = 30;
            this.workingTimeIntervalsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.workingTimeIntervalsGrid.Size = new System.Drawing.Size(129, 118);
            this.workingTimeIntervalsGrid.TabIndex = 9;
            this.workingTimeIntervalsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.workingTimeIntervalsGrid_CellClick);
            // 
            // Start
            // 
            this.Start.DataPropertyName = "Start";
            dataGridViewCellStyle1.Format = "t";
            dataGridViewCellStyle1.NullValue = null;
            this.Start.DefaultCellStyle = dataGridViewCellStyle1;
            this.Start.HeaderText = "Start";
            this.Start.MinimumWidth = 35;
            this.Start.Name = "Start";
            this.Start.Width = 35;
            // 
            // End
            // 
            this.End.DataPropertyName = "End";
            dataGridViewCellStyle2.Format = "t";
            dataGridViewCellStyle2.NullValue = null;
            this.End.DefaultCellStyle = dataGridViewCellStyle2;
            this.End.HeaderText = "End";
            this.End.MinimumWidth = 35;
            this.End.Name = "End";
            this.End.Width = 35;
            // 
            // maxRestDurationTextBox
            // 
            this.maxRestDurationTextBox.Location = new System.Drawing.Point(231, 38);
            this.maxRestDurationTextBox.Mask = "0:00";
            this.maxRestDurationTextBox.Name = "maxRestDurationTextBox";
            this.maxRestDurationTextBox.Size = new System.Drawing.Size(31, 20);
            this.maxRestDurationTextBox.TabIndex = 8;
            this.maxRestDurationTextBox.Text = "015";
            this.maxRestDurationTextBox.TextChanged += new System.EventHandler(this.maxRestDurationTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(205, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Possible work interruption duration (h:mm):";
            // 
            // manuallyRadioButton
            // 
            this.manuallyRadioButton.AutoSize = true;
            this.manuallyRadioButton.Location = new System.Drawing.Point(7, 57);
            this.manuallyRadioButton.Name = "manuallyRadioButton";
            this.manuallyRadioButton.Size = new System.Drawing.Size(66, 17);
            this.manuallyRadioButton.TabIndex = 1;
            this.manuallyRadioButton.Text = "manually";
            this.manuallyRadioButton.UseVisualStyleBackColor = true;
            // 
            // automaticallyRadioButton
            // 
            this.automaticallyRadioButton.AutoSize = true;
            this.automaticallyRadioButton.Checked = true;
            this.automaticallyRadioButton.Location = new System.Drawing.Point(7, 20);
            this.automaticallyRadioButton.Name = "automaticallyRadioButton";
            this.automaticallyRadioButton.Size = new System.Drawing.Size(86, 17);
            this.automaticallyRadioButton.TabIndex = 0;
            this.automaticallyRadioButton.TabStop = true;
            this.automaticallyRadioButton.Text = "automatically";
            this.automaticallyRadioButton.UseVisualStyleBackColor = true;
            this.automaticallyRadioButton.CheckedChanged += new System.EventHandler(this.automaticallyRadioButton_CheckedChanged);
            // 
            // timeOnWorkTextBox
            // 
            this.timeOnWorkTextBox.Location = new System.Drawing.Point(132, 22);
            this.timeOnWorkTextBox.Name = "timeOnWorkTextBox";
            this.timeOnWorkTextBox.ReadOnly = true;
            this.timeOnWorkTextBox.Size = new System.Drawing.Size(34, 20);
            this.timeOnWorkTextBox.TabIndex = 4;
            this.timeOnWorkTextBox.Text = "8:00";
            this.timeOnWorkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Time on work (h:mm):";
            // 
            // efficiencyTextBox
            // 
            this.efficiencyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.efficiencyTextBox.Location = new System.Drawing.Point(401, 3);
            this.efficiencyTextBox.Name = "efficiencyTextBox";
            this.efficiencyTextBox.ReadOnly = true;
            this.efficiencyTextBox.Size = new System.Drawing.Size(148, 62);
            this.efficiencyTextBox.TabIndex = 1;
            this.efficiencyTextBox.Text = "85%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(377, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Working time usage efficiency ( [Working activities] / [Time on work]  * 100% ):";
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
            this.statisticsGroup.Controls.Add(this.selectedRowsTime);
            this.statisticsGroup.Controls.Add(this.label3);
            this.statisticsGroup.Controls.Add(this.workingActivitiesTime);
            this.statisticsGroup.Controls.Add(this.label2);
            this.statisticsGroup.Controls.Add(this.allActivitiesTime);
            this.statisticsGroup.Controls.Add(this.label1);
            this.statisticsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statisticsGroup.Location = new System.Drawing.Point(3, 294);
            this.statisticsGroup.Name = "statisticsGroup";
            this.statisticsGroup.Size = new System.Drawing.Size(563, 54);
            this.statisticsGroup.TabIndex = 2;
            this.statisticsGroup.TabStop = false;
            this.statisticsGroup.Text = "Statistics";
            // 
            // selectedRowsTime
            // 
            this.selectedRowsTime.Location = new System.Drawing.Point(332, 13);
            this.selectedRowsTime.Name = "selectedRowsTime";
            this.selectedRowsTime.ReadOnly = true;
            this.selectedRowsTime.Size = new System.Drawing.Size(34, 20);
            this.selectedRowsTime.TabIndex = 6;
            this.selectedRowsTime.Text = "12:34";
            this.selectedRowsTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Time in selected rows (h:mm):";
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statisticsGroup, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(569, 351);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // showTimeLogButton
            // 
            this.showTimeLogButton.Location = new System.Drawing.Point(168, 81);
            this.showTimeLogButton.Name = "showTimeLogButton";
            this.showTimeLogButton.Size = new System.Drawing.Size(94, 23);
            this.showTimeLogButton.TabIndex = 10;
            this.showTimeLogButton.Text = "Show Time Log";
            this.showTimeLogButton.UseVisualStyleBackColor = true;
            this.showTimeLogButton.Click += new System.EventHandler(this.showTimeLogButton_Click);
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 351);
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.efficiency.ResumeLayout(false);
            this.efficiency.PerformLayout();
            this.intervalsDefinitionGroupBox.ResumeLayout(false);
            this.intervalsDefinitionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workingTimeIntervalsGrid)).EndInit();
            this.statisticsGroup.ResumeLayout(false);
            this.statisticsGroup.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumnForTasksSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn spentColumnForTasksSummary;
        private System.Windows.Forms.GroupBox statisticsGroup;
        private System.Windows.Forms.TextBox workingActivitiesTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox selectedRowsTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage efficiency;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox efficiencyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox intervalsDefinitionGroupBox;
        private System.Windows.Forms.RadioButton automaticallyRadioButton;
        private System.Windows.Forms.TextBox timeOnWorkTextBox;
        private System.Windows.Forms.RadioButton manuallyRadioButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox maxRestDurationTextBox;
        private System.Windows.Forms.DataGridView workingTimeIntervalsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn activityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn spentColumnForActivitySummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumnForActivitySummary;
        private System.Windows.Forms.Button showTimeLogButton;
    }
}