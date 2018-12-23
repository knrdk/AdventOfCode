using System;

namespace src
{
    public class InputStream
    {
        private string _input;
        private int currentIndex = 0;

        public char? Current => currentIndex < _input.Length ? _input[currentIndex] : (char?)null;

        public InputStream(string input)
        {
            _input = input;
        }

        public char? Next()
        {
            char? itemToReturn = Current;
            currentIndex++;
            return itemToReturn;
        }

        public void Eat(char c)
        {
            if (Current != c)
            {
                throw new InvalidOperationException($"Cannon eat current characted: {Current}, expected: {c}");
            }
            currentIndex++;
        }
    }
}
