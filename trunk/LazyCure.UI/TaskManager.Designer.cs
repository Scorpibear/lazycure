namespace LifeIdea.LazyCure.UI
{
    partial class TaskManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskManager));
            this.treeView = new System.Windows.Forms.TreeView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.isWorkingCheckBox = new System.Windows.Forms.CheckBox();
            this.addSibling = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(110, 83);
            this.treeView.TabIndex = 0;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.isWorkingCheckBox);
            this.splitContainer.Panel2.Controls.Add(this.addSibling);
            this.splitContainer.Size = new System.Drawing.Size(183, 83);
            this.splitContainer.SplitterDistance = 110;
            this.splitContainer.TabIndex = 1;
            // 
            // isWorkingCheckBox
            // 
            this.isWorkingCheckBox.AutoSize = true;
            this.isWorkingCheckBox.Location = new System.Drawing.Point(4, 34);
            this.isWorkingCheckBox.Name = "isWorkingCheckBox";
            this.isWorkingCheckBox.Size = new System.Drawing.Size(66, 17);
            this.isWorkingCheckBox.TabIndex = 1;
            this.isWorkingCheckBox.Text = "Working";
            this.isWorkingCheckBox.UseVisualStyleBackColor = true;
            this.isWorkingCheckBox.CheckedChanged += new System.EventHandler(this.isWorkingCheckBox_CheckedChanged);
            // 
            // addSibling
            // 
            this.addSibling.Location = new System.Drawing.Point(4, 4);
            this.addSibling.Name = "addSibling";
            this.addSibling.Size = new System.Drawing.Size(52, 23);
            this.addSibling.TabIndex = 0;
            this.addSibling.Text = "Add";
            this.toolTip.SetToolTip(this.addSibling, "Add sibling task");
            this.addSibling.UseVisualStyleBackColor = true;
            this.addSibling.Click += new System.EventHandler(this.addSibling_Click);
            // 
            // taskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(183, 83);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "taskManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "taskManager";
            this.TopMost = true;
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button addSibling;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox isWorkingCheckBox;
    }
}