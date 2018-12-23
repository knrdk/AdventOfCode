using System;
using System.Collections.Generic;

namespace src
{
    public class OptionalBranchRegex : IRegex
    {
        private IRegex[] _branches;

        public OptionalBranchRegex(IRegex[] branches)
        {
            if (branches == null)
            {
                throw new ArgumentNullException();
            }
            _branches = branches;
        }

        public string GetLongestNonCyclicWord()
        {
            return string.Empty;
        }

        public IEnumerable<string> GetNonCyclicWords()
        {
            yield return string.Empty;
        }

        public IEnumerable<string> GetLongestsNotYetCyclicWords()
        {
            foreach (IRegex branch in _branches)
            {
                foreach (string word in branch.GetNonCyclicWords())
                {
                    yield return word.Substring(0, word.Length - 1);
                }
            }
        }
    }
}
