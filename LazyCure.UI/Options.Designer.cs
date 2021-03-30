namespace LifeIdea.LazyCure.UI
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.saveAfterDone = new System.Windows.Forms.CheckBox();
            this.maxActivitiesInHistory = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reminderTime = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selectTimeLogsFolder = new System.Windows.Forms.Button();
            this.timeLogFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.languageGroupBox = new System.Windows.Forms.GroupBox();
            this.languageOptionBelarusian = new System.Windows.Forms.RadioButton();
            this.languageOptionSpanish = new System.Windows.Forms.RadioButton();
            this.languageOptionUkrainian = new System.Windows.Forms.RadioButton();
            this.languageOptionEnglish = new System.Windows.Forms.RadioButton();
            this.languageOptionRussian = new System.Windows.Forms.RadioButton();
            this.splitByComma = new System.Windows.Forms.CheckBox();
            this.switchTimeLogAtMidnight = new System.Windows.Forms.CheckBox();
            this.switchOnLogOff = new System.Windows.Forms.CheckBox();
            this.tabHotKeys = new System.Windows.Forms.TabPage();
            this.hotKeyToSwitchLabel = new System.Windows.Forms.Label();
            this.editSwitchKey = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.hotKeyToActivateLabel = new System.Windows.Forms.Label();
            this.editActivateKey = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabTray = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.opensMainWindow = new System.Windows.Forms.RadioButton();
            this.showsRecentActivities = new System.Windows.Forms.RadioButton();
            this.activitiesNumberInTray = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maxActivitiesInHistory)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.languageGroupBox.SuspendLayout();
            this.tabHotKeys.SuspendLayout();
            this.tabTray.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesNumberInTray)).BeginInit();
            this.SuspendLayout();
            // 
            // saveAfterDone
            // 
            resources.ApplyResources(this.saveAfterDone, "saveAfterDone");
            this.saveAfterDone.Name = "saveAfterDone";
            this.saveAfterDone.UseVisualStyleBackColor = true;
            // 
            // maxActivitiesInHistory
            // 
            resources.ApplyResources(this.maxActivitiesInHistory, "maxActivitiesInHistory");
            this.maxActivitiesInHistory.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.maxActivitiesInHistory.Name = "maxActivitiesInHistory";
            this.maxActivitiesInHistory.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // reminderTime
            // 
            resources.ApplyResources(this.reminderTime, "reminderTime");
            this.reminderTime.Name = "reminderTime";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // selectTimeLogsFolder
            // 
            resources.ApplyResources(this.selectTimeLogsFolder, "selectTimeLogsFolder");
            this.selectTimeLogsFolder.Name = "selectTimeLogsFolder";
            this.selectTimeLogsFolder.UseVisualStyleBackColor = true;
            this.selectTimeLogsFolder.Click += new System.EventHandler(this.selectTimeLogsFolder_Click);
            // 
            // timeLogFolder
            // 
            resources.ApplyResources(this.timeLogFolder, "timeLogFolder");
            this.timeLogFolder.Name = "timeLogFolder";
            this.timeLogFolder.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ok
            // 
            resources.ApplyResources(this.ok, "ok");
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Name = "ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            resources.ApplyResources(this.cancel, "cancel");
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Name = "cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabHotKeys);
            this.tabControl.Controls.Add(this.tabTray);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Controls.Add(this.languageGroupBox);
            this.tabGeneral.Controls.Add(this.splitByComma);
            this.tabGeneral.Controls.Add(this.switchTimeLogAtMidnight);
            this.tabGeneral.Controls.Add(this.switchOnLogOff);
            this.tabGeneral.Controls.Add(this.saveAfterDone);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.maxActivitiesInHistory);
            this.tabGeneral.Controls.Add(this.reminderTime);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.timeLogFolder);
            this.tabGeneral.Controls.Add(this.selectTimeLogsFolder);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // languageGroupBox
            // 
            resources.ApplyResources(this.languageGroupBox, "languageGroupBox");
            this.languageGroupBox.Controls.Add(this.languageOptionBelarusian);
            this.languageGroupBox.Controls.Add(this.languageOptionSpanish);
            this.languageGroupBox.Controls.Add(this.languageOptionUkrainian);
            this.languageGroupBox.Controls.Add(this.languageOptionEnglish);
            this.languageGroupBox.Controls.Add(this.languageOptionRussian);
            this.languageGroupBox.Name = "languageGroupBox";
            this.languageGroupBox.TabStop = false;
            // 
            // languageOptionBelarusian
            // 
            resources.ApplyResources(this.languageOptionBelarusian, "languageOptionBelarusian");
            this.languageOptionBelarusian.Name = "languageOptionBelarusian";
            this.languageOptionBelarusian.UseVisualStyleBackColor = true;
            // 
            // languageOptionSpanish
            // 
            resources.ApplyResources(this.languageOptionSpanish, "languageOptionSpanish");
            this.languageOptionSpanish.Name = "languageOptionSpanish";
            this.languageOptionSpanish.UseVisualStyleBackColor = true;
            // 
            // languageOptionUkrainian
            // 
            resources.ApplyResources(this.languageOptionUkrainian, "languageOptionUkrainian");
            this.languageOptionUkrainian.Name = "languageOptionUkrainian";
            this.languageOptionUkrainian.UseVisualStyleBackColor = true;
            // 
            // languageOptionEnglish
            // 
            resources.ApplyResources(this.languageOptionEnglish, "languageOptionEnglish");
            this.languageOptionEnglish.Checked = true;
            this.languageOptionEnglish.Name = "languageOptionEnglish";
            this.languageOptionEnglish.TabStop = true;
            this.languageOptionEnglish.UseVisualStyleBackColor = true;
            // 
            // languageOptionRussian
            // 
            resources.ApplyResources(this.languageOptionRussian, "languageOptionRussian");
            this.languageOptionRussian.Name = "languageOptionRussian";
            this.languageOptionRussian.UseVisualStyleBackColor = true;
            // 
            // splitByComma
            // 
            resources.ApplyResources(this.splitByComma, "splitByComma");
            this.splitByComma.Name = "splitByComma";
            this.splitByComma.UseVisualStyleBackColor = true;
            // 
            // switchTimeLogAtMidnight
            // 
            resources.ApplyResources(this.switchTimeLogAtMidnight, "switchTimeLogAtMidnight");
            this.switchTimeLogAtMidnight.Name = "switchTimeLogAtMidnight";
            this.switchTimeLogAtMidnight.UseVisualStyleBackColor = true;
            // 
            // switchOnLogOff
            // 
            resources.ApplyResources(this.switchOnLogOff, "switchOnLogOff");
            this.switchOnLogOff.Name = "switchOnLogOff";
            this.switchOnLogOff.UseVisualStyleBackColor = true;
            // 
            // tabHotKeys
            // 
            resources.ApplyResources(this.tabHotKeys, "tabHotKeys");
            this.tabHotKeys.Controls.Add(this.hotKeyToSwitchLabel);
            this.tabHotKeys.Controls.Add(this.editSwitchKey);
            this.tabHotKeys.Controls.Add(this.label7);
            this.tabHotKeys.Controls.Add(this.hotKeyToActivateLabel);
            this.tabHotKeys.Controls.Add(this.editActivateKey);
            this.tabHotKeys.Controls.Add(this.label6);
            this.tabHotKeys.Name = "tabHotKeys";
            this.tabHotKeys.UseVisualStyleBackColor = true;
            // 
            // hotKeyToSwitchLabel
            // 
            resources.ApplyResources(this.hotKeyToSwitchLabel, "hotKeyToSwitchLabel");
            this.hotKeyToSwitchLabel.Name = "hotKeyToSwitchLabel";
            // 
            // editSwitchKey
            // 
            resources.ApplyResources(this.editSwitchKey, "editSwitchKey");
            this.editSwitchKey.Name = "editSwitchKey";
            this.editSwitchKey.UseVisualStyleBackColor = true;
            this.editSwitchKey.Click += new System.EventHandler(this.editSwitchKey_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // hotKeyToActivateLabel
            // 
            resources.ApplyResources(this.hotKeyToActivateLabel, "hotKeyToActivateLabel");
            this.hotKeyToActivateLabel.Name = "hotKeyToActivateLabel";
            // 
            // editActivateKey
            // 
            resources.ApplyResources(this.editActivateKey, "editActivateKey");
            this.editActivateKey.Name = "editActivateKey";
            this.editActivateKey.UseVisualStyleBackColor = true;
            this.editActivateKey.Click += new System.EventHandler(this.editActivateKey_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tabTray
            // 
            resources.ApplyResources(this.tabTray, "tabTray");
            this.tabTray.Controls.Add(this.groupBox1);
            this.tabTray.Controls.Add(this.activitiesNumberInTray);
            this.tabTray.Controls.Add(this.label5);
            this.tabTray.Name = "tabTray";
            this.tabTray.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.opensMainWindow);
            this.groupBox1.Controls.Add(this.showsRecentActivities);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // opensMainWindow
            // 
            resources.ApplyResources(this.opensMainWindow, "opensMainWindow");
            this.opensMainWindow.Checked = true;
            this.opensMainWindow.Name = "opensMainWindow";
            this.opensMainWindow.TabStop = true;
            this.opensMainWindow.UseVisualStyleBackColor = true;
            // 
            // showsRecentActivities
            // 
            resources.ApplyResources(this.showsRecentActivities, "showsRecentActivities");
            this.showsRecentActivities.Name = "showsRecentActivities";
            this.showsRecentActivities.UseVisualStyleBackColor = true;
            // 
            // activitiesNumberInTray
            // 
            resources.ApplyResources(this.activitiesNumberInTray, "activitiesNumberInTray");
            this.activitiesNumberInTray.Name = "activitiesNumberInTray";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // Options
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Opacity = 0.9D;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.maxActivitiesInHistory)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.languageGroupBox.ResumeLayout(false);
            this.languageGroupBox.PerformLayout();
            this.tabHotKeys.ResumeLayout(false);
            this.tabHotKeys.PerformLayout();
            this.tabTray.ResumeLayout(false);
            this.tabTray.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesNumberInTray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox saveAfterDone;
        private System.Windows.Forms.NumericUpDown maxActivitiesInHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox timeLogFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectTimeLogsFolder;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.MaskedTextBox reminderTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.CheckBox switchOnLogOff;
        private System.Windows.Forms.CheckBox switchTimeLogAtMidnight;
        private System.Windows.Forms.CheckBox splitByComma;
        private System.Windows.Forms.TabPage tabHotKeys;
        private System.Windows.Forms.Label hotKeyToSwitchLabel;
        private System.Windows.Forms.Button editSwitchKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label hotKeyToActivateLabel;
        private System.Windows.Forms.Button editActivateKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox languageGroupBox;
        private System.Windows.Forms.RadioButton languageOptionEnglish;
        private System.Windows.Forms.RadioButton languageOptionRussian;
        private System.Windows.Forms.RadioButton languageOptionUkrainian;
        private System.Windows.Forms.TabPage tabTray;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton opensMainWindow;
        private System.Windows.Forms.RadioButton showsRecentActivities;
        private System.Windows.Forms.NumericUpDown activitiesNumberInTray;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton languageOptionBelarusian;
        private System.Windows.Forms.RadioButton languageOptionSpanish;

    }
}
