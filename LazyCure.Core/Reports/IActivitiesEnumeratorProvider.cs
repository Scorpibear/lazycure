using System.Collections.Generic;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public interface IActivitiesEnumeratorProvider
    {
        List<IActivity>.Enumerator GetActivityEnumerator();
    }
}
