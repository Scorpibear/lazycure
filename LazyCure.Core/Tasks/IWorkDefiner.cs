namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Define is activity is working
    /// </summary>
    public interface IWorkDefiner
    {
        bool IsWorkingActivity(string activity);
    }
}
