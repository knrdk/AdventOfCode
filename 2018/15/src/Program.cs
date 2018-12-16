using System;
using System.IO;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            string[] inputLines = File.ReadAllLines(fileName);

            Game game = InputParser.Parse(inputLines);

            while (!game.IsCompleted)
            {
                game.NextTurn();
            }

            Console.WriteLine(game);
            Console.WriteLine(game.Turn);
            Console.WriteLine(game.Score);
        }
    }
}
