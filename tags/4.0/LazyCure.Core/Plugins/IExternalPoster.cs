using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Shared.Structures;

namespace LifeIdea.LazyCure.Core.Plugins
{
    public interface IExternalPoster
    {
        TokensPair AccessTokens { set; }

        void PostAsync(string text);

        TokensPair SetPin(string pin);

        void ShowAuthorizationPage();
    }
}
