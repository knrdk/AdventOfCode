using System;
using System.Text.RegularExpressions;

namespace src
{
    class Parser
    {
        private static Regex rowRegex = new Regex(@"^\[(.*)\]\s+(.*)$", RegexOptions.Compiled);

        public static LogDto Parse(string input)
        {
            Match match = rowRegex.Match(input);

            DateTime date = DateTime.Parse(match.Groups[1].Value);
            string value = match.Groups[2].Value;

            return new LogDto
            {
                DateTime = date,
                Value = value
            };
        }
    }
}
