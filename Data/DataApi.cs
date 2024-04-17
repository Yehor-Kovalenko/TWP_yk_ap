using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI

    {
        public abstract void addBall(Ball ball);
        public abstract Ball getBall(int i);

        public abstract int getBallsCount();

        public static Ball createBall(Vector2 k, double pr, Vector2 p)
        {
            return new Ball(k, pr, p);
        }
        public static Board createBoard(double s, double w)
        {
            return new Board(s, w);

        }
    }
}
