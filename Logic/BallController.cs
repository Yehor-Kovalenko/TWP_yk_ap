using System;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace Logic
{
    public class BallController : LogicAPI
    {

        readonly int boardWidth = 14;
        readonly int boardHeight = 14;

        public int BoardWidth
        {
            get => boardWidth;
        }

        public int BoardHeight
        {
            get => boardHeight;
        }

        public override Ball createBall(int posX, int posY, int speedX, int speedY, int radius)
        {
            return new Ball(posX, posY, speedX, speedY, radius);
        }

        public override void stopBall(Ball ball)
        {
            ball.SpeedX = 0;
            ball.SpeedY = 0;
        }

        public override Ball createBallAtRandomPosition()
        {
            Random r = new();
            int rad = 10;
            int pos_X = r.Next(rad, boardWidth - rad);
            int pos_Y = r.Next(rad, boardHeight - rad);
            int speed_X = r.Next(-5, 5);
            int speed_Y = r.Next(-5, 5);
            return new Ball(pos_X, pos_Y, speed_X, speed_Y, rad);
        }

        public override void stopAllBalls()
        {

        }

        public override void startAllBalls()
        {

        }

        public override void removeBall(Ball ball)
        {

        }

        public override void removeAllBalls()
        {

        }
    }

}
