using System;
using System.Text.RegularExpressions;

namespace src
{
    class Parser
    {
        private static Regex rowRegex = new Regex(@"^\[(.*)\]\s+(.*)$", RegexOptions.Compiled);
        private static Regex idRegex = new Regex(@"^.*#(\d*).*$", RegexOptions.Compiled);
        public static LogDto Parse(string input)
        {
            Match match = rowRegex.Match(input);

            DateTime date = DateTime.Parse(match.Groups[1].Value);
            string value = match.Groups[2].Value;

            if (value.Contains('#'))
            {
                Match idMatch = idRegex.Match(value);
                int id = int.Parse(idMatch.Groups[1].Value);
                return new LogDto
                {
                    DateTime = date,
                    Id = id
                };
            }

            return new LogDto
            {
                DateTime = date,
                FallsAsleep = value.Contains("asleep")
            };
        }
    }
}
