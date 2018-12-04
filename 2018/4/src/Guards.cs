using System;
using System.Collections.Generic;

namespace src
{
    public class Guards
    {
        public Guard LastGuard { get; private set; }

        public HashSet<Guard> AllGuards { get; private set; }

        public Guards()
        {
            AllGuards = new HashSet<Guard>();
        }

        public void Add(Guard guard)
        {
            bool wasNew = AllGuards.Add(guard);
            AllGuards.TryGetValue(guard, out Guard lastGuard);
            LastGuard = lastGuard;
        }

        public void Add(SleepEvent @event)
        {
            LastGuard.Events.Add(@event);
        }
    }
}