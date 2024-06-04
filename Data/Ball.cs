using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Data
{
    public class Ball
    {
        private Vector2 position;
        private Vector2 speed;
        public double Radius { get; set; }

        public int Mass { get; set; }

        private static object _lockObject = new object();

        public Ball(Vector2 k, double pr, Vector2 p)
        {
            Random r = new Random();
            Position = k;
            Radius = 10;
            Speed = p;
            Mass = r.Next(1, 10);
            Debug.WriteLine($"Kula zostala utworzona");
        }

        public Vector2 Position
        {
            get
            {
               lock (_lockObject)
                {
                    return position;
                }

            }
            set
            {
                lock (_lockObject)
                {
                    position = value;
                }
            }
        }
        public Vector2 Speed
        {
            get
            {
                lock (_lockObject)
                {
                    return speed;
                }

            }
            set
            {
                lock (_lockObject)
                {
                    speed = value;
                }
            }
        }
    }
}
