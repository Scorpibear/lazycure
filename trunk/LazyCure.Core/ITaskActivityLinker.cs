namespace LifeIdea.LazyCure.Core
{
    public interface ITaskActivityLinker
    {
        string GetRelatedTask(string activity);
        bool LinkActivityAndTask(string activity, string task);
    }
}
