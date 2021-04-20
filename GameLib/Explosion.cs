using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameLib
{
    class Explosion: Shape
    {
        private const int IMAGE_SIZE = 128;
        public int Counter { get; set; }
        public Explosion(double x, double y, int size)
        {
            Counter = 0;
            X = x - IMAGE_SIZE/2;
            Y = y - IMAGE_SIZE / 2;
            Size = 3*size;
        }
    }
}
