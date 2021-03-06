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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.activityStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.activityDuration = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.nowYou = new System.Windows.Forms.GroupBox();
            this.postToTwitter = new System.Windows.Forms.CheckBox();
            this.currentActivity = new System.Windows.Forms.ComboBox();
            this.switchButton = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miShow = new System.Windows.Forms.ToolStripMenuItem();
            this.showMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miActivityDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.miTimeLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.miTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.miReport = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpentOnActivityInDifferentDays = new System.Windows.Forms.ToolStripMenuItem();
            this.miOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miHowToUse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.miSubscribe = new System.Windows.Forms.ToolStripMenuItem();
            this.miSubmit = new System.Windows.Forms.ToolStripMenuItem();
            this.miDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.miContextShow = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miContextActivate = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miContextExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.nowYou.SuspendLayout();
            this.menu.SuspendLayout();
            this.showMenu.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
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
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menu);
            // 
            // statusBar
            // 
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activityStartTime,
            this.activityDuration,
            this.currentTime});
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.SizingGrip = false;
            // 
            // activityStartTime
            // 
            this.activityStartTime.Name = "activityStartTime";
            resources.ApplyResources(this.activityStartTime, "activityStartTime");
            this.activityStartTime.Spring = true;
            // 
            // activityDuration
            // 
            this.activityDuration.Name = "activityDuration";
            resources.ApplyResources(this.activityDuration, "activityDuration");
            this.activityDuration.Spring = true;
            // 
            // currentTime
            // 
            this.currentTime.Name = "currentTime";
            resources.ApplyResources(this.currentTime, "currentTime");
            this.currentTime.Spring = true;
            // 
            // nowYou
            // 
            this.nowYou.Controls.Add(this.postToTwitter);
            this.nowYou.Controls.Add(this.currentActivity);
            this.nowYou.Controls.Add(this.switchButton);
            resources.ApplyResources(this.nowYou, "nowYou");
            this.nowYou.Name = "nowYou";
            this.nowYou.TabStop = false;
            // 
            // postToTwitter
            // 
            resources.ApplyResources(this.postToTwitter, "postToTwitter");
            this.postToTwitter.Name = "postToTwitter";
            this.postToTwitter.UseVisualStyleBackColor = true;
            // 
            // currentActivity
            // 
            this.currentActivity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.currentActivity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.currentActivity.BackColor = System.Drawing.SystemColors.Window;
            this.currentActivity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.currentActivity.FormattingEnabled = true;
            resources.ApplyResources(this.currentActivity, "currentActivity");
            this.currentActivity.Name = "currentActivity";
            this.toolTip.SetToolTip(this.currentActivity, resources.GetString("currentActivity.ToolTip"));
            this.currentActivity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.currentActivity_KeyDown);
            // 
            // switchButton
            // 
            resources.ApplyResources(this.switchButton, "switchButton");
            this.switchButton.Name = "switchButton";
            this.toolTip.SetToolTip(this.switchButton, resources.GetString("switchButton.ToolTip"));
            this.switchButton.UseVisualStyleBackColor = true;
            this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
            // 
            // menu
            // 
            resources.ApplyResources(this.menu, "menu");
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miShow,
            this.miOptions,
            this.miHelp});
            this.menu.Name = "menu";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miSave,
            this.miSaveAs,
            this.toolStripSeparator1,
            this.miExit});
            this.miFile.Name = "miFile";
            resources.ApplyResources(this.miFile, "miFile");
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            resources.ApplyResources(this.miOpen, "miOpen");
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            resources.ApplyResources(this.miSave, "miSave");
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Name = "miSaveAs";
            resources.ApplyResources(this.miSaveAs, "miSaveAs");
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            resources.ApplyResources(this.miExit, "miExit");
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miShow
            // 
            this.miShow.DropDown = this.showMenu;
            this.miShow.Name = "miShow";
            resources.ApplyResources(this.miShow, "miShow");
            // 
            // showMenu
            // 
            this.showMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.showMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miActivityDetails,
            this.miTimeLog,
            this.miSummary,
            this.miTasks,
            this.miReport});
            this.showMenu.Name = "contextMenu";
            this.showMenu.OwnerItem = this.miContextShow;
            resources.ApplyResources(this.showMenu, "showMenu");
            // 
            // miActivityDetails
            // 
            this.miActivityDetails.Checked = true;
            this.miActivityDetails.CheckOnClick = true;
            this.miActivityDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miActivityDetails.Name = "miActivityDetails";
            resources.ApplyResources(this.miActivityDetails, "miActivityDetails");
            this.miActivityDetails.Click += new System.EventHandler(this.miActivityDetails_Click);
            // 
            // miTimeLog
            // 
            this.miTimeLog.CheckOnClick = true;
            this.miTimeLog.Name = "miTimeLog";
            resources.ApplyResources(this.miTimeLog, "miTimeLog");
            this.miTimeLog.Click += new System.EventHandler(this.miTimeLog_Click);
            // 
            // miSummary
            // 
            this.miSummary.CheckOnClick = true;
            this.miSummary.Name = "miSummary";
            resources.ApplyResources(this.miSummary, "miSummary");
            this.miSummary.Click += new System.EventHandler(this.miSummary_Click);
            // 
            // miTasks
            // 
            this.miTasks.CheckOnClick = true;
            this.miTasks.Name = "miTasks";
            resources.ApplyResources(this.miTasks, "miTasks");
            this.miTasks.Click += new System.EventHandler(this.miTasks_Click);
            // 
            // miReport
            // 
            this.miReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSpentOnActivityInDifferentDays});
            this.miReport.Name = "miReport";
            resources.ApplyResources(this.miReport, "miReport");
            // 
            // miSpentOnActivityInDifferentDays
            // 
            this.miSpentOnActivityInDifferentDays.CheckOnClick = true;
            this.miSpentOnActivityInDifferentDays.Name = "miSpentOnActivityInDifferentDays";
            resources.ApplyResources(this.miSpentOnActivityInDifferentDays, "miSpentOnActivityInDifferentDays");
            this.miSpentOnActivityInDifferentDays.Click += new System.EventHandler(this.miSpentOnActivityInDifferentDays_Click);
            // 
            // miOptions
            // 
            this.miOptions.Name = "miOptions";
            resources.ApplyResources(this.miOptions, "miOptions");
            this.miOptions.Click += new System.EventHandler(this.miOptions_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHowToUse,
            this.toolStripSeparator3,
            this.miOnline,
            this.miSubscribe,
            this.miSubmit,
            this.miDonate,
            this.toolStripSeparator2,
            this.miAbout});
            this.miHelp.Name = "miHelp";
            resources.ApplyResources(this.miHelp, "miHelp");
            // 
            // miHowToUse
            // 
            this.miHowToUse.Name = "miHowToUse";
            resources.ApplyResources(this.miHowToUse, "miHowToUse");
            this.miHowToUse.Tag = "";
            this.miHowToUse.Click += new System.EventHandler(this.miHowToUse_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // miOnline
            // 
            this.miOnline.Name = "miOnline";
            resources.ApplyResources(this.miOnline, "miOnline");
            this.miOnline.Tag = "http://lazycure.com/";
            this.miOnline.Click += new System.EventHandler(this.Link_Click);
            // 
            // miSubscribe
            // 
            this.miSubscribe.Name = "miSubscribe";
            resources.ApplyResources(this.miSubscribe, "miSubscribe");
            this.miSubscribe.Tag = "http://lazycure.com/feed/";
            this.miSubscribe.Click += new System.EventHandler(this.Link_Click);
            // 
            // miSubmit
            // 
            this.miSubmit.Name = "miSubmit";
            resources.ApplyResources(this.miSubmit, "miSubmit");
            this.miSubmit.Tag = "http://lazycure.com/submit/";
            this.miSubmit.Click += new System.EventHandler(this.Link_Click);
            // 
            // miDonate
            // 
            this.miDonate.Name = "miDonate";
            resources.ApplyResources(this.miDonate, "miDonate");
            this.miDonate.Click += new System.EventHandler(this.miDonate_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            resources.ApplyResources(this.miAbout, "miAbout");
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miContextShow
            // 
            this.miContextShow.DropDown = this.showMenu;
            this.miContextShow.Name = "miContextShow";
            resources.ApplyResources(this.miContextShow, "miContextShow");
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miContextActivate,
            this.miContextShow,
            this.miSeparator,
            this.miContextExit});
            this.contextMenu.Name = "contextMenu";
            resources.ApplyResources(this.contextMenu, "contextMenu");
            // 
            // miContextActivate
            // 
            this.miContextActivate.Name = "miContextActivate";
            resources.ApplyResources(this.miContextActivate, "miContextActivate");
            this.miContextActivate.Click += new System.EventHandler(this.miContextActivate_Click);
            // 
            // miSeparator
            // 
            this.miSeparator.Name = "miSeparator";
            resources.ApplyResources(this.miSeparator, "miSeparator");
            // 
            // miContextExit
            // 
            this.miContextExit.Name = "miContextExit";
            resources.ApplyResources(this.miContextExit, "miContextExit");
            this.miContextExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
            // 
            // Main
            // 
            this.AcceptButton = this.switchButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.toolStripContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.nowYou.ResumeLayout(false);
            this.nowYou.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.showMenu.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miContextShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miOptions;
        private System.Windows.Forms.ToolStripMenuItem miSubscribe;
        private System.Windows.Forms.ToolStripMenuItem miSubmit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miTasks;
        private System.Windows.Forms.ToolStripMenuItem miContextActivate;
        private System.Windows.Forms.ToolStripSeparator miSeparator;
        private System.Windows.Forms.ToolStripMenuItem miContextExit;
        private System.Windows.Forms.ToolStripMenuItem miHowToUse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox postToTwitter;
        private System.Windows.Forms.ToolStripMenuItem miDonate;
        private System.Windows.Forms.ToolStripMenuItem miReport;
        private System.Windows.Forms.ToolStripMenuItem miSpentOnActivityInDifferentDays;
    }
}
