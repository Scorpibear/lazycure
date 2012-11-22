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
            this.activityLabel = new System.Windows.Forms.Label();
            this.daySpentDataGrid = new System.Windows.Forms.DataGridView();
            this.day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activityComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.daySpentDataGrid)).BeginInit();
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
            // activityLabel
            // 
            resources.ApplyResources(this.activityLabel, "activityLabel");
            this.activityLabel.Name = "activityLabel";
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
            // activityComboBox
            // 
            resources.ApplyResources(this.activityComboBox, "activityComboBox");
            this.activityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activityComboBox.FormattingEnabled = true;
            this.activityComboBox.Name = "activityComboBox";
            this.activityComboBox.SelectedValueChanged += new System.EventHandler(this.activityComboBox_SelectedValueChanged);
            // 
            // SpentInDiffDaysView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.activityLabel);
            this.Controls.Add(this.daySpentDataGrid);
            this.Controls.Add(this.activityComboBox);
            this.Name = "SpentInDiffDaysView";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.SpentOnDiffDays_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.daySpentDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox activityComboBox;
        private System.Windows.Forms.DataGridView daySpentDataGrid;
        private System.Windows.Forms.Label activityLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn day;
        private System.Windows.Forms.DataGridViewTextBoxColumn spent;
    }
}