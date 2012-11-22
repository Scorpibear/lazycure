namespace LifeIdea.LazyCure.UI
{
    partial class TimeLogEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLogEditor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timeLogView = new System.Windows.Forms.DataGridView();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.timeLogView)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeLogView
            // 
            resources.ApplyResources(this.timeLogView, "timeLogView");
            this.timeLogView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timeLogView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.Activity,
            this.Duration,
            this.End});
            this.timeLogView.ContextMenuStrip = this.contextMenu;
            this.timeLogView.Name = "timeLogView";
            dataGridViewCellStyle4.NullValue = null;
            this.timeLogView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.timeLogView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.timeLogView_CellEndEdit);
            this.timeLogView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.timeLogView_DataError);
            this.timeLogView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timeLogView_KeyDown);
            this.timeLogView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.timeLogView_KeyUp);
            // 
            // Start
            // 
            this.Start.DataPropertyName = "Start";
            dataGridViewCellStyle1.Format = "T";
            dataGridViewCellStyle1.NullValue = null;
            this.Start.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.Start, "Start");
            this.Start.Name = "Start";
            this.Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Activity
            // 
            this.Activity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Activity.DataPropertyName = "Activity";
            resources.ApplyResources(this.Activity, "Activity");
            this.Activity.Name = "Activity";
            this.Activity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Duration
            // 
            this.Duration.DataPropertyName = "Duration";
            dataGridViewCellStyle2.Format = "T";
            dataGridViewCellStyle2.NullValue = null;
            this.Duration.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.Duration, "Duration");
            this.Duration.Name = "Duration";
            this.Duration.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // End
            // 
            this.End.DataPropertyName = "End";
            dataGridViewCellStyle3.Format = "T";
            this.End.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.End, "End");
            this.End.Name = "End";
            this.End.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            // TimeLogEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timeLogView);
            this.MinimizeBox = false;
            this.Name = "TimeLogEditor";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.timeLogView)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView timeLogView;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
    }
}