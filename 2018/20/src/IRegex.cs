using System;
using System.Collections.Generic;

namespace src
{
    public interface IRegex
    {
        string GetLongestNonCyclicWord();
        IEnumerable<string> GetNonCyclicWords();
    }
}
