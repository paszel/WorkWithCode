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

        public TextPart(String contents, TextPartType type)
        {
            this._contents = contents;
            this._type = type;
        }

        public bool IsWord() => Type.Equals(TextPartType.WORD);

        public static List<TextPart> From(string text)
        {
            List<TextPart> textParts = new List<TextPart>();

            MatchCollection matchCollection = Regex.Matches(text, @"(\w+|\W+)");
            foreach (Match match in matchCollection)
            {
                String foundPart = match.Groups[0].ToString();
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
    }

    public enum TextPartType
    {
        NONWORD,
        WORD
    }
}
