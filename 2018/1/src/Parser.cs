using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace src
{
    public class Parser
    {
        public static IEnumerable<int> Parse(IEnumerable<string> input)
        {
            return input == null ?
                Enumerable.Empty<int>() :
                input.Select(Parse);
        }

        public static int Parse(string input)
        {
            // TODO use LanguageExt
            bool isValidNumber = int.TryParse(input, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out int result);
            return result;
        }
    }
}
