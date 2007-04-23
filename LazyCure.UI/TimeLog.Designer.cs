namespace LifeIdea.LazyCure.UI
{
    partial class TimeLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timeLogView = new System.Windows.Forms.DataGridView();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.timeLogView)).BeginInit();
            this.SuspendLayout();
            // 
            // timeLogView
            // 
            this.timeLogView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timeLogView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.Activity,
            this.Duration,
            this.End});
            this.timeLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLogView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.timeLogView.Location = new System.Drawing.Point(0, 0);
            this.timeLogView.Name = "timeLogView";
            this.timeLogView.RowHeadersWidth = 32;
            dataGridViewCellStyle8.NullValue = null;
            this.timeLogView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.timeLogView.Size = new System.Drawing.Size(513, 343);
            this.timeLogView.TabIndex = 0;
            this.timeLogView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.timeLogView_CellValidating);
            this.timeLogView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.timeLogView_CellValueChanged);
            this.timeLogView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.timeLogView_KeyUp);
            // 
            // Start
            // 
            dataGridViewCellStyle5.Format = "T";
            dataGridViewCellStyle5.NullValue = null;
            this.Start.DefaultCellStyle = dataGridViewCellStyle5;
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            this.Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Start.Width = 50;
            // 
            // Activity
            // 
            this.Activity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Activity.HeaderText = "Activity";
            this.Activity.Name = "Activity";
            // 
            // Duration
            // 
            dataGridViewCellStyle6.Format = "T";
            dataGridViewCellStyle6.NullValue = "00:00:00";
            this.Duration.DefaultCellStyle = dataGridViewCellStyle6;
            this.Duration.HeaderText = "Duration";
            this.Duration.Name = "Duration";
            this.Duration.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Duration.Width = 50;
            // 
            // End
            // 
            dataGridViewCellStyle7.Format = "T";
            this.End.DefaultCellStyle = dataGridViewCellStyle7;
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.End.Width = 50;
            // 
            // TimeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 343);
            this.Controls.Add(this.timeLogView);
            this.MaximumSize = new System.Drawing.Size(1600, 1200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(24, 24);
            this.Name = "TimeLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Time Log";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.timeLogView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView timeLogView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
    }
}