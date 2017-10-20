using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Strip.Obfuscating
{
    public class TextManagerHelper
    {
        private Random random = new Random();

        public List<TextPart> From(string text)
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
}