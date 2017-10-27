using Examples;
using NUnit.Framework;

namespace NUnit.Tests
{
    [TestFixture]
    public class PeselValidatorTest
    {
        [TestCase(null, false)]
        [TestCase(" ", false)]
        [TestCase("           ", false)]
        [TestCase("123456789", false)]
        [TestCase("1234567890142323432324234324234", false)]
        public void ShouldReturnFalseWhenInvalidPesel(string pesel, bool expected)
        {
            var result = PeselValidator.IsValid(pesel);

            Assert.AreEqual(expected, result);
        }

        [TestCase("23456789012", false)]
        [TestCase("76543765434", false)]
        [TestCase("12345678901", false)]
        public void ShouldReturnFalseIfLengthIsOk(string pesel, bool expected)
        {
            var result = PeselValidator.IsValid(pesel);

            Assert.AreEqual(expected, result);
        }

        [TestCase("02070803628", true)]
        [TestCase("56040216479", true)]
        [TestCase("47083072638", true)]
        [TestCase("45030996123", true)]
        [TestCase("82040188726", true)]
        [TestCase("60032361777", true)]
        [TestCase("48121185552", true)]
        [TestCase("51080781745", true)]
        [TestCase("60062692399", true)]
        [TestCase("82011389369", true)]
        [TestCase("61082822582", true)]
        public void ShouldReturnTrueWhenValidPesel(string pesel, bool expected)
        {
            var result = PeselValidator.IsValid(pesel);
            Assert.AreEqual(expected, result);
        }
    }
}