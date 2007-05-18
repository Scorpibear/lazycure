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
            this.activitiesSummary = new System.Windows.Forms.DataGridView();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // activitiesSummary
            // 
            this.activitiesSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activitiesSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Activity,
            this.Spent,
            this.Task});
            this.activitiesSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activitiesSummary.Location = new System.Drawing.Point(0, 0);
            this.activitiesSummary.Name = "activitiesSummary";
            this.activitiesSummary.Size = new System.Drawing.Size(513, 340);
            this.activitiesSummary.TabIndex = 0;
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
            this.Spent.Width = 50;
            // 
            // Task
            // 
            this.Task.DataPropertyName = "Task";
            this.Task.HeaderText = "Task";
            this.Task.Name = "Task";
            this.Task.ReadOnly = true;
            this.Task.Width = 150;
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 340);
            this.Controls.Add(this.activitiesSummary);
            this.MinimizeBox = false;
            this.Name = "Summary";
            this.Text = "Summary";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.activitiesSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView activitiesSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spent;
        private System.Windows.Forms.DataGridViewComboBoxColumn Task;
    }
}