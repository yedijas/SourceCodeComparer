using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Token
{
    public class Koordinat
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Koordinat()
        {
            X = 0;
            Y = 0;
        }

        public Koordinat(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
