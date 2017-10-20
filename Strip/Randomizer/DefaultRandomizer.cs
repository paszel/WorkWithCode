using System;

namespace Examples.Randomizer
{
    public class DefaultRandomizer : IRandomizer
    {
        Random _random = new Random();

        public bool ShouldBeDoneWith(double probability)
        {
            return _random.NextDouble() < probability;
        }

        public int Next(int maxLength)
        {
            return _random.Next(maxLength);
        }
    }
}