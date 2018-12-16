using System;

namespace src
{
    class Unit
    {
        public Guid Id { get; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int HitPoints { get; private set; }
        public int AttackPower { get; }
        public UnitType Type { get; }
        public bool IsAlive => HitPoints > 0;

        public Unit(int x, int y, UnitType type)
        {
            Id = Guid.NewGuid();
            X = x;
            Y = y;
            Type = type;
            HitPoints = 200;
            AttackPower = 3;
        }

        public void Attack(Unit unitToAttack)
        {
            unitToAttack.HitPoints -= AttackPower;
        }

        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return (obj is Unit) && Id == ((Unit)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}