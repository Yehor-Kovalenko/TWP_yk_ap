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
        private readonly object _lockObject = new object();
        public Vector2 Speed
        {
            get
            {
                lock (_lockObject)
                {
                    return Speed;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    Speed = value;
                }
            }
        }
        public Vector2 Position
        {
            get
            {
                lock (_lockObject)
                {
                    return Position;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    Position = value;
                }
            }
        }

        public double Radius { get; set; }

        public int Mass { get; set; }

        public Ball(Vector2 k, double pr, Vector2 p)
        {
            Random r = new Random();
            Position = k;
            Radius = 10;
            Speed = p;
            Mass = r.Next(1, 10);
        }
    }
}
