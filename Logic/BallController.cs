using Logic.Data;
using System;
using System.Runtime.Serialization.Formatters;

namespace Logic
{
    public class BallController : LogicAPI
    {

        readonly int boardWidth;
        readonly int boardHeight;
        private DataApi repo;

        public override int BoardWidth
        {
            get => boardWidth;
        }

        public override int BoardHeight
        {
            get => boardHeight;
        }

        public override DataApi Repo
        {
            get => repo;
        }

        public BallController(int bWidth, int bHeight)
        {
            boardWidth = bWidth;
            boardHeight = bHeight;
            repo = DataApi.instantiate();
        }

        public override Ball createBall(int posX, int posY, int speedX, int speedY, int radius)
        {
            return new Ball(posX, posY, speedX, speedY, radius);
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

        public override void startAllBalls()
        {
            for(int i = 0; i < repo.Balls.Count; i++)
            {
                repo.Balls[i].startMovement(boardWidth, BoardHeight);
            }
        }

        public override void stopAllBalls()
        {
            for(int i = 0; i < repo.Balls.Count; i++)
            {
                repo.Balls[i].BallTimer.Dispose();
            }
        }

        public override void removeBall(Ball ball)
        {
            repo.removeBall(ball);
        }

        public override void removeAllBalls()
        {
            repo.removeAllBalls();
        }

        static void Main() { }
    }

}
