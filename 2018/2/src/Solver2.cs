using System;
using System.Collections.Generic;
using System.Text;

namespace src
{
    public class Solver2
    {
        public static string Solve(string[] input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    string result = IsMatch(input[i], input[j]);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public static string IsMatch(string a, string b)
        {
            var resultBuilder = new StringBuilder();
            bool wasDifferenceEncountered = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    resultBuilder.Append(a[i]);
                }
                else
                {
                    if (wasDifferenceEncountered)
                    {
                        return null;
                    }
                    wasDifferenceEncountered = true;
                }
            }

            return resultBuilder.ToString();
        }
    }
}
