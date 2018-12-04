using System;
using System.Collections.Generic;

namespace src
{
    public class GuardSleepingActivity
    {
        private int? mostSleepMinute;

        public int Id { get; private set; }

        public int[] SleepingActivity { get; private set; }
        public int TotalSleepingTime { get; private set; }

        public GuardSleepingActivity(int id)
        {
            Id = id;
            SleepingActivity = new int[60];
            TotalSleepingTime = 0;
            mostSleepMinute = null;
        }

        public void Sleep(int startMinute, int endMinute)
        {
            mostSleepMinute = null;
            TotalSleepingTime += endMinute - startMinute;
            for (int i = startMinute; i < endMinute; i++)
            {
                SleepingActivity[i] += 1;
            }
        }

        public int GetMostSleepMinute()
        {
            if (mostSleepMinute.HasValue)
            {
                return mostSleepMinute.Value;
            }

            int maxKey = -1;
            int maxValue = -1;
            for (int i = 0; i < 60; i++)
            {
                int currentValue = SleepingActivity[i];
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxKey = i;
                }
            }
            mostSleepMinute = maxKey;
            return mostSleepMinute.Value;
        }

        public int GetMostSleepMinuteCount()
        {
            int key = GetMostSleepMinute();
            return SleepingActivity[key];
        }
    }
}
