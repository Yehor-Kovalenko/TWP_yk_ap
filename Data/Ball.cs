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
        public Vector2 Speed { 
            get
            {
                lock (this)
                {
                    return this.Speed;
                }
            } 
            set
            {
                lock (this)
                {
                    this.Speed = value;
                }
            }
        }
        public Vector2 Position { 
            get
            {
                lock (this)
                {
                    return this.Position;
                }
            }
            set
            {
                lock (this)
                {
                    this.Position = value;
                }
            }
        }
        public double Radius { get; set; }

        public double Mass { get; set; }

        public Ball(Vector2 k, double pr, Vector2 p)
        {
            Position = k;
            Radius = 25;
            Speed = p;
            Mass = 10;
        }
    }
}
