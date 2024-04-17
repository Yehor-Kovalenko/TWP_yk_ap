using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball
    {
        public Vector2 Speed { get; set; }
        public Vector2 Position { get; set; }
        public double Radius { get; set; }

        public Ball(Vector2 k, double pr, Vector2 p)
        {
            Position = k;
            Radius = 25;
            Speed = p;
        }
    }
}
