using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace src
{
    public class MultiPartRegex : IRegex
    {
        private IRegex[] _parts;

        public MultiPartRegex(IRegex[] parts)
        {
            if (parts == null)
            {
                throw new ArgumentNullException();
            }
            _parts = parts;
        }

        public string GetLongestNonCyclicWord()
        {
            return _parts
                .Aggregate(new StringBuilder(), (sb, regex) => sb.Append(regex.GetLongestNonCyclicWord()))
                .ToString();
        }

        public IEnumerable<string> GetNonCyclicWords()
        {
            if (_parts.Length == 0)
            {
                return Enumerable.Empty<string>();
            }
            return GetNonCyclicWords(new string[] { string.Empty }, _parts);
        }

        private static IEnumerable<string> GetNonCyclicWords(IEnumerable<string> words, IEnumerable<IRegex> remainingParts)
        {
            if (!remainingParts.Any())
            {
                return words;
            }
            IRegex nextPart = remainingParts.First();

            words = new List<string>(words);

            IEnumerable<string> nextWords = nextPart.GetNonCyclicWords();
            IEnumerable<string> newWords = CreateCartesianProduct(words, nextWords);
            IEnumerable<string> toReturn = GetNonCyclicWords(newWords, remainingParts.Skip(1));

            if (nextPart is OptionalBranchRegex)
            {
                var nextOptionalPart = (OptionalBranchRegex)nextPart;
                IEnumerable<string> nextOptionalWords = nextOptionalPart.GetLongestsNotYetCyclicWords();
                IEnumerable<string> newOptionalWords = CreateCartesianProduct(words, nextOptionalWords);
                toReturn = toReturn.Concat(newOptionalWords);
            }

            return toReturn;
        }

        private static IEnumerable<string> CreateCartesianProduct(IEnumerable<string> firstSequence, IEnumerable<string> secondSequence)
        {
            return from first in firstSequence from second in secondSequence select first + second;
        }
    }
}
