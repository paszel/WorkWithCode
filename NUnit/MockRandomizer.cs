using Examples.Randomizer;

namespace NUnit
{
    public class MockRandomizer : IRandomizer
    {
        private readonly bool[] _returns;
        private int current = 0;

        public MockRandomizer(bool [] returns)
        {
            _returns = returns;
        }
        public bool ShouldBeDoneWith(double probability)
        {
            return _returns[current++];
        }
    }
}