using System;
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
    }
}
