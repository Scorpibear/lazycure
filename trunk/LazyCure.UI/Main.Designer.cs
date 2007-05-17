namespace LifeIdea.LazyCure.UI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.currentActivity = new System.Windows.Forms.ComboBox();
            this.switchButton = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.activityStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.activityDuration = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.nowYou = new System.Windows.Forms.GroupBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miShow = new System.Windows.Forms.ToolStripMenuItem();
            this.showMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miActivityDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.miTimeLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusBar.SuspendLayout();
            this.nowYou.SuspendLayout();
            this.menu.SuspendLayout();
            this.showMenu.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentActivity
            // 
            this.currentActivity.BackColor = System.Drawing.SystemColors.Window;
            this.currentActivity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.currentActivity.FormattingEnabled = true;
            this.currentActivity.Location = new System.Drawing.Point(6, 19);
            this.currentActivity.Name = "currentActivity";
            this.currentActivity.Size = new System.Drawing.Size(203, 21);
            this.currentActivity.TabIndex = 0;
            this.toolTip.SetToolTip(this.currentActivity, "Enter an activity that you are doing right now");
            // 
            // switchButton
            // 
            this.switchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.switchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.switchButton.Location = new System.Drawing.Point(215, 13);
            this.switchButton.Name = "switchButton";
            this.switchButton.Size = new System.Drawing.Size(70, 37);
            this.switchButton.TabIndex = 2;
            this.switchButton.Text = "Done!";
            this.toolTip.SetToolTip(this.switchButton, "Press the button, when you have finished the current activity and are starting an" +
                    "other one");
            this.switchButton.UseVisualStyleBackColor = true;
            this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activityStartTime,
            this.activityDuration,
            this.currentTime});
            this.statusBar.Location = new System.Drawing.Point(0, 0);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.Size = new System.Drawing.Size(294, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "Status Bar";
            // 
            // activityStartTime
            // 
            this.activityStartTime.Name = "activityStartTime";
            this.activityStartTime.Size = new System.Drawing.Size(93, 17);
            this.activityStartTime.Spring = true;
            this.activityStartTime.Text = "00:00:00";
            this.activityStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.activityStartTime.ToolTipText = "Activity start time";
            // 
            // activityDuration
            // 
            this.activityDuration.Name = "activityDuration";
            this.activityDuration.Size = new System.Drawing.Size(93, 17);
            this.activityDuration.Spring = true;
            this.activityDuration.Text = "00:00:00";
            this.activityDuration.ToolTipText = "Activity duration";
            // 
            // currentTime
            // 
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(93, 17);
            this.currentTime.Spring = true;
            this.currentTime.Text = "00:00:00";
            this.currentTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.currentTime.ToolTipText = "Current time";
            // 
            // nowYou
            // 
            this.nowYou.Controls.Add(this.currentActivity);
            this.nowYou.Controls.Add(this.switchButton);
            this.nowYou.Location = new System.Drawing.Point(3, 3);
            this.nowYou.Name = "nowYou";
            this.nowYou.Size = new System.Drawing.Size(290, 54);
            this.nowYou.TabIndex = 3;
            this.nowYou.TabStop = false;
            this.nowYou.Text = "Now you...";
            // 
            // menu
            // 
            this.menu.Dock = System.Windows.Forms.DockStyle.None;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miShow,
            this.miHelp});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(294, 24);
            this.menu.TabIndex = 4;
            this.menu.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSave,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(35, 20);
            this.miFile.Text = "&File";
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(136, 22);
            this.miSave.Text = "&Save";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(136, 22);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miShow
            // 
            this.miShow.DropDown = this.showMenu;
            this.miShow.Name = "miShow";
            this.miShow.Size = new System.Drawing.Size(45, 20);
            this.miShow.Text = "&Show";
            // 
            // showMenu
            // 
            this.showMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miActivityDetails,
            this.miTimeLog,
            this.miSummary});
            this.showMenu.Name = "contextMenu";
            this.showMenu.OwnerItem = this.miShow;
            this.showMenu.Size = new System.Drawing.Size(175, 70);
            // 
            // miActivityDetails
            // 
            this.miActivityDetails.Checked = true;
            this.miActivityDetails.CheckOnClick = true;
            this.miActivityDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miActivityDetails.Name = "miActivityDetails";
            this.miActivityDetails.Size = new System.Drawing.Size(174, 22);
            this.miActivityDetails.Text = "Show &Activity Details";
            this.miActivityDetails.Click += new System.EventHandler(this.miActivityDetails_Click);
            // 
            // miTimeLog
            // 
            this.miTimeLog.Name = "miTimeLog";
            this.miTimeLog.Size = new System.Drawing.Size(174, 22);
            this.miTimeLog.Text = "Show &Time Log";
            this.miTimeLog.Click += new System.EventHandler(this.showTimeLog_Click);
            // 
            // miSummary
            // 
            this.miSummary.Name = "miSummary";
            this.miSummary.Size = new System.Drawing.Size(174, 22);
            this.miSummary.Text = "Show &Summary";
            this.miSummary.Click += new System.EventHandler(this.miShowSummary_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOnline,
            this.miAbout});
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(40, 20);
            this.miHelp.Text = "&Help";
            // 
            // miOnline
            // 
            this.miOnline.Name = "miOnline";
            this.miOnline.Size = new System.Drawing.Size(152, 22);
            this.miOnline.Text = "LazyCure &Online";
            this.miOnline.Click += new System.EventHandler(this.miOnline_Click);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(152, 22);
            this.miAbout.Text = "&About LazyCure";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusBar);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.nowYou);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(294, 58);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(294, 104);
            this.toolStripContainer1.TabIndex = 5;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menu);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "LazyCure";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // Main
            // 
            this.AcceptButton = this.switchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(294, 104);
            this.ContextMenuStrip = this.showMenu;
            this.Controls.Add(this.toolStripContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 107);
            this.Name = "Main";
            this.Text = "Main";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.nowYou.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.showMenu.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox currentActivity;
        private System.Windows.Forms.Button switchButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.GroupBox nowYou;
        private System.Windows.Forms.ToolStripMenuItem miShow;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel activityStartTime;
        private System.Windows.Forms.ToolStripStatusLabel activityDuration;
        private System.Windows.Forms.ToolStripStatusLabel currentTime;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStripMenuItem miOnline;
        private System.Windows.Forms.ContextMenuStrip showMenu;
        private System.Windows.Forms.ToolStripMenuItem miActivityDetails;
        private System.Windows.Forms.ToolStripMenuItem miTimeLog;
        private System.Windows.Forms.ToolStripMenuItem miSummary;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}