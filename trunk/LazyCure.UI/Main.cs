using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;
using LifeIdea.LazyCure.UI.Properties;
using System.Collections.Generic;
using Microsoft.Win32;

namespace LifeIdea.LazyCure.UI
{
    using Backend;
    /// <summary>
    /// Represent classic main window GUI
    /// </summary>
    public partial class Main : MainBase, IMainForm, IDisposable
    {
        private const string DefaultActivity = "(specify what you are doing)";
        private readonly ILazyCureDriver lazyCure;
        private readonly string nextActivity = DefaultActivity;
        private HotKeyManager hotKeyManager = new HotKeyManager();
        private ToolStripSeparator topSeparatorForActivities = new ToolStripSeparator();
        private Dictionary<string, ToolStripMenuItem> activitiesMenuItems = new Dictionary<string, ToolStripMenuItem>();
        private Size expandedSize;
        private Size collapsedSize;
        private Timer leftClickTimer;
        
        public bool PostToTwitterEnabled { set { postToTwitter.Visible = value; } }

        public Main(ILazyCureDriver driver, ISettings settings)
        {
            InitializeComponent();
            this.lazyCure = driver;
            Dialogs.MainForm = this;
            Dialogs.LazyCureDriver = driver;
            Dialogs.Settings = settings;
            timer.Start();
            SetCaption();
            UpdateCurrentActivity();
            UpdateContextMenuActivities();
            UpdateActivityStartTime();
            expandedSize = Size;
            collapsedSize = new Size(Size.Width, Size.Height - statusBar.Height);
            SetLocation(settings.MainWindowLocation);
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            postToTwitter.Visible = Dialogs.Settings.TwitterEnabled;
            leftClickTimer = new Timer();
            leftClickTimer.Tick += new EventHandler(notifyIcon_LeftClick);
            leftClickTimer.Interval = 300; // should be changed on max double click interval
            RegisterHotKey();
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if(Dialogs.Settings.SwitchOnLogOff && 
                (e.Reason == SessionSwitchReason.SessionLock))
                SwitchActivity();
        }

        public void RegisterHotKey()
        {
            hotKeyManager.Unregister(this);
            hotKeyManager.Register(this, HotKey.Parse(Dialogs.Settings.HotKeyToActivate));
        }

        public void ViewsVisibilityChanged()
        {
            miTimeLog.Checked = Dialogs.TimeLog.Visible;
            miSummary.Checked = Dialogs.Summary.Visible;
            miTasks.Checked = Dialogs.TaskManager.Visible;
        }

        #region Private Methods

        private string GetFinishedActivity()
        {
            string finishedActivity =
                (this.currentActivity.Text == DefaultActivity) ?
                lazyCure.GetUniqueActivityName() : this.currentActivity.Text;
            return finishedActivity;
        }

        private void PostToExternals(string activity)
        {
            if (postToTwitter.Visible && postToTwitter.Checked)
            {
                lazyCure.PostToTwitter(activity);
                postToTwitter.Checked = false;
            }
        }

        private void SaveWithNotification(FormClosingEventArgs e)
        {
            if (!lazyCure.Save())
            {
                DialogResult result = MessageBox.Show(this, "Time Log could not be saved. Exit from LazyCure anyway?", "Could not save time log", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void SwitchActivity()
        {
            string finishedActivity = GetFinishedActivity();
            SwitchActivity(finishedActivity);
            PostToExternals(finishedActivity);
        }

        private void SwitchActivity(string finishedActivity)
        {
            Dialogs.CancelEditTimeLog();
            lazyCure.FinishActivity(finishedActivity, nextActivity);
            UpdateCurrentActivity();
            UpdateContextMenuActivities();
            UpdateActivityStartTime();
            currentActivity.SelectAll();
            SetCaption();
        }

        private void SetCaption()
        {
            string[] versionNumbers = Application.ProductVersion.Split('.');
            this.Text = String.Format("{0} - {1} {2}.{3}", lazyCure.TimeLogDate, Application.ProductName,
                versionNumbers[0], versionNumbers[1]);
        }

        private void UpdateActivityStartTime()
        {
            this.activityStartTime.Text = Format.Time(lazyCure.CurrentActivity.Start);
        }

        private void UpdateContextMenuActivities()
        {
            foreach (ToolStripMenuItem item in activitiesMenuItems.Values)
                contextMenu.Items.Remove(item);
            if (lazyCure.LatestActivities.Length > 0)
            {
                foreach (string activity in lazyCure.LatestActivities)
                {
                    ToolStripMenuItem menuItem;
                    if (activitiesMenuItems.ContainsKey(activity))
                        menuItem = activitiesMenuItems[activity];
                    else
                    {
                        menuItem = new ToolStripMenuItem(activity);
                        menuItem.Click += ActivityMenuItem_Click;
                        activitiesMenuItems.Add(activity, menuItem);
                    }
                    contextMenu.Items.Insert(2, menuItem);
                }
                contextMenu.Items.Insert(2, topSeparatorForActivities);
            }
        }

        private void UpdateCurrentActivity()
        {
            currentActivity.Text = nextActivity;
            currentActivity.Items.Clear();
            currentActivity.Items.AddRange(lazyCure.HistoryActivities);
        }

        private void UpdateTime()
        {
            this.activityDuration.Text = Format.Duration(lazyCure.CurrentActivity.Duration);
            this.currentTime.Text = Format.Time(DateTime.Now);
        }

        private void UpdateTrayIcon()
        {
            if ((notifyIcon.Tag as string) == "Empty")
            {
                notifyIcon.Tag = "LazyCure";
                notifyIcon.Icon = Resources.LazyCure;
            }
            else
            {
                if (lazyCure.TimeToUpdateTimeLog)
                {
                    notifyIcon.Tag = "Empty";
                    notifyIcon.Icon = Resources.Empty;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
                Display();
            base.WndProc(ref m);
        }

        #endregion Private Methods

        #region Event Handlers

        private void ActivityMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                SwitchActivity(menuItem.Text);
            }
        }

        private void currentActivity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                WindowState = FormWindowState.Minimized;
        }

        private void Link_Click(object sender, EventArgs e)
        {
            string link = (string)((ToolStripItem)sender).Tag;
            Process.Start(link);
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            this.currentActivity.Focus();
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    WindowState = FormWindowState.Minimized;
                    e.Cancel=true;
                    break;
                case CloseReason.WindowsShutDown:
                    DialogResult result = MessageBox.Show(this,
                    "Do you want to log current activity before LazyCure will be closed?",
                    "LazyCure is shutting down", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        this.Hide();
                        this.ShowDialog();
                        lazyCure.Save();
                    }
                    else
                        SaveWithNotification(e);
                    break;
                default:
                    SaveWithNotification(e);
                    break;
            }
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            try
            {
                Dialogs.About.ShowDialog(this);
            }
            catch(Exception ex)
            {
                Log.Exception(ex);
            }
        }

        private void miActivityDetails_Click(object sender, EventArgs e)
        {
            statusBar.Visible = miActivityDetails.Checked;
            if (statusBar.Visible)
                this.Size = expandedSize;
            else
                this.Size = collapsedSize;
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = Dialogs.Open;
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                lazyCure.LoadTimeLog(openDialog.FileName);
                Dialogs.TimeLog.Update();
                SetCaption();
            }
        }

        private void miOptions_Click(object sender, EventArgs e)
        {
            Dialogs.Options.Show();
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            if (!lazyCure.Save())
                MessageBox.Show("Time log has not been saved!", "Saving  error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = Dialogs.Save;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                lazyCure.SaveTimeLog(saveDialog.FileName);
            }
        }

        private void miSummary_Click(object sender, EventArgs e)
        {
            Dialogs.Summary.Visible = miSummary.Checked;
        }

        private void miTasks_Click(object sender, EventArgs e)
        {
            Dialogs.TaskManager.Visible = miTasks.Checked;

        }

        private void miTimeLog_Click(object sender, EventArgs e)
        {
            try
            {
                Dialogs.TimeLog.Visible = miTimeLog.Checked;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        private void switchButton_Click(object sender, EventArgs e)
        {
            SwitchActivity();
            currentActivity.Focus();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
            UpdateTrayIcon();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
            hotKeyManager.Unregister(this);
            Display();
            Dialogs.Settings.MainWindowLocation = Location;
            Dialogs.Settings.Save();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftClickTimer.Start();
        }

        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            string activityName = (currentActivity.Text == DefaultActivity) ?
                "current activity is lasting" : currentActivity.Text;
            notifyIcon.Text = String.Format("{0} from {1} for {2}", activityName,
                Format.Time(lazyCure.CurrentActivity.Start),
                Format.Duration(lazyCure.CurrentActivity.Duration));
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            leftClickTimer.Stop();
            Display();
        }

        void notifyIcon_LeftClick(object sender, EventArgs e)
        {
            leftClickTimer.Stop();
            if (Dialogs.Settings.LeftClickOnTray == LeftClickOnTray.ShowsMainWindow)
                Display();
            else
                notifyIcon.ContextMenuStrip.Show(Cursor.Position);
        }

        private void miContextActivate_Click(object sender, EventArgs e)
        {
            Display();
        }

        #endregion Event Handlers
    }
}
