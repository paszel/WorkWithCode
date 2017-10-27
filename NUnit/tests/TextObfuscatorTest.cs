using Examples.Obfuscating;
using Examples.Randomizer;
using Moq;
using NUnit.Framework;

namespace NUnit.Tests
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


        [TestCase("", "")]
        [TestCase("Ala ma kota", "Alamakota")]
        public void ShouldRemoveWhiteSpaces(string given, string expected)
        {
            var obfuscator = new Obfuscations(_mockRandomizer.Object);

            var result = obfuscator.RemoveWhiteSpaces(given);
            Assert.AreEqual(expected, result);
        }

        [TestCase("", "")]
        [TestCase("Ala", "alA")]
        [TestCase("abcdef", "AbCdeF")]
        [TestCase("Ala ma kota", "alA mA kOta")]
        [TestCase("Zażółć gęślą jaźń", "zaŻółĆ gĘślĄ jAźŃ")]
        public void ShouldSwapLowerAndUpperCaseRandomly(string given, string expected)
        {
            _mockRandomizer.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            var obfuscator = new Obfuscations(_mockRandomizer.Object);

            var result = obfuscator.CharactersTransformation(given);
            Assert.AreEqual(expected, result);
        }

        [TestCase("", "")]
        [TestCase("Ala", "Ala")]
        [TestCase("Ala ma", "Ala.ma")]
        [TestCase("Ala ma kota", "Ala.ma kota")]
        [TestCase("Ala ma kota a kot ma ale", "Ala.ma kota!a kot,ma ale")]
        public void ShouldInsertSpecialCharactersRandomly(string given, string expected)
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

            var obfuscator = new Obfuscations(_mockRandomizer.Object);

            var result = obfuscator.InsertSpecialCharacters(given);
            Assert.AreEqual(expected, result);
        }

        [TestCase("obfuscator", "ObFuScaToR")]
        [TestCase("OBFUSCATOR", "oBfUsCAtOr")]
        [TestCase("oBfUsCAtOr", "OBFUSCATOR")]
        public void ShouldTransformCharactersRandomly(string given, string expected)
        {
            _mockRandomizer.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true);

            var obfuscator = new Obfuscations(_mockRandomizer.Object);
            var result = obfuscator.CharactersTransformation(given);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Zażółć gęślą jaźń", "Zazolc gesla jazn")]
        [TestCase("Taki wręcz nadmiar polskich znaków mamy też w tytułowym zdaniu zażółć gęślą jaźń.", "Taki wrecz nadmiar polskich znakow mamy tez w tytulowym zdaniu zazolc gesla jazn.")]
        public void ShouldReplacePolishSigns(string given, string expected)
        {
            var obfuscator = new Obfuscations(_mockRandomizer.Object);
            var result = obfuscator.ReplacePolishSigns(given);

            Assert.AreEqual(expected, result);
        }

        [TestCase("?!-.,:;'()", "")]
        [TestCase("Ala. ma. kota? !-+-", "Ala ma kota ")]
        [TestCase("A cóż to za cholerstwo?", "A cóż to za cholerstwo")]
        public void ShouldRemovePunctuationMarks(string given, string expected)
        {
            var obfuscator = new Obfuscations(_mockRandomizer.Object);
            var result = obfuscator.RemovePunctuationMarks(given);

            Assert.AreEqual(expected, result);
        }

        [TestCase("", "")]
        [TestCase(" ", " i  ")]
        [TestCase("Ala", " i Ala")]
        [TestCase("Zażółć gęślą jaźń", " i Zażółć aczkolwiek   a  poniekąd gęślą aczkolwiek   a  aczkolwiek jaźń")]
        [TestCase("Ala ma kota", " i Ala aczkolwiek   a  poniekąd ma aczkolwiek   a  a  poniekąd kota")]
        public void ShouldInsertObfuscatorsRandomly(string given, string expected)
        {
            _mockRandomizer.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true);

            _mockRandomizer.SetupSequence(m => m.RandomInteger(It.IsAny<int>()))
              .Returns(0)
              .Returns(2)
              .Returns(1)
              .Returns(3)
              .Returns(2)
              .Returns(1);

            var obfuscator = new Obfuscations(_mockRandomizer.Object);

            var textParts = TextPart.From(given);
            var result = TextPart.ToString(obfuscator.InsertObfuscators(textParts));

            Assert.AreEqual(expected, result);
        }

        [TestCase("", "")]
        [TestCase("Ala", "Ala")]
        [TestCase("Ala ma", "ma Ala")]
        [TestCase("Ala ma kota", "ma Ala kota")]
        [TestCase("Ala ma kota a kot ma ale", "ma Ala a kota ma ale kot")]
        public void ShouldSwapWordsRandomy(string given, string expected)
        {
            _mockRandomizer.SetupSequence(m => m.ShouldBeDoneWith(It.IsAny<double>()))
                   .Returns(true)
                   .Returns(false)
                   .Returns(true)
                   .Returns(false)
                   .Returns(true)
                   .Returns(true)
                   .Returns(false)
                   .Returns(true)
                   .Returns(false)
                   .Returns(true);

            var obfuscator = new Obfuscations(_mockRandomizer.Object);

            var textParts = TextPart.From(given);
            var result = TextPart.ToString(obfuscator.SwapWords(textParts));

            Assert.AreEqual(expected, result);
        }
    }
}