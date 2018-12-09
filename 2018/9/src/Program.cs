using System;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPlayers = int.Parse(args[0]);
            long lastMarble = long.Parse(args[1]);

            long winningScore = Solve(numberOfPlayers, lastMarble);
            Console.WriteLine(winningScore);
        }

        private static long Solve(int numberOfPlayers, long lastMarble)
        {
            Player[] players = Enumerable
                .Range(1, numberOfPlayers)
                .Select(id => new Player(id))
                .ToArray();

            var board = new Board<long>();
            for (long i = 0; i < lastMarble; i++)
            {
                long marbleValue = i + 1;
                int playerId = (int)(i % numberOfPlayers);
                if (marbleValue % 23 != 0)
                {
                    board.MoveCurrent(1);
                    board.AddClockwiseToCurrentAndSelect(marbleValue);
                }
                else
                {
                    board.MoveCurrent(-6);
                    long removedValue = board.RemoveCounterClockwise();
                    long scored = removedValue + marbleValue;
                    players[playerId].AddToScore(scored);
                }
            }

            long winningScore = players.OrderByDescending(x => x.Score).First().Score;
            return winningScore;
        }
    }
}
