using Examples;
using NUnit.Framework;

namespace NUnit.Tests
{
    [TestFixture]
    public class StripHelpersTest
    {
        [TestCase(null, null)]
        [TestCase("", null)]
        public void ShouldReturnNull(string given, string expected)
        {
            var result = Helpers.StripIllegalCharacters(given);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void ShouldStripOnlyIllegalCharacters()
        {
            var result = Helpers.StripIllegalCharacters("!@#$%^&*");
            Assert.AreEqual("", result);
        }

        [Test]
        public void ShouldExcludeIllegalCharacters()
        {
            var result = Helpers.StripIllegalCharacters("qwertyuioplkjhgfdsazxcvbnm!@#$%^&*");
            Assert.AreEqual("qwertyuioplkjhgfdsazxcvbnm", result);
        }
    }
}
