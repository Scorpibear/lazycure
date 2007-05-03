using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;
using System.Diagnostics;

namespace LifeIdea.LazyCure.UI
{
    public partial class Main : Form
    {
        ILazyCureDriver lazyCure;
        ITimeLogView timeLogView;
        ISummaryView summaryView;
        AboutBox aboutBox;
        public Main(ILazyCureDriver driver)
        {
            InitializeComponent();
            this.lazyCure = driver;
            timer.Start();
            this.currentActivity.Text = lazyCure.CurrentActivity.Name;
            UpdateActivityStartTime();
            timeLogView = new TimeLog(lazyCure);
            summaryView = new Summary();
            aboutBox = new AboutBox();
        }

        private void UpdateActivityStartTime()
        {
            this.activityStartTime.Text = Format.Time(lazyCure.CurrentActivity.StartTime);
        }

        private void showTimeLog_Click(object sender, EventArgs e)
        {
            timeLogView.Show();
        }

        private void switchButton_Click(object sender, EventArgs e)
        {
            string nextActivity = "(specify what you are doing)";
            lazyCure.FinishActivity(this.currentActivity.Text,nextActivity);
            this.currentActivity.Text = nextActivity;
            UpdateActivityStartTime();
            timeLogView.UpdateData();
            currentActivity.SelectAll();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.activityDuration.Text = Format.Duration(lazyCure.CurrentActivity.Duration);
            this.currentTime.Text = Format.Time(DateTime.Now);
        }

        private void activityStartTime_TextChanged(object sender, EventArgs e)
        {
            lazyCure.CurrentActivity.StartTime = DateTime.Parse(this.activityStartTime.Text);
        }

        private void miShowSummary_Click(object sender, EventArgs e)
        {
            summaryView.Show();
        }

        private void miActivityDetails_Click(object sender, EventArgs e)
        {
            statusBar.Visible = miActivityDetails.Checked;
            if (statusBar.Visible)
                this.Size = new Size(300, 128);
            else
                this.Size = new Size(300, 128-statusBar.Height);
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            this.currentActivity.Focus();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            aboutBox.Show();
        }

        private void miOnline_Click(object sender, EventArgs e)
        {
            Process.Start("http://lifeidea.org/lazycure/");
        }
    }
}