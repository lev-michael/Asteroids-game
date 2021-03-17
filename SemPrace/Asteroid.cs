using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemPrace
{
    enum AsteroidSize { BIG, MEDIUM, SMALL }
    class Asteroid
    {
        public int Speed { get; }
        public AsteroidSize Size { get; }

        Asteroid()
        {

        }

        Asteroid(AsteroidSize size)
        {
            switch (size)
            {
                case AsteroidSize.BIG:
                    break;
                case AsteroidSize.MEDIUM:
                    break;
                case AsteroidSize.SMALL:
                    break;
            }
        }
    }
}
