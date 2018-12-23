using System;

namespace src
{
    public class ConstRegex : IRegex
    {
        private string _value;

        public ConstRegex(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            _value = value;
        }

        public string GetLongestNonCyclicWord()
        {
            return _value;
        }
    }
}
