using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public interface ITasksSummary:IDataProvider
    {
        ITaskCollection TaskCollection { set; }
    }
}
