using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Core.Plugins
{
    public interface IExternalPoster
    {
        string Username { set;}
        string Password { set;}
        void PostAsync(string text);
    }
}
