using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];
            var inputs = File
                .ReadAllLines(filename)
                .Select(Parser.Parse)
                .OrderBy(x => x.DateTime)
                .Aggregate(new Guards(), CreateModel);

            IEnumerable<Guard> guards = inputs.AllGuards;

            List<GuardSleepingActivity> guardsSleepingActivity = guards.Select(CalcualteGuardSleepingActivity).ToList();

            // part 1
            GuardSleepingActivity part1Winner = guardsSleepingActivity.OrderBy(x => x.TotalSleepingTime).Last();
            ShowWinner(part1Winner);

            // part 2
            GuardSleepingActivity part2Winner = guardsSleepingActivity.OrderBy(x => x.GetMostSleepMinuteCount()).Last();
            ShowWinner(part2Winner);
        }

        private static Guards CreateModel(Guards guards, LogDto logLine)
        {
            Regex idRegex = new Regex(@"^.*#(\d*).*$", RegexOptions.Compiled);
            if (logLine.Value.Contains('#'))
            {
                Match idMatch = idRegex.Match(logLine.Value);
                int id = int.Parse(idMatch.Groups[1].Value);
                var guard = new Guard(id);
                guards.Add(guard);
            }
            else
            {
                var sleepEvent = new SleepEvent
                {
                    DateTime = logLine.DateTime,
                    IsFallingAsleep = logLine.Value.Contains("asleep")
                };
                guards.Add(sleepEvent);
            }
            return guards;
        }

        private static GuardSleepingActivity CalcualteGuardSleepingActivity(Guard guard)
        {
            var sleepingActivity = new GuardSleepingActivity(guard.Id);
            for (int i = 0; i < guard.Events.Count; i += 2)
            {
                var startEvent = guard.Events[i];
                var endEvent = guard.Events[i + 1];
                sleepingActivity.Sleep(startEvent.DateTime.Minute, endEvent.DateTime.Minute);
            }
            return sleepingActivity;
        }

        public static void ShowWinner(GuardSleepingActivity winner)
        {
            Console.WriteLine($"Part 1 - Guard id: {winner.Id} total sleeping time: {winner.GetMostSleepMinute()}");
        }
    }
}
