using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace src
{
    public class Parser
    {
        private const char _REGEX_OPENING = '^';
        private const char _REGEX_ENDING = '$';
        private const char _BRANCH_SEPARATOR = '|';
        private const char _BRANCH_OPENING = '(';
        private const char _BRANCH_ENDING = ')';

        private static readonly char?[] _TOKENS = { _REGEX_OPENING, _REGEX_ENDING, _BRANCH_SEPARATOR, _BRANCH_OPENING, _BRANCH_ENDING };
        public static IRegex Parse(string regex)
        {
            var inputStream = new InputStream(regex);
            inputStream.Eat(_REGEX_OPENING);
            IRegex output = ParseMultiPartRegex(inputStream);
            inputStream.Eat(_REGEX_ENDING);
            return output;
        }

        private static IRegex ParseMultiPartRegex(InputStream inputStream)
        {
            List<IRegex> parts = new List<IRegex>();

            while (inputStream.Current.HasValue && inputStream.Current != _REGEX_ENDING
            && inputStream.Current != _BRANCH_SEPARATOR && inputStream.Current != _BRANCH_ENDING)
            {
                IRegex part;
                if (inputStream.Current == _BRANCH_OPENING)
                {
                    part = ParseBranchRegex(inputStream);
                }
                else
                {
                    part = ParseConstRegex(inputStream);
                }

                parts.Add(part);
            }

            return new MultiPartRegex(parts.ToArray());
        }

        private static IRegex ParseConstRegex(InputStream inputStream)
        {
            StringBuilder sb = new StringBuilder();
            while (inputStream.Current.HasValue && !_TOKENS.Contains(inputStream.Current))
            {
                sb.Append(inputStream.Next());
            }

            return new ConstRegex(sb.ToString());
        }

        private static IRegex ParseBranchRegex(InputStream inputStream)
        {
            inputStream.Eat(_BRANCH_OPENING);
            List<IRegex> branches = new List<IRegex>();
            while (inputStream.Current.HasValue && inputStream.Current != _BRANCH_ENDING)
            {
                if (inputStream.Current == _BRANCH_SEPARATOR)
                {
                    inputStream.Next();
                    if (inputStream.Current == _BRANCH_ENDING)
                    {
                        inputStream.Eat(_BRANCH_ENDING);
                        return new OptionalBranchRegex();
                    }
                }
                IRegex branch = ParseMultiPartRegex(inputStream);
                branches.Add(branch);
            }
            inputStream.Eat(_BRANCH_ENDING);

            return new BranchRegex(branches.ToArray());
        }
    }
}
