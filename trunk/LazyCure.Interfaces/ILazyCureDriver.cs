using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ILazyCureDriver
    {
        IActivity SwitchTo(string nextTaskName);
        IActivity CurrentActivity { get;}
        IActivity PreviousActivity { get;}
        void FinishActivity(string finishedActivity, string nextActivity);
    }
}
