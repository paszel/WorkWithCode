using Examples.Obfuscating;
using Examples.Randomizer;
using Moq;
using NUnit.Framework;

namespace NUnit
{
    [TestFixture]
    public class TextObfuscatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldSwapLowerAndUpperCaseRandomly()
        {
            var moq = new Mock<IRandomizer>();
            moq.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(true);

            var obfuscator = new TextObfuscator(moq.Object);

            var result = obfuscator.CharactersTransformation("abcdef");
            Assert.AreEqual("AbCdeF", result);
        }
    }
}