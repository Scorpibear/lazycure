using NUnit.Framework;

namespace LifeIdea.LazyCure.Shared.Structures
{
    [TestFixture]
    public class TokenPairTest
    {
        [Test]
        public void EmptyFillNulls()
        {
            TokensPair pair = TokensPair.Empty;
            Assert.IsNull(pair.Token);
            Assert.IsNull(pair.TokenSecret);
        }
        [Test]
        public void EmptyTokenIsInvalid()
        {
            TokensPair pair = new TokensPair("","something");
            Assert.False(pair.IsValid);
        }
    }
}
