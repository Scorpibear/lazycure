namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface ITaskActivityLinker
    {
        string GetRelatedTaskName(string activity);

        bool LinkActivityAndTask(string activity, string task);
    }
}