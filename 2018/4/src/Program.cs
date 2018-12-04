using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];
            List<LogDto> inputs = File.ReadAllLines(filename).Select(Parser.Parse).ToList();
            inputs.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));


            Dictionary<int, int> totalTimeAsleep = new Dictionary<int, int>();
            Dictionary<int, int[]> minutesAsleep = new Dictionary<int, int[]>();

            int currentGuardId = 0;
            int minuteFallAsleep = 0;
            foreach (var input in inputs)
            {
                if (input.Id > 0)
                {
                    currentGuardId = input.Id;
                }
                else if (input.FallsAsleep)
                {
                    minuteFallAsleep = input.DateTime.Minute;
                }
                else
                {
                    int minutesSlept = input.DateTime.Minute - minuteFallAsleep;
                    if (!totalTimeAsleep.ContainsKey(currentGuardId))
                    {
                        totalTimeAsleep[currentGuardId] = 0;
                        minutesAsleep[currentGuardId] = new int[60];
                    }
                    totalTimeAsleep[currentGuardId] += minutesSlept;

                    for (int i = minuteFallAsleep; i < input.DateTime.Minute; i++)
                    {
                        minutesAsleep[currentGuardId][i] += 1;
                    }
                }
            }
            int mostSleepingGuard = totalTimeAsleep.OrderBy(x => x.Value).Last().Key;
            int maxKey = -1;
            int maxValue = -1;
            for (int i = 0; i < 60; i++)
            {
                int currentValue = minutesAsleep[mostSleepingGuard][i];
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxKey = i;
                }
            }
            Console.WriteLine($"Part1 - Guard {mostSleepingGuard} minute {maxKey}");

            // part 2
            maxKey = -1;
            maxValue = -1;
            int maxGuardId = -1;
            foreach (int guardId in minutesAsleep.Keys)
            {                
                for (int i = 0; i < 60; i++)
                {
                    int currentValue = minutesAsleep[guardId][i];
                    if (currentValue > maxValue)
                    {
                        maxValue = currentValue;
                        maxKey = i;
                        maxGuardId = guardId;
                    }
                }
            }
            Console.WriteLine($"Part2 - Guard {maxGuardId} minute {maxKey}");
        }
    }
}
