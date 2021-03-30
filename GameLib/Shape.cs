using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public abstract class Shape
    {
        public const double HALF_OF_PI = Math.PI/2;
        public const double TWO_THIRDS_OF_PI = (2 * Math.PI) / 3;

        public double X { get; set; }
        public double Y { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public double Size { get;  set; }
        public double Angle { get; set; }

    }
}
