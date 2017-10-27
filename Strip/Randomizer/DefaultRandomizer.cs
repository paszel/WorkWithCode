using System;

namespace Examples.Randomizer
{
    public class DefaultRandomizer : IRandomizer
    {
        private readonly Random _random = new Random();

        public bool ShouldBeDoneWith(double probability)
        {
            return _random.NextDouble() < probability;
        }

        public int RandomInteger(int maxLength)
        {
            return _random.Next(maxLength);
        }
    }
}