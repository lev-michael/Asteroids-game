using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GameLib
{
    public abstract class Constants
    {

        public const double QUARTER_OF_PI = Math.PI / 4;
        public const double HALF_OF_PI = Math.PI / 2;
        public const double TWO_THIRDS_OF_PI = (Math.PI * 2) / 3;
        public const int HALF_SEC = 500;
        public const int FIVE_SEC = 5000;
        public const int FIFTEEN_SEC = 15000;
        public const int THIRTY_SEC = 30000;
        public const int ONE_MINUTE = 60000;
        public const int BONUS_SIZE = 32;
        public const string HIGH_SCORE_FILE = "..//..//highScore.txt";

    }
}
