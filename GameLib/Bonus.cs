using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{

    public enum BonusType { SPEED=1, SHIELD=2, TRIPPLE_SHOT=3, NONE=0}
    public class Bonus: Shape
    {
        public BonusType Type { get; private set; }

        public Bonus(BonusType type, double x, double y)
        {
            X = x;
            Y = y;
            Type = type;
            Size = Constants.BONUS_SIZE;
        }


        public bool IsBonusActive()
        {
            return Type != BonusType.NONE;
        }

        public void DisableBonus()
        {
            Type = BonusType.NONE;
        }

    }
}
