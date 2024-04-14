using System;
using Logic;
using System.Collections.Generic;

namespace Logic.Data
{
    public abstract class DataApi
    {
        public abstract List<Ball>? Balls { get; }

        public static BallRepository instantiate()
        {
            return new BallRepository();
        }

        public abstract void addBall(Ball ball);

        public abstract void removeBall(Ball ball);

        public abstract void removeAllBalls();

        public abstract List<Ball> getAllBalls();
    }
}