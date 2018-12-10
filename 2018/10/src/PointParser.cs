using System.Text.RegularExpressions;

namespace src
{
    public class PointParser
    {       
        private static Regex regex = new Regex(@"^.*<\s*(-?\d+),\s*(-?\d+).*<\s*(-?\d+),\s*(-?\d+).*$", RegexOptions.Compiled);

        public static Point Parse(string input)
        {
            Match match = regex.Match(input);

            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            int speedX = int.Parse(match.Groups[3].Value);
            int speedY = int.Parse(match.Groups[4].Value);
            
            return new Point(x, y, speedX, speedY);
        }
    }
}