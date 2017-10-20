namespace Examples.Randomizer
{
    public interface IRandomizer
    {
        bool ShouldBeDoneWith(double probability);
        int Next(int maxLength);
    }
}