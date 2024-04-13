using System;
using Logic.Data;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract Ball createBall(int posX, int posY, int speedX, int speedY, int radius);

        public abstract void removeBall(Ball ball);

        public abstract void removeAllBalls();

        public abstract void stopBall(Ball ball);

        public abstract void stopAllBalls();

        public abstract Ball createBallAtRandomPosition();

        public abstract void startAllBalls();
    }
}
 