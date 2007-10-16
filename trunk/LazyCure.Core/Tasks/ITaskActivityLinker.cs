namespace LifeIdea.LazyCure.Core.Tasks
{
    public interface ITaskActivityLinker
    {
        string GetRelatedTask(string activity);
        bool LinkActivityAndTask(string activity, string task);
    }
}