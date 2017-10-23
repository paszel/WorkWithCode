using System.Collections.Generic;
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
            var resultWords = "";

            var specialChars = new[] { '.', ',', '!' };
            var words = result.Split(' ');
            //NOTE => add [.,!] between words
            for (var i = 0; i < words.Length - 1; i++)
            {
                if (_randomizer.ShouldBeDoneWith(0.5))
                {
                    var index = _randomizer.RandomInteger(specialChars.Length);// random.Next(specialChars.Length);
                    resultWords += words[i] + specialChars[index];
                }
                else
                {
                    resultWords += words[i] + " ";
                }
            }

            resultWords += words[words.Length - 1];

            return resultWords;

        }

        public string RemoveWhiteSpaces(string result)
        {
            return result.Replace(" ", "");
        }

        public string CharactersTransformation(string result)
        {
            var resultCharacteres = "";
            var wordCharts = result.ToCharArray();

            //NOTE => some characters toUpper, some toLower (LitWO...)
            foreach (var item in wordCharts)
            {
                if (_randomizer.ShouldBeDoneWith(0.3))
                {
                    char? newCh;
                    if (char.IsLower(item))
                    {
                        newCh = char.ToUpper(item);
                    }
                    else
                    {
                        newCh = char.ToLower(item);
                    }

                    resultCharacteres += newCh;
                }
                else
                {
                    resultCharacteres += item;
                }
            }
            return resultCharacteres;
        }

        public string RemovePolishSigns(string result)
        {
            result = result
                .Replace("ą", "a")
                .Replace("ł", "l")
                .Replace("ę", "e")
                .Replace("ń", "n")
                .Replace("ż", "z")
                .Replace("ź", "z")
                .Replace("ó", "o")
                .Replace("ś", "s")
                .Replace("ć", "c")
                .Replace("Ą", "A")
                .Replace("Ł", "L")
                .Replace("Ę", "E")
                .Replace("Ń", "N")
                .Replace("Ż", "Z")
                .Replace("Ź", "Z")
                .Replace("Ó", "O")
                .Replace("Ś", "S");

            return result;
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
                if (_randomizer.ShouldBeDoneWith(0.2))
                {
                    textParts.Insert(i, new TextPart(" ", TextPartType.NONWORD));
                    i++;
                    var index = _randomizer.RandomInteger(obfuscatorWords.Length);
                    textParts.Insert(i, new TextPart(obfuscatorWords[index], TextPartType.WORD));
                    i++;
                    textParts.Insert(i, new TextPart(" ", TextPartType.NONWORD));
                }
            }

            return textParts;
        }

        public List<TextPart> SwapWords(List<TextPart> textParts)
        {
            //NOTE => swap some words, depends on random
            for (var i = 0; i < textParts.Count; i++)
            {
                if (NeedSwap(textParts[i], i, textParts.Count))
                {
                    var temp = textParts[i];
                    textParts[i] = textParts[i + 2];
                    textParts[i + 2] = temp;
                }
            }

            return textParts;
        }

        private bool NeedSwap(TextPart element, int index, int length)
        {
            return element.IsWord() && _randomizer.ShouldBeDoneWith(0.2) && index + 2 < length;
        }
    }
}