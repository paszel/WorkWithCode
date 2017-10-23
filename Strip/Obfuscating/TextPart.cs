using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Examples.Obfuscating
{
    public class TextPart
    {
        private string _contents;
        public string Contents { 
            get { return _contents; }
        }

        private TextPartType _type;
        public TextPartType Type { get { return _type; } }

        public TextPart(string contents, TextPartType type)
        {
            _contents = contents;
            _type = type;
        }

        public bool IsWord() => Type.Equals(TextPartType.WORD);

        public static List<TextPart> From(string text)
        {
            var textParts = new List<TextPart>();
            var matchCollection = Regex.Matches(text, @"(\w+|\W+)");

            foreach (Match match in matchCollection)
            {
                var foundPart = match.Groups[0].ToString();
                if (Regex.IsMatch(foundPart, @"\w+"))
                {
                    textParts.Add(new TextPart(foundPart, TextPartType.WORD));
                }
                else
                {
                    textParts.Add(new TextPart(foundPart, TextPartType.NONWORD));
                }
            }
            return textParts;
        }

        public static string ToString(IEnumerable<TextPart> textParts)
        {
            var result = "";

            foreach (var part in textParts)
            {
                result += part.Contents;
            }

            return result;
        }

    }

    public enum TextPartType
    {
        NONWORD,
        WORD
    }
}
