using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LanguageExt;
using static LanguageExt.Option<int>;

namespace src
{
    public class Parser
    {
        public static IEnumerable<Option<int>> Parse(IEnumerable<string> input)
        {
            return input == null ?
                Enumerable.Empty<Option<int>>() :
                input.Select(Parse);
        }

        public static Option<int> Parse(string input)
        {
            // TODO use LanguageExt
            bool isValidNumber = int.TryParse(input, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out int result);
            return isValidNumber? Some(result): None;
        }
    }
}
