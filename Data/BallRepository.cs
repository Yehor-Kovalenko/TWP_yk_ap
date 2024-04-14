using System;
using System.Collections.Generic;

namespace Data
{
    public class BallRepository : DataApi
    {
        private List<Ball> balls = new List<Ball>();

        public BallRepository() 
        { 
        }

        public override List<Ball> Balls
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

        public override List<Ball> getAllBalls()
        {
            return balls;
        }

        static void Main() { }
    }
}