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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.tasksContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddSubtask = new System.Windows.Forms.ToolStripMenuItem();
            this.miRename = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.closeButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.addSubtaskButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.isWorkingCheckBox = new System.Windows.Forms.CheckBox();
            this.addTaskButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tasksContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.toolTip.SetToolTip(this.splitContainer.Panel1, resources.GetString("splitContainer.Panel1.ToolTip"));
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.Controls.Add(this.closeButton);
            this.splitContainer.Panel2.Controls.Add(this.renameButton);
            this.splitContainer.Panel2.Controls.Add(this.addSubtaskButton);
            this.splitContainer.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer.Panel2.Controls.Add(this.isWorkingCheckBox);
            this.splitContainer.Panel2.Controls.Add(this.addTaskButton);
            this.toolTip.SetToolTip(this.splitContainer.Panel2, resources.GetString("splitContainer.Panel2.ToolTip"));
            this.toolTip.SetToolTip(this.splitContainer, resources.GetString("splitContainer.ToolTip"));
            // 
            // treeView
            // 
            resources.ApplyResources(this.treeView, "treeView");
            this.treeView.ContextMenuStrip = this.tasksContextMenu;
            this.treeView.HideSelection = false;
            this.treeView.LabelEdit = true;
            this.treeView.Name = "treeView";
            this.toolTip.SetToolTip(this.treeView, resources.GetString("treeView.ToolTip"));
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // tasksContextMenu
            // 
            resources.ApplyResources(this.tasksContextMenu, "tasksContextMenu");
            this.tasksContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAdd,
            this.miAddSubtask,
            this.miRename,
            this.miDelete});
            this.tasksContextMenu.Name = "tasksContextMenu";
            this.toolTip.SetToolTip(this.tasksContextMenu, resources.GetString("tasksContextMenu.ToolTip"));
            // 
            // miAdd
            // 
            resources.ApplyResources(this.miAdd, "miAdd");
            this.miAdd.Name = "miAdd";
            this.miAdd.Click += new System.EventHandler(this.addTaskButton_Click);
            // 
            // miAddSubtask
            // 
            resources.ApplyResources(this.miAddSubtask, "miAddSubtask");
            this.miAddSubtask.Name = "miAddSubtask";
            this.miAddSubtask.Click += new System.EventHandler(this.addSubtaskButton_Click);
            // 
            // miRename
            // 
            resources.ApplyResources(this.miRename, "miRename");
            this.miRename.Name = "miRename";
            this.miRename.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // miDelete
            // 
            resources.ApplyResources(this.miDelete, "miDelete");
            this.miDelete.Name = "miDelete";
            this.miDelete.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            this.toolTip.SetToolTip(this.closeButton, resources.GetString("closeButton.ToolTip"));
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // renameButton
            // 
            resources.ApplyResources(this.renameButton, "renameButton");
            this.renameButton.Name = "renameButton";
            this.toolTip.SetToolTip(this.renameButton, resources.GetString("renameButton.ToolTip"));
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // addSubtaskButton
            // 
            resources.ApplyResources(this.addSubtaskButton, "addSubtaskButton");
            this.addSubtaskButton.Name = "addSubtaskButton";
            this.toolTip.SetToolTip(this.addSubtaskButton, resources.GetString("addSubtaskButton.ToolTip"));
            this.addSubtaskButton.UseVisualStyleBackColor = true;
            this.addSubtaskButton.Click += new System.EventHandler(this.addSubtaskButton_Click);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.toolTip.SetToolTip(this.deleteButton, resources.GetString("deleteButton.ToolTip"));
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // isWorkingCheckBox
            // 
            resources.ApplyResources(this.isWorkingCheckBox, "isWorkingCheckBox");
            this.isWorkingCheckBox.Name = "isWorkingCheckBox";
            this.toolTip.SetToolTip(this.isWorkingCheckBox, resources.GetString("isWorkingCheckBox.ToolTip"));
            this.isWorkingCheckBox.UseVisualStyleBackColor = true;
            this.isWorkingCheckBox.CheckedChanged += new System.EventHandler(this.isWorkingCheckBox_CheckedChanged);
            // 
            // addTaskButton
            // 
            resources.ApplyResources(this.addTaskButton, "addTaskButton");
            this.addTaskButton.Name = "addTaskButton";
            this.toolTip.SetToolTip(this.addTaskButton, resources.GetString("addTaskButton.ToolTip"));
            this.addTaskButton.UseVisualStyleBackColor = true;
            this.addTaskButton.Click += new System.EventHandler(this.addTaskButton_Click);
            // 
            // TaskManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.Controls.Add(this.splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskManager";
            this.ShowInTaskbar = false;
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.TaskManager_VisibleChanged);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tasksContextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.Button closeButton;
    }
}