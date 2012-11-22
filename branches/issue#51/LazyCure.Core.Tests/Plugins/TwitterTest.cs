using NUnit.Framework;
using LifeIdea.LazyCure.Shared.Structures;

namespace LifeIdea.LazyCure.Core.Plugins
{
    [TestFixture]
    public class TwitterTest
    {
        Twitter twitter;
        [SetUp]
        public void SetUp()
        {
            twitter = new Twitter();
        }
        [Test]
        public void SetPinWithEmptyVerifierReturnsEmptyPair()
        {
            Assert.AreEqual(TokensPair.Empty, twitter.SetPin(string.Empty));
        }
        [Test]
        public void SetPinWithoutRequestReturnsEmptyPair()
        {
            Assert.AreEqual(TokensPair.Empty, twitter.SetPin("asdf"));
        }
        [Test]
        public void GetAuthorizationUrlIsInRightFormat()
        {
            // please, refer to https://dev.twitter.com/docs/api/1/get/oauth/authorize if it stops working
            Assert.AreEqual("https://api.twitter.com/oauth/authorize?oauth_token=MYTOKEN", Twitter.GetAuthorizationUrl("MYTOKEN").AbsoluteUri);
        }
    }
}
