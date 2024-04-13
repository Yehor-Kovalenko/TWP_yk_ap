using System;

namespace Logic.Data
{
    public abstract class DataApi
    {
        public static DataApi instantiate()
        {
            return new BallRepository();
        }

        public abstract void addBall(Ball ball);

        public abstract void removeBall(Ball ball);

        public abstract void removeAllBalls();
    }
}