using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Token
{
    class MatrixCell
    {
        public int Direction { get; set; }
        public double Value { get; set; }

        public MatrixCell(int dir, double val)
        {
            Direction = dir;
            Value = val;
        }

        public override string ToString()
        {
            return "(" + Value + " , " + Direction + ")";
        }
    }
}
