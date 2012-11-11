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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Summary));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.activitiesSummary = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.activities = new System.Windows.Forms.TabPage();
            this.tasks = new System.Windows.Forms.TabPage();
            this.tasksSummary = new System.Windows.Forms.DataGridView();
            this.efficiency = new System.Windows.Forms.TabPage();
            this.intervalsDefinitionGroupBox = new System.Windows.Forms.GroupBox();
            this.showTimeLogButton = new System.Windows.Forms.Button();
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
            this.closeButton = new System.Windows.Forms.Button();
            this.selectedRowsTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.workingActivitiesTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.activityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spentColumnForActivitySummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskColumnForActivitySummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskColumnForTasksSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spentColumnForTasksSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).BeginInit();
            this.contextMenu.SuspendLayout();
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
            resources.ApplyResources(this.activitiesSummary, "activitiesSummary");
            this.activitiesSummary.AllowUserToAddRows = false;
            this.activitiesSummary.AllowUserToDeleteRows = false;
            this.activitiesSummary.AllowUserToOrderColumns = true;
            this.activitiesSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activitiesSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.activityColumn,
            this.spentColumnForActivitySummary,
            this.taskColumnForActivitySummary});
            this.activitiesSummary.ContextMenuStrip = this.contextMenu;
            this.activitiesSummary.Name = "activitiesSummary";
            this.activitiesSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.activitiesSummary_CellDoubleClick);
            this.activitiesSummary.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.activitiesSummary_CellEndEdit);
            // 
            // contextMenu
            // 
            resources.ApplyResources(this.contextMenu, "contextMenu");
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.activities);
            this.tabControl.Controls.Add(this.tasks);
            this.tabControl.Controls.Add(this.efficiency);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // activities
            // 
            resources.ApplyResources(this.activities, "activities");
            this.activities.Controls.Add(this.activitiesSummary);
            this.activities.Name = "activities";
            this.activities.UseVisualStyleBackColor = true;
            // 
            // tasks
            // 
            resources.ApplyResources(this.tasks, "tasks");
            this.tasks.Controls.Add(this.tasksSummary);
            this.tasks.Name = "tasks";
            this.tasks.UseVisualStyleBackColor = true;
            // 
            // tasksSummary
            // 
            resources.ApplyResources(this.tasksSummary, "tasksSummary");
            this.tasksSummary.AllowUserToAddRows = false;
            this.tasksSummary.AllowUserToDeleteRows = false;
            this.tasksSummary.AllowUserToOrderColumns = true;
            this.tasksSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tasksSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskColumnForTasksSummary,
            this.spentColumnForTasksSummary});
            this.tasksSummary.ContextMenuStrip = this.contextMenu;
            this.tasksSummary.Name = "tasksSummary";
            this.tasksSummary.ReadOnly = true;
            // 
            // efficiency
            // 
            resources.ApplyResources(this.efficiency, "efficiency");
            this.efficiency.Controls.Add(this.intervalsDefinitionGroupBox);
            this.efficiency.Controls.Add(this.timeOnWorkTextBox);
            this.efficiency.Controls.Add(this.label5);
            this.efficiency.Controls.Add(this.efficiencyTextBox);
            this.efficiency.Controls.Add(this.label4);
            this.efficiency.Name = "efficiency";
            this.efficiency.UseVisualStyleBackColor = true;
            // 
            // intervalsDefinitionGroupBox
            // 
            resources.ApplyResources(this.intervalsDefinitionGroupBox, "intervalsDefinitionGroupBox");
            this.intervalsDefinitionGroupBox.Controls.Add(this.showTimeLogButton);
            this.intervalsDefinitionGroupBox.Controls.Add(this.workingTimeIntervalsGrid);
            this.intervalsDefinitionGroupBox.Controls.Add(this.maxRestDurationTextBox);
            this.intervalsDefinitionGroupBox.Controls.Add(this.label7);
            this.intervalsDefinitionGroupBox.Controls.Add(this.manuallyRadioButton);
            this.intervalsDefinitionGroupBox.Controls.Add(this.automaticallyRadioButton);
            this.intervalsDefinitionGroupBox.Name = "intervalsDefinitionGroupBox";
            this.intervalsDefinitionGroupBox.TabStop = false;
            // 
            // showTimeLogButton
            // 
            resources.ApplyResources(this.showTimeLogButton, "showTimeLogButton");
            this.showTimeLogButton.Name = "showTimeLogButton";
            this.showTimeLogButton.UseVisualStyleBackColor = true;
            this.showTimeLogButton.Click += new System.EventHandler(this.showTimeLogButton_Click);
            // 
            // workingTimeIntervalsGrid
            // 
            resources.ApplyResources(this.workingTimeIntervalsGrid, "workingTimeIntervalsGrid");
            this.workingTimeIntervalsGrid.ColumnHeadersVisible = false;
            this.workingTimeIntervalsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.End});
            this.workingTimeIntervalsGrid.Name = "workingTimeIntervalsGrid";
            this.workingTimeIntervalsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.workingTimeIntervalsGrid_CellClick);
            this.workingTimeIntervalsGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.workingTimeIntervalsGrid_DataError);
            // 
            // Start
            // 
            this.Start.DataPropertyName = "Start";
            dataGridViewCellStyle3.Format = "t";
            dataGridViewCellStyle3.NullValue = null;
            this.Start.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.Start, "Start");
            this.Start.Name = "Start";
            // 
            // End
            // 
            this.End.DataPropertyName = "End";
            dataGridViewCellStyle4.Format = "t";
            dataGridViewCellStyle4.NullValue = null;
            this.End.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.End, "End");
            this.End.Name = "End";
            // 
            // maxRestDurationTextBox
            // 
            resources.ApplyResources(this.maxRestDurationTextBox, "maxRestDurationTextBox");
            this.maxRestDurationTextBox.Name = "maxRestDurationTextBox";
            this.maxRestDurationTextBox.TextChanged += new System.EventHandler(this.maxRestDurationTextBox_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // manuallyRadioButton
            // 
            resources.ApplyResources(this.manuallyRadioButton, "manuallyRadioButton");
            this.manuallyRadioButton.Name = "manuallyRadioButton";
            this.manuallyRadioButton.UseVisualStyleBackColor = true;
            // 
            // automaticallyRadioButton
            // 
            resources.ApplyResources(this.automaticallyRadioButton, "automaticallyRadioButton");
            this.automaticallyRadioButton.Checked = true;
            this.automaticallyRadioButton.Name = "automaticallyRadioButton";
            this.automaticallyRadioButton.TabStop = true;
            this.automaticallyRadioButton.UseVisualStyleBackColor = true;
            this.automaticallyRadioButton.CheckedChanged += new System.EventHandler(this.automaticallyRadioButton_CheckedChanged);
            // 
            // timeOnWorkTextBox
            // 
            resources.ApplyResources(this.timeOnWorkTextBox, "timeOnWorkTextBox");
            this.timeOnWorkTextBox.Name = "timeOnWorkTextBox";
            this.timeOnWorkTextBox.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // efficiencyTextBox
            // 
            resources.ApplyResources(this.efficiencyTextBox, "efficiencyTextBox");
            this.efficiencyTextBox.Name = "efficiencyTextBox";
            this.efficiencyTextBox.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // allActivitiesTime
            // 
            resources.ApplyResources(this.allActivitiesTime, "allActivitiesTime");
            this.allActivitiesTime.Name = "allActivitiesTime";
            this.allActivitiesTime.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // statisticsGroup
            // 
            resources.ApplyResources(this.statisticsGroup, "statisticsGroup");
            this.statisticsGroup.Controls.Add(this.closeButton);
            this.statisticsGroup.Controls.Add(this.selectedRowsTime);
            this.statisticsGroup.Controls.Add(this.label3);
            this.statisticsGroup.Controls.Add(this.workingActivitiesTime);
            this.statisticsGroup.Controls.Add(this.label2);
            this.statisticsGroup.Controls.Add(this.allActivitiesTime);
            this.statisticsGroup.Controls.Add(this.label1);
            this.statisticsGroup.Name = "statisticsGroup";
            this.statisticsGroup.TabStop = false;
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // selectedRowsTime
            // 
            resources.ApplyResources(this.selectedRowsTime, "selectedRowsTime");
            this.selectedRowsTime.Name = "selectedRowsTime";
            this.selectedRowsTime.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // workingActivitiesTime
            // 
            resources.ApplyResources(this.workingActivitiesTime, "workingActivitiesTime");
            this.workingActivitiesTime.Name = "workingActivitiesTime";
            this.workingActivitiesTime.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statisticsGroup, 0, 1);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // activityColumn
            // 
            this.activityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.activityColumn.DataPropertyName = "Activity";
            this.activityColumn.FillWeight = 65F;
            resources.ApplyResources(this.activityColumn, "activityColumn");
            this.activityColumn.Name = "activityColumn";
            // 
            // spentColumnForActivitySummary
            // 
            this.spentColumnForActivitySummary.DataPropertyName = "Spent";
            this.spentColumnForActivitySummary.FillWeight = 10F;
            resources.ApplyResources(this.spentColumnForActivitySummary, "spentColumnForActivitySummary");
            this.spentColumnForActivitySummary.Name = "spentColumnForActivitySummary";
            this.spentColumnForActivitySummary.ReadOnly = true;
            this.spentColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // taskColumnForActivitySummary
            // 
            this.taskColumnForActivitySummary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.taskColumnForActivitySummary.DataPropertyName = "Task";
            this.taskColumnForActivitySummary.FillWeight = 35F;
            resources.ApplyResources(this.taskColumnForActivitySummary, "taskColumnForActivitySummary");
            this.taskColumnForActivitySummary.Name = "taskColumnForActivitySummary";
            this.taskColumnForActivitySummary.ReadOnly = true;
            this.taskColumnForActivitySummary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // taskColumnForTasksSummary
            // 
            this.taskColumnForTasksSummary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.taskColumnForTasksSummary.DataPropertyName = "Task";
            resources.ApplyResources(this.taskColumnForTasksSummary, "taskColumnForTasksSummary");
            this.taskColumnForTasksSummary.Name = "taskColumnForTasksSummary";
            this.taskColumnForTasksSummary.ReadOnly = true;
            // 
            // spentColumnForTasksSummary
            // 
            this.spentColumnForTasksSummary.DataPropertyName = "Spent";
            resources.ApplyResources(this.spentColumnForTasksSummary, "spentColumnForTasksSummary");
            this.spentColumnForTasksSummary.Name = "spentColumnForTasksSummary";
            this.spentColumnForTasksSummary.ReadOnly = true;
            this.spentColumnForTasksSummary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Summary
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimizeBox = false;
            this.Name = "Summary";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.UpdateStatistics);
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).EndInit();
            this.contextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.Button showTimeLogButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn activityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn spentColumnForActivitySummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumnForActivitySummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskColumnForTasksSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn spentColumnForTasksSummary;
    }
}