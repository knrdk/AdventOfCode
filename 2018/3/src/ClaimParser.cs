using System;
using System.Text.RegularExpressions;

namespace src
{
    public static class ClaimParser
    {
        private static Regex regex = new Regex(@"^#(\d*).*@\D*(.*),(.*):\D*(.*)x(.*)$", RegexOptions.Compiled);

        public static ClaimDto Parse(string input)
        {
            Match match = regex.Match(input);

            int id = int.Parse(match.Groups[1].Value);
            int leftEdge = int.Parse(match.Groups[2].Value);
            int topEdge = int.Parse(match.Groups[3].Value);
            int width = int.Parse(match.Groups[4].Value);
            int height = int.Parse(match.Groups[5].Value);

            return new ClaimDto(id, leftEdge, topEdge, width, height);
        }
    }
}
