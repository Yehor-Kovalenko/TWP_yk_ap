using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Board
    {
        public double Height { get; }
        public double Width { get; }

        public Board(double height, double width)
        {
            Width = height;
            Height = width;
        }

    }
}
