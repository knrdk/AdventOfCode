using System.Collections.Generic;
using System.Text;

namespace src
{
    class Player
    {
        public int Id { get; }
        public int Score { get; private set; }
        
        public Player(int id)
        {
            Id = id;
        }

        public void AddToScore(int value)
        {
            Score += value;
        }
    }
}