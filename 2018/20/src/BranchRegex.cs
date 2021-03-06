using System;
using System.Collections.Generic;

namespace src
{
    public class BranchRegex : IRegex
    {
        private IRegex[] _branches;

        public BranchRegex(IRegex[] branches)
        {
            if (branches == null)
            {
                throw new ArgumentNullException();
            }
            _branches = branches;
        }

        public string GetLongestNonCyclicWord()
        {
            string longestWord = string.Empty;
            foreach (IRegex branch in _branches)
            {
                string branchLongestWord = branch.GetLongestNonCyclicWord();
                if (branchLongestWord.Length > longestWord.Length)
                {
                    longestWord = branchLongestWord;
                }
            }
            return longestWord;
        }

        public IEnumerable<string> GetNonCyclicWords()
        {
            foreach (IRegex branch in _branches)
            {
                foreach (string word in branch.GetNonCyclicWords())
                {
                    yield return word;
                }
            }
        }
    }
}
