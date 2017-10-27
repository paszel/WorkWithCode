using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Examples.Randomizer;

namespace Examples.Obfuscating
{
    public class Obfuscations 
    {
        private readonly IRandomizer _randomizer;

        public Obfuscations(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string InsertSpecialCharacters(string result)
        {
            var specialChars = new[] { '.', ',', '!' };
            var words = result.Split(' ');
            
            for (var i = 0; i < words.Length - 1; i++)
            {
                if (_randomizer.ShouldBeDoneWith(0.5))
                {
                    var index = _randomizer.RandomInteger(specialChars.Length);
                    words[i] += specialChars[index];
                }
                else
                {
                    words[i] += " ";
                }
            }

            return string.Join("", words);

        }

        public string RemoveWhiteSpaces(string result)
        {
            return result.Replace(" ", "");
        }

        public string CharactersTransformation(string result)
        {
            var array = result.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];

                if (_randomizer.ShouldBeDoneWith(0.3))
                {
                    array[i] = char.IsLower(item) ? char.ToUpper(item) : char.ToLower(item); 
                }
            }

            return string.Join("", array);
        }

        public string ReplacePolishSigns(string result)
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(result));
        }

        public string RemovePunctuationMarks(string result)
        {
            return Regex.Replace(result, @"[\?!-\.,:;'\(\)]", "", RegexOptions.CultureInvariant);
        }

        public List<TextPart> InsertObfuscators(List<TextPart> textParts)
        {
            //NOTE => insert " ", [i, a, aczkolwiek, poniekad], " " between words
            var obfuscatorWords = new[] { "i", "a", "aczkolwiek", "poniekąd" };

            for (var i = 0; i < textParts.Count; i++)
            {
                if (!_randomizer.ShouldBeDoneWith(0.2))
                    continue;

                textParts.Insert(i, new TextPart(" ", TextPartType.NONWORD));
                i++;
                var index = _randomizer.RandomInteger(obfuscatorWords.Length);
                textParts.Insert(i, new TextPart(obfuscatorWords[index], TextPartType.WORD));
                i++;
                textParts.Insert(i, new TextPart(" ", TextPartType.NONWORD));
            }

            return textParts;
        }

        public List<TextPart> SwapWords(List<TextPart> textParts)
        {
            //NOTE => swap some words, depends on random
            for (var i = 0; i < textParts.Count - 2; i++)
            {
                if (!NeedSwap(textParts[i]))
                    continue;

                var temp = textParts[i];
                textParts[i] = textParts[i + 2];
                textParts[i + 2] = temp;
            }

            return textParts;
        }

        private bool NeedSwap(TextPart element)
        {
            return element.IsWord() && _randomizer.ShouldBeDoneWith(0.2);
        }
    }
}