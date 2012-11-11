using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.Reports
{
    public interface ITasksSummary:IDataProvider
    {
        ITaskCollection TaskCollection { set; }
    }
}
