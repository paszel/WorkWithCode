using Examples.Randomizer;

namespace Examples.Obfuscating
{
    public class TextObfuscator
    {
        //REFACTOR => move Convert method to TextPart as a factory method
        private readonly IRandomizer _randomizer;
        private readonly Obfuscations _obfuscations;

        public TextObfuscator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
            _obfuscations = new Obfuscations(_randomizer);
        }

        public string Convert(string text)
        {
            var textParts = TextPart.From(text);
            textParts = _obfuscations.SwapWords(textParts);
            textParts = _obfuscations.InsertObfuscators(textParts);

            var result = TextPart.ToString(textParts);
            result = _obfuscations.RemovePunctuationMarks(result);
            result = _obfuscations.InsertSpecialCharacters(result);
            result = _obfuscations.RemovePolishSigns(result);
            result = _obfuscations.CharactersTransformation(result);
            result = _obfuscations.RemoveWhiteSpaces(result);

            return result;
        }
    }
}