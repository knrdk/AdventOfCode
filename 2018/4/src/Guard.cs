using System;
using System.Collections.Generic;

namespace src
{
    public class Guard
    {
        public int Id { get; private set; }

        public List<SleepEvent> Events {get; private set;}

        public Guard(int id)
        {
            Id = id;
            Events = new List<SleepEvent>();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Guard that = (Guard)obj;
                return (Id == that.Id);
            }
        }

        public override int GetHashCode()
        {
            return Id;
        }

    }
}
