using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Data
{
    public class BallIndex : DataAPI
    {
        private List<Ball> balls;

        public BallIndex()
        {
            balls = new List<Ball>();
        }

        public override int getBallsCount()
        {
            return balls.Count;
        }

        public override void addBall(Ball ball)
        {
            balls.Add(ball);
            Debug.WriteLine($"Kula została dodana do listy pod numerem {balls.Count}");
        }

        public override Ball getBall(int id)
        {
            return balls[id];
        }
    }
}

