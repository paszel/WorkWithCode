using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Strip.Obfuscating
{
    public class TextObfuscator
    {
        //REFACTOR => move Convert method to TextPart as a factory method
        private Random random = new Random();

        public string Convert(string text)
        {
            var textParts = TextPart.From(text);
            textParts = SwapWords(textParts);
            textParts = InsertObfuscators(textParts);

            var result = ToString(textParts);
            result = RemovePunctuationMarks(result);
            result = InsertSpecialCharacters(result);
            result = RemovePolishSigns(result);
            result = CharactersTransformation(result);
            result = RemoveWhiteSpaces(result);

            return result;
        }

        private static string RemoveWhiteSpaces(string result)
        {
            //NOTE => remove spaces
            result = result.Replace(" ", "");
            return result;
        }

        private string CharactersTransformation(string result)
        {
            var resultCharacteres = "";
            var wordCharts = result.ToCharArray();

            //NOTE => some characters toUpper, some toLower (LitWO...)
            foreach (var item in wordCharts)
            {
                if (random.NextDouble() < 0.3)
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

        private static string RemovePolishSigns(string result)
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

        private string InsertSpecialCharacters(string result)
        {
            var resultWords = "";

            var specialChars = new[] { '.', ',', '!' };
            var words = result.Split(' ');
            //NOTE => add [.,!] between words
            for (var i = 0; i < words.Length - 1; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    var index = random.Next(specialChars.Length);
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

        private static string RemovePunctuationMarks(string result)
        {
            //NOTE => removes unique characters
            return Regex.Replace(result, @"[\?!-\.,:;'\(\)]", "", RegexOptions.CultureInvariant);
        }

        private static string ToString(List<TextPart> textParts)
        {
            var result = "";

            foreach (var part in textParts)
            {
                result += part.Contents;
            }

            return result;
        }

        private List<TextPart> InsertObfuscators(List<TextPart> textParts)
        {
            //NOTE => insert " ", [i, a, aczkolwiek, poniekad], " " between words
            var obfuscatorWords = new[] { "i", "a", "aczkolwiek", "poniekąd" };

            for (var i = 0; i < textParts.Count; i++)
            {
                if (random.NextDouble() < 0.2)
                {
                    textParts.Insert(i, new TextPart(" ", TextPartType.NONWORD));
                    i++;
                    var index = random.Next(obfuscatorWords.Length);
                    textParts.Insert(i, new TextPart(obfuscatorWords[index], TextPartType.WORD));
                    i++;
                    textParts.Insert(i, new TextPart(" ", TextPartType.NONWORD));
                }
            }

            return textParts;
        }

        private List<TextPart> SwapWords(List<TextPart> textParts)
        {
            //NOTE => swap some words, depends on random
            for (var i = 0; i < textParts.Count; i++)
            {
                //REFACTOR => join ifs
                //SMELL => condition to Method
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
            return element.IsWord() && random.NextDouble() < 0.2 && index + 2 < length;
        }
    }
}