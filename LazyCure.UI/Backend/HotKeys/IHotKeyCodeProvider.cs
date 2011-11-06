using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.UI.Backend.HotKeys
{
    public interface IHotKeyCodeProvider
    {
        int Code { get;}
        int ModifiersCode { get;}
    }
}
