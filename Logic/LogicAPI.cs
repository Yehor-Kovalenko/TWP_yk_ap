using System;
using Data;


namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract DataApi Repo { get; }

        public abstract int BoardWidth { get; }

        public abstract int BoardHeight { get; }

        public static LogicAPI Instantiate(int bHeight, int bWidth)
        {
            return new BallController(bHeight, bWidth);
        }

        public abstract void createBall(int posX, int posY, int speedX, int speedY, int radius);

        public abstract void removeBall(Ball ball);

        public abstract void removeAllBalls();

        public abstract void stopAllBalls();

        public abstract void createBallAtRandomPosition();

        public abstract void startAllBalls();
    }
}
 