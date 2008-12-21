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
            this.tasksContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddSubtask = new System.Windows.Forms.ToolStripMenuItem();
            this.miRename = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.renameButton = new System.Windows.Forms.Button();
            this.addSubtaskButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.isWorkingCheckBox = new System.Windows.Forms.CheckBox();
            this.addTaskButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tasksContextMenu.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.ContextMenuStrip = this.tasksContextMenu;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(149, 109);
            this.treeView.TabIndex = 0;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // tasksContextMenu
            // 
            this.tasksContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAdd,
            this.miAddSubtask,
            this.miRename,
            this.miDelete});
            this.tasksContextMenu.Name = "tasksContextMenu";
            this.tasksContextMenu.Size = new System.Drawing.Size(181, 92);
            // 
            // miAdd
            // 
            this.miAdd.Name = "miAdd";
            this.miAdd.ShortcutKeyDisplayString = "Enter";
            this.miAdd.Size = new System.Drawing.Size(180, 22);
            this.miAdd.Text = "Add task";
            this.miAdd.Click += new System.EventHandler(this.addTaskButton_Click);
            // 
            // miAddSubtask
            // 
            this.miAddSubtask.Name = "miAddSubtask";
            this.miAddSubtask.ShortcutKeyDisplayString = "Insert";
            this.miAddSubtask.Size = new System.Drawing.Size(180, 22);
            this.miAddSubtask.Text = "Add subtask";
            this.miAddSubtask.Click += new System.EventHandler(this.addSubtaskButton_Click);
            // 
            // miRename
            // 
            this.miRename.Name = "miRename";
            this.miRename.ShortcutKeyDisplayString = "F2";
            this.miRename.Size = new System.Drawing.Size(180, 22);
            this.miRename.Text = "Rename";
            this.miRename.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeyDisplayString = "Delete";
            this.miDelete.Size = new System.Drawing.Size(180, 22);
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.deleteButton_Click);
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
            this.splitContainer.Panel2.Controls.Add(this.renameButton);
            this.splitContainer.Panel2.Controls.Add(this.addSubtaskButton);
            this.splitContainer.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer.Panel2.Controls.Add(this.isWorkingCheckBox);
            this.splitContainer.Panel2.Controls.Add(this.addTaskButton);
            this.splitContainer.Size = new System.Drawing.Size(296, 109);
            this.splitContainer.SplitterDistance = 149;
            this.splitContainer.TabIndex = 1;
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(74, 3);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(66, 20);
            this.renameButton.TabIndex = 4;
            this.renameButton.Text = "rename";
            this.toolTip.SetToolTip(this.renameButton, "Rename task (F2)");
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // addSubtaskButton
            // 
            this.addSubtaskButton.Location = new System.Drawing.Point(2, 22);
            this.addSubtaskButton.Name = "addSubtaskButton";
            this.addSubtaskButton.Size = new System.Drawing.Size(66, 20);
            this.addSubtaskButton.TabIndex = 3;
            this.addSubtaskButton.Text = "+ subtask";
            this.toolTip.SetToolTip(this.addSubtaskButton, "Add subtask (Insert)");
            this.addSubtaskButton.UseVisualStyleBackColor = true;
            this.addSubtaskButton.Click += new System.EventHandler(this.addSubtaskButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(74, 22);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(66, 20);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "delete";
            this.toolTip.SetToolTip(this.deleteButton, "Delete task (Delete)");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // isWorkingCheckBox
            // 
            this.isWorkingCheckBox.AutoSize = true;
            this.isWorkingCheckBox.Location = new System.Drawing.Point(3, 48);
            this.isWorkingCheckBox.Name = "isWorkingCheckBox";
            this.isWorkingCheckBox.Size = new System.Drawing.Size(66, 17);
            this.isWorkingCheckBox.TabIndex = 1;
            this.isWorkingCheckBox.Text = "Working";
            this.isWorkingCheckBox.UseVisualStyleBackColor = true;
            this.isWorkingCheckBox.CheckedChanged += new System.EventHandler(this.isWorkingCheckBox_CheckedChanged);
            // 
            // addTaskButton
            // 
            this.addTaskButton.Location = new System.Drawing.Point(2, 3);
            this.addTaskButton.Name = "addTaskButton";
            this.addTaskButton.Size = new System.Drawing.Size(66, 20);
            this.addTaskButton.TabIndex = 0;
            this.addTaskButton.Text = "+ task";
            this.toolTip.SetToolTip(this.addTaskButton, "Add sibling task (Enter)");
            this.addTaskButton.UseVisualStyleBackColor = true;
            this.addTaskButton.Click += new System.EventHandler(this.addTaskButton_Click);
            // 
            // TaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(296, 109);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Task Manager";
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.TaskManager_VisibleChanged);
            this.tasksContextMenu.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button addTaskButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox isWorkingCheckBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ContextMenuStrip tasksContextMenu;
        private System.Windows.Forms.ToolStripMenuItem miAdd;
        private System.Windows.Forms.ToolStripMenuItem miRename;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.Button addSubtaskButton;
        private System.Windows.Forms.ToolStripMenuItem miAddSubtask;
        private System.Windows.Forms.Button renameButton;
    }
}