using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Twitterizer;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Structures;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Plugins
{
    public class Twitter:IExternalPoster
    {
        readonly string consumerKey = "sMPQT2knx5ddauFYovHQNA";
        readonly string consumerSecret = "Q9GGMWW5KJmQEjLTdQ0O97EOZ30YgwdsjItroHucg";
        // please, refer to https://dev.twitter.com/docs/auth/oauth for workflow
        OAuthTokenResponse requestTokenResponse, accessTokenResponse;
        private OAuthTokens tokens = new OAuthTokens();

        public Twitter()
        {
            tokens.ConsumerKey = consumerKey;
            tokens.ConsumerSecret = consumerSecret;
        }

        private void GetRequestToken()
        {
            this.requestTokenResponse = OAuthUtility.GetRequestToken(consumerKey, consumerSecret, "oob");
        }

        private string text;

        #region IExternalPoster Members

        public TokensPair AccessTokens
        {
            set
            {
                if (value.IsValid)
                {
                    tokens.AccessToken = value.Token;
                    tokens.AccessTokenSecret = value.TokenSecret;
                }
            }
        }

        public void ShowAuthorizationPage()
        {
            GetRequestToken();
            Uri authorizationUri = GetAuthorizationUrl();
            if (authorizationUri != null)
                System.Diagnostics.Process.Start(authorizationUri.ToString());
        }

        /// <summary>
        /// Set pin and returns access tokens
        /// </summary>
        /// <param name="verifier">pin as a verifier</param>
        /// <returns>access TokensPair</returns>
        public TokensPair SetPin(string verifier)
        {
            if (string.IsNullOrWhiteSpace(verifier))
                return TokensPair.Empty;
            if (requestTokenResponse == null)
                return TokensPair.Empty;
            if (string.IsNullOrWhiteSpace(requestTokenResponse.Token))
                return TokensPair.Empty;
            try
            {
                accessTokenResponse = null;
                accessTokenResponse = OAuthUtility.GetAccessToken(consumerKey, consumerSecret, requestTokenResponse.Token, verifier);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            if (accessTokenResponse == null)
                return TokensPair.Empty;
            var accessTokensPair = new TokensPair(accessTokenResponse.Token, accessTokenResponse.TokenSecret);
            AccessTokens = accessTokensPair;
            return accessTokensPair;
        }

        private Uri GetAuthorizationUrl()
        {
            return (requestTokenResponse != null) ? GetAuthorizationUrl(requestTokenResponse.Token) : null;
        }

        public static Uri GetAuthorizationUrl(string requestToken)
        {
            // please, refer to https://dev.twitter.com/docs/api/1/get/oauth/authorize if it stops working
            StringBuilder parameters = new StringBuilder("https://api.twitter.com/oauth/");
            parameters.Append("authorize");
            parameters.AppendFormat("?oauth_token={0}", requestToken);
            return new Uri(parameters.ToString());
        }

        public void Post()
        {
            try
            {
                TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, text);
                if (tweetResponse.Result == RequestResult.Success)
                {
                    // Tweet posted successfully!
                }
                else
                {
                    Log.Error("Posting to twitter was unsuccessful. Error message: " + tweetResponse.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void PostAsync(string text)
        {
            Thread thread = new Thread(new ThreadStart(Post));
            this.text = text;
            thread.Start();
        }

        #endregion

    }
}
