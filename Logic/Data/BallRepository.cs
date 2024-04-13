using System;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace Logic.Data
{
    internal class BallRepository : DataApi
    {
        readonly List<Ball> balls = new();

        public List<Ball> Balls
        {
            get => balls;
        }

        public override void addBall(Ball ball)
        {
            balls.Add(ball);
        }

        public override void removeBall(Ball ball)
        {
            balls.Remove(ball);
        }

        public override void removeAllBalls()
        {
            balls.Clear();
        }
    }
}