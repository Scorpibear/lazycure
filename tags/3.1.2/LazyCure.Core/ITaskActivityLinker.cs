namespace LifeIdea.LazyCure.Core
{
    public interface ITaskActivityLinker
    {
        string GetRelatedTask(string activity);
        void LinkActivityAndTask(string activity, string task);
    }
}
