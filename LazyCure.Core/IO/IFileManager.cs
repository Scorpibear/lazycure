using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.IO
{
    public interface IFileManager
    {
        bool SaveTasks(ITaskCollection taskCollection);
    }
}
