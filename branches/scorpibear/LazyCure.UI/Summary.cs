using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    partial class Summary : View,ISummaryView
    {
        private ILazyCureDriver lazyCure;
        public Summary(ILazyCureDriver lazyCure)
        {
            this.lazyCure = lazyCure;
            InitializeComponent();
            this.activitiesSummary.DataSource = lazyCure.ActivitiesSummaryData;
        }
    }
}