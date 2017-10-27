using Examples.Randomizer;

namespace NUnit.Mocks
{
    public class MockRandomizer : IRandomizer
    {
        private readonly bool[] _returns;
        private readonly int[] _indexes;
        private int currentReturns = 0, currentIndexes;

        public MockRandomizer(bool[] returns, int[] indexes)
        {
            _returns = returns;
            _indexes = indexes;
        }
        public bool ShouldBeDoneWith(double probability)
        {
            return _returns[currentReturns++];
        }

        public int RandomInteger(int maxLength)
        {
            return _indexes[currentIndexes++];
        }
    }
}