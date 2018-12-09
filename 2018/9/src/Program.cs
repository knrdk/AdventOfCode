using System;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPlayers = int.Parse(args[0]);
            int lastMarble = int.Parse(args[1]);

            int winningScore = SolvePart1(numberOfPlayers, lastMarble);
            Console.WriteLine(winningScore);
        }

        private static int SolvePart1(int numberOfPlayers, int lastMarble)
        {
            Player[] players = Enumerable
                .Range(1, numberOfPlayers)
                .Select(id => new Player(id))
                .ToArray();

            var board = new Board();
            for (int i = 0; i < lastMarble; i++)
            {
                int marbleValue = i + 1;
                int playerId = i % numberOfPlayers;
                if (marbleValue % 23 != 0)
                {
                    board.MoveCurrent(1);
                    board.AddClockwiseToCurrentAndSelect(marbleValue);
                }
                else
                {
                    board.MoveCurrent(-6);
                    int removedValue = board.RemoveCounterClockwise();
                    int scored = removedValue + marbleValue;
                    players[playerId].AddToScore(scored);
                }
            }

            int winningScore = players.OrderByDescending(x=>x.Score).First().Score;
            return winningScore;
        }
    }
}
