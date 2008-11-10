using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Core.Activities
{
    public interface IActivitiesHistory
    {
        string[] Activities{ get;}

        string[] LatestActivities { get;}

        int LatestSize { get; set;}

        int Size { get; set;}

        string UniqueName{ get;}

        void AddActivity(string finishedActivity);

        bool Load(string filename);

        bool Save(string filename);
    }
}
