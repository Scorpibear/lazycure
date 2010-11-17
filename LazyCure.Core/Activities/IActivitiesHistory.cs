using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Activities
{
    public interface IActivitiesHistory
    {
        string[] Activities{ get;}

        string[] LatestActivities { get;}

        int LatestSize { get; set;}

        int Size { get; set;}

        string UniqueName{ get;}

        void AddActivity(string activity);

        void AddActivities(List<IActivity> activities);

        bool Load(string filename);

        void RenameActivity(string before, string after);

        bool Save(string filename);
    }
}
