using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Data
{
    public class Ball
    {
        public Vector2 Speed { get; set; }
        public Vector2 Position { get; set; }
        public double Radius { get; set; }

        public int Mass { get; set; }

        public Ball(Vector2 k, double pr, Vector2 p)
        {
            Random r = new Random();
            Position = k;
            Radius = 10;
            Speed = p;
            Mass = r.Next(1, 10);
            Debug.WriteLine($"Kula zostala utworzona");
        }
    }
}
