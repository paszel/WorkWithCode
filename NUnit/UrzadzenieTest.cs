using NUnit.Framework;
using Strip;

namespace NUnit
{
    [TestFixture]
    public class UrzadzenieTest
    {
        [TestCase(null, null, null, null)]
        [TestCase(1, null, null, 1)]
        [TestCase(null, 1, null, 1)]
        [TestCase(null, null, 1, 1)]
        [TestCase(1, 2, null, 3)]
        [TestCase(1, null, 2, 2)]
        [TestCase(null, 1, 2, 2)]
        [TestCase(1, 48, 3, 3)]
        [TestCase(10, 2, null, 12)]
        public void CzesciOgolemWartosciBrzegowe(int? ruchome, int? stale, int? ogolem, int? oczekiwanyWynik)
        {
            var urzadzenie = new Urzadzenie
            {
                CzesciOgolem = ogolem,
                CzesciRuchomych = ruchome,
                CzesciStalych = stale
            };

            Assert.AreEqual(oczekiwanyWynik, urzadzenie.CzesciOgolem);
        }
    }
}
