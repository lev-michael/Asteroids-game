using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public enum BonusType { SPEED, SHIELD, TRIPPLE_SHOT, NONE}
    public class Bonus: Shape
    {
        public BonusType Type { get; private set; }

        public Bonus(BonusType type, double x, double y)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
