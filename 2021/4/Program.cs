using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC4
{
    public class Program
    {
        const string inputFile = "input.txt";
        const int partToSolve = 1; // 2;

        public static void Main(string[] args)
        {
            string[] inputLines = File.ReadAllLines(inputFile);

            int[] randomNumbers = inputLines[0].Split(',').Select(int.Parse).ToArray();

            List<int[][]> boards = new List<int[][]>();
            int[][] currentBoard = null;
            int currentBoardRow = 0;
            foreach (var line in inputLines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (currentBoard == null)
                {
                    currentBoard = new int[5][];
                    currentBoardRow = 0;
                }


                currentBoard[currentBoardRow] = line.Split(' ').Where(x => !String.IsNullOrEmpty(x)).Select(x => x.Trim()).Select(int.Parse).ToArray();

                if (currentBoardRow == 4)
                {
                    boards.Add(currentBoard);
                    currentBoard = null;
                }

                currentBoardRow++;
            }

            // input has been read

            if (partToSolve == 1)
            {
                // part1
                (int board, int random) = GetWinner(randomNumbers, boards);
                int boardScore = CalculateBoardScore(boards[board]);

                int part1Solution = boardScore * random;
                Console.WriteLine($"Part1: {part1Solution}");
            }
            else if (partToSolve == 2)
            {
                // part2
                (int board, int random) = GetLooser(randomNumbers, boards);
                int boardScore = CalculateBoardScore(boards[board]);

                int part1Solution = boardScore * random;
                Console.WriteLine($"Part1: {part1Solution}");
            }
        }

        private static (int board, int random) GetWinner(int[] randomNumbers, List<int[][]> boards)
        {
            foreach (var randomNumber in randomNumbers)
            {
                // mark
                foreach (var board in boards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[i][j] == randomNumber)
                            {
                                board[i][j] += 100;
                            }
                        }
                    }
                }

                // check winners

                for (int boardNumber = 0; boardNumber < boards.Count; boardNumber++)
                {
                    int[][] board = boards[boardNumber];
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[i][j] < 100)
                            {
                                break;
                            }

                            if (j == 4)
                            {
                                // we have found winner;
                                Console.WriteLine($"Winner board: {boardNumber + 1}, random: {randomNumber}");
                                return (boardNumber, randomNumber);
                            }
                        }
                    }
                }

                for (int boardNumber = 0; boardNumber < boards.Count; boardNumber++)
                {
                    int[][] board = boards[boardNumber];
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[j][i] < 100)
                            {
                                break;
                            }

                            if (j == 4)
                            {
                                // we have found winner;
                                Console.WriteLine($"Winner board: {boardNumber + 1}, random: {randomNumber}");
                                return (boardNumber, randomNumber);
                            }
                        }
                    }
                }
            }
            return (-1, -1);
        }

        private static (int board, int random) GetLooser(int[] randomNumbers, List<int[][]> boards)
        {
            List<int> winners = new List<int>();
            int looser = -1;

            foreach (var randomNumber in randomNumbers)
            {
                // mark
                foreach (var board in boards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[i][j] == randomNumber)
                            {
                                board[i][j] += 100;
                            }
                        }
                    }
                }

                // check winners

                for (int boardNumber = 0; boardNumber < boards.Count; boardNumber++)
                {
                    if (winners.Contains(boardNumber))
                    {
                        continue;
                    }

                    int[][] board = boards[boardNumber];
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[i][j] < 100)
                            {
                                break;
                            }

                            if (j == 4)
                            {
                                if (winners.Count + 1 < boards.Count)
                                {
                                    winners.Add(boardNumber);
                                }
                                else
                                {
                                    // we have found looser;
                                    Console.WriteLine($"Looser board: {boardNumber + 1}, random: {randomNumber}");
                                    return (boardNumber, randomNumber);
                                }
                            }
                        }
                    }
                }

                for (int boardNumber = 0; boardNumber < boards.Count; boardNumber++)
                {
                    if (winners.Contains(boardNumber))
                    {
                        continue;
                    }

                    int[][] board = boards[boardNumber];
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (board[j][i] < 100)
                            {
                                break;
                            }

                            if (j == 4)
                            {
                                if (winners.Count + 1 < boards.Count)
                                {
                                    winners.Add(boardNumber);
                                }
                                else
                                {
                                    // we have found looser;
                                    Console.WriteLine($"Looser board: {boardNumber + 1}, random: {randomNumber}");
                                    return (boardNumber, randomNumber);
                                }
                            }
                        }
                    }
                }
            }
            return (-1, -1);
        }

        private static int CalculateBoardScore(int[][] board)
        {
            int score = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[i][j] < 100)
                    {
                        score += board[i][j];
                    }
                }
            }
            return score;
        }
    }
}