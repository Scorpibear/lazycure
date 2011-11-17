using System.Data;

namespace LifeIdea.LazyCure.Core.Reports
{
    public interface IDataProvider
    {
        DataTable Data{ get;}
    }
}
