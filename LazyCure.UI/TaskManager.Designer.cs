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
            this.addSiblingButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.deleteButton = new System.Windows.Forms.Button();
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
            this.treeView.Size = new System.Drawing.Size(153, 81);
            this.treeView.TabIndex = 0;
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
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
            this.splitContainer.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer.Panel2.Controls.Add(this.isWorkingCheckBox);
            this.splitContainer.Panel2.Controls.Add(this.addSiblingButton);
            this.splitContainer.Size = new System.Drawing.Size(256, 81);
            this.splitContainer.SplitterDistance = 153;
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
            // addSiblingButton
            // 
            this.addSiblingButton.Location = new System.Drawing.Point(4, 4);
            this.addSiblingButton.Name = "addSiblingButton";
            this.addSiblingButton.Size = new System.Drawing.Size(39, 23);
            this.addSiblingButton.TabIndex = 0;
            this.addSiblingButton.Text = "Add";
            this.toolTip.SetToolTip(this.addSiblingButton, "Add sibling task (Enter)");
            this.addSiblingButton.UseVisualStyleBackColor = true;
            this.addSiblingButton.Click += new System.EventHandler(this.addSiblingButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(49, 4);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(47, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.toolTip.SetToolTip(this.deleteButton, "Delete task (Del)");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // TaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(256, 81);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Task Manager";
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
        private System.Windows.Forms.Button addSiblingButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox isWorkingCheckBox;
        private System.Windows.Forms.Button deleteButton;
    }
}