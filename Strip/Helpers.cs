using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Strip
{
    public class Helpers
    {
        /// <summary>
        /// Strips all illegal charaters from the given lower case string.
        /// </summary>
        /// <param name="source">the original string</param>
        /// <returns>the string with all illegal characters stripped</returns>
        public static string StripIllegalCharacters(string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;

            var sb = new StringBuilder(source.Length);
            foreach (var character in source.Where(char.IsLetterOrDigit))
            {
                sb.Append(character);
            }

            return sb.ToString();
        }
    }

    public class PeselValidator
    {
        public static bool IsValid(string pesel)
        {
            if (IsNotValidFormat(pesel))
                return false;

            var sum = ComputeWeightSum(pesel);
            return CheckLastDigit(pesel[pesel.Length - 1], sum);
        }

        private static bool CheckLastDigit(char character, int sum)
        {
            var lastDigit = int.Parse(character.ToString());

            return ((10 - (sum%10))%10) == lastDigit;
        }

        private static int ComputeWeightSum(string pesel)
        {
            int[] weights = {1, 3, 7, 9, 1, 3, 7, 9, 1, 3};
            var sum = 0;

            for (var i = 0; i < pesel.Length - 1; i++)
            {
                var digit = int.Parse(pesel[i].ToString());
                sum += weights[i]*digit;
            }

            return sum;
        }

        private static bool IsNotValidFormat(string pesel)
        {
            return string.IsNullOrEmpty(pesel) || pesel.Length != 11 || pesel.Any(x => !char.IsDigit(x));
        }
    }
}
