using Examples.Obfuscating;
using Examples.Randomizer;
using Moq;
using NUnit.Framework;

namespace NUnit
{
    [TestFixture]
    public class TextObfuscatorTest
    {
        private Mock<IRandomizer> _mockRandomizer;
        [OneTimeSetUp]
        public void Setup()
        {
            _mockRandomizer = new Mock<IRandomizer>();
        }

        [Test]
        public void ShouldSwapLowerAndUpperCaseRandomly()
        {
            _mockRandomizer.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(true);

            var obfuscator = new TextObfuscator(_mockRandomizer.Object);

            var result = obfuscator.CharactersTransformation("abcdef");
            Assert.AreEqual("AbCdeF", result);
        }

        [Test]
        public void ShouldInsertSpecialCharactersRandomly()
        {
            _mockRandomizer.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true);

            _mockRandomizer.SetupSequence(m => m.RandomInteger(It.IsAny<int>()))
                .Returns(0)
                .Returns(2)
                .Returns(1)
                .Returns(2);

            var obfuscator = new TextObfuscator(_mockRandomizer.Object);

            var result = obfuscator.InsertSpecialCharacters("Ala ma kota a kot ma ale");
            Assert.AreEqual("Ala.ma kota!a kot,ma ale", result);
        }
    }
}