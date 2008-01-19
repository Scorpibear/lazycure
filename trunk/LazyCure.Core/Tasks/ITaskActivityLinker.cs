namespace LifeIdea.LazyCure.Core.Tasks
{
    public interface ITaskActivityLinker
    {
        string GetRelatedTaskName(string activity);

        bool LinkActivityAndTask(string activity, string task);

        ITaskCollection TaskCollection { set; }
    }
}