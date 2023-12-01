using System.IO;


namespace AoC2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "input.txt";
            var allLines = File.ReadAllLines(inputFile);

            long depth = 0;
            long position = 0;
            long aim = 0;
            long depth2 = 0;

            foreach (string line in allLines)
            {
                string[] command = line.Split(' ');
                string instruction = command[0];
                int commandValue = int.Parse(command[1]);

                if (instruction == "forward")
                {
                    position += commandValue;
                    depth2 += aim * commandValue;
                }
                else if (instruction == "down")
                {
                    depth += commandValue;
                    aim += commandValue;
                }
                else if (instruction == "up")
                {
                    depth -= commandValue;
                    aim -= commandValue;
                }
                else
                {
                    throw new InvalidDataException();
                }
            }

            long part1Solution = position * depth;
            long part2Solution = position * depth2;
            Console.WriteLine($"Part1: {part1Solution}");
            Console.WriteLine($"Part2: {part2Solution}");
        }
    }
}