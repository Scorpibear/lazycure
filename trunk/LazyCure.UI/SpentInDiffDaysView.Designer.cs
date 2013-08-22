namespace LifeIdea.LazyCure.UI
{
    partial class SpentInDiffDaysView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpentInDiffDaysView));
            this.closeButton = new System.Windows.Forms.Button();
            this.daySpentDataGrid = new System.Windows.Forms.DataGridView();
            this.day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activityOrTaskValueComboBox = new System.Windows.Forms.ComboBox();
            this.totalSpentOnGroup = new System.Windows.Forms.GroupBox();
            this.taskRadioButton = new System.Windows.Forms.RadioButton();
            this.activityRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.daySpentDataGrid)).BeginInit();
            this.totalSpentOnGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // daySpentDataGrid
            // 
            resources.ApplyResources(this.daySpentDataGrid, "daySpentDataGrid");
            this.daySpentDataGrid.AllowUserToAddRows = false;
            this.daySpentDataGrid.AllowUserToDeleteRows = false;
            this.daySpentDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.daySpentDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.day,
            this.spent});
            this.daySpentDataGrid.Name = "daySpentDataGrid";
            this.daySpentDataGrid.ReadOnly = true;
            this.daySpentDataGrid.RowHeadersVisible = false;
            this.daySpentDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.daySpentDataGrid_DataError);
            this.daySpentDataGrid.BindingContextChanged += new System.EventHandler(this.daySpentDataGrid_BindingContextChanged);
            // 
            // day
            // 
            this.day.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.day.DataPropertyName = "Day";
            resources.ApplyResources(this.day, "day");
            this.day.Name = "day";
            this.day.ReadOnly = true;
            // 
            // spent
            // 
            this.spent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.spent.DataPropertyName = "Spent";
            resources.ApplyResources(this.spent, "spent");
            this.spent.Name = "spent";
            this.spent.ReadOnly = true;
            // 
            // activityOrTaskValueComboBox
            // 
            resources.ApplyResources(this.activityOrTaskValueComboBox, "activityOrTaskValueComboBox");
            this.activityOrTaskValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activityOrTaskValueComboBox.FormattingEnabled = true;
            this.activityOrTaskValueComboBox.Name = "activityOrTaskValueComboBox";
            this.activityOrTaskValueComboBox.SelectedValueChanged += new System.EventHandler(this.activityComboBox_SelectedValueChanged);
            // 
            // totalSpentOnGroup
            // 
            resources.ApplyResources(this.totalSpentOnGroup, "totalSpentOnGroup");
            this.totalSpentOnGroup.Controls.Add(this.taskRadioButton);
            this.totalSpentOnGroup.Controls.Add(this.activityRadioButton);
            this.totalSpentOnGroup.Controls.Add(this.activityOrTaskValueComboBox);
            this.totalSpentOnGroup.Name = "totalSpentOnGroup";
            this.totalSpentOnGroup.TabStop = false;
            // 
            // taskRadioButton
            // 
            resources.ApplyResources(this.taskRadioButton, "taskRadioButton");
            this.taskRadioButton.Name = "taskRadioButton";
            this.taskRadioButton.UseVisualStyleBackColor = true;
            // 
            // activityRadioButton
            // 
            resources.ApplyResources(this.activityRadioButton, "activityRadioButton");
            this.activityRadioButton.Checked = true;
            this.activityRadioButton.Name = "activityRadioButton";
            this.activityRadioButton.TabStop = true;
            this.activityRadioButton.UseVisualStyleBackColor = true;
            this.activityRadioButton.CheckedChanged += new System.EventHandler(this.activityRadioButton_CheckedChanged);
            // 
            // SpentInDiffDaysView
            // 
            this.AcceptButton = this.closeButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.Controls.Add(this.totalSpentOnGroup);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.daySpentDataGrid);
            this.Name = "SpentInDiffDaysView";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.SpentOnDiffDays_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.daySpentDataGrid)).EndInit();
            this.totalSpentOnGroup.ResumeLayout(false);
            this.totalSpentOnGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox activityOrTaskValueComboBox;
        private System.Windows.Forms.DataGridView daySpentDataGrid;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.GroupBox totalSpentOnGroup;
        private System.Windows.Forms.RadioButton taskRadioButton;
        private System.Windows.Forms.RadioButton activityRadioButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn day;
        private System.Windows.Forms.DataGridViewTextBoxColumn spent;
    }
}