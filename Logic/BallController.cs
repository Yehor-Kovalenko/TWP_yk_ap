using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class BallController : LogicAPI
    {
        public BallIndex Balls { get; set; } //list of balls

        public Board BoardSize { get; set; }

        public CancellationTokenSource CancelSimulationSource { get; private set; }




        public BallController(double w, double h)
        {
            BoardSize = new Board(w, h);
            Balls = new BallIndex();
            CancelSimulationSource = new CancellationTokenSource();
        }


        protected override void changePosition(BallMovement args)
        {
            base.changePosition(args);
        }


        public override int getBallsCount()
        {
            return Balls.getBallsCount();
        }


        //public void createBall(double p)
        //{
        //    Random rng = new Random();
        //    double x = ((double)rng.NextDouble() * (BoardSize.Width - (2 * p)) + p);
        //    double y = ((double)rng.NextDouble() * (BoardSize.Height - (2 * p)) + p);
        //    double vx = (rng.NextDouble() - 0.5) * 20;
        //    double vy = (rng.NextDouble() - 0.5) * 20;
        //    this.Balls.addBall(new Ball(new Vector2((float)x, (float)y), p, new Vector2((float)vx, (float)vy)));
        //}



        public override void addBalls(int n)
        {
            Random rng = new Random();
            for (int i = 0; i < n; i++)
            {

                double p = (rng.NextDouble() * 20) + 20;
                double x = (rng.NextDouble() * (BoardSize.Width - (2 * p)) + p);
                double y = (rng.NextDouble() * (BoardSize.Height - (2 * p)) + p);
                double vx = (rng.NextDouble() - 0.5) * 20;
                double vy = (rng.NextDouble() - 0.5) * 20;
                this.Balls.addBall(new Ball(new Vector2((float)x, (float)y), p, new Vector2((float)vx, (float)vy)));
            }

        }


        public override Ball getBall(int i)
        {
            return Balls.getBall(i);
        }


        //public void BallTravel(Ball k)
        //{
        //    Vector2 newPosition = k.Position + k.Speed;
        //    if (newPosition.X - k.Radius < 0 && newPosition.X + k.Radius > BoardSize.Width)
        //    {
        //        k.Speed = k.Speed * new Vector2(1, -1);
        //    }

        //    if (newPosition.Y - k.Radius < 0 && newPosition.Y + k.Radius > BoardSize.Height)
        //    {
        //        k.Speed = k.Speed * new Vector2(-1, 1);
        //    }

        //    k.Position = k.Position + k.Speed;
        //}


        public override void Start()
        {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();

            for (int i = 0; i < Balls.getBallsCount(); i++)
            {
                BallMovement ball = new BallMovement(Balls.getBall(i), i, this, BoardSize.Width, BoardSize.Height);
                ball.changePosition += (_, args) => changePosition(ball);
                Task.Factory.StartNew(ball.movement, CancelSimulationSource.Token);
            }
            Thread collisionManager = new Thread(() =>
            {
                CheckForCollision();
                Thread.Sleep(6);
            });
            collisionManager.Start();

        }

        private double BallsDistance(Ball ball1, Ball ball2)
        {
            float ball1_x = ball1.Position[0];
            float ball1_y = ball1.Position[1];
            float ball2_x = ball2.Position[0];
            float ball2_y = ball2.Position[1];
            return Math.Sqrt((ball1_x - ball2_x) * (ball1_x - ball2_x) + (ball1_y - ball2_y) * (ball1_y - ball2_y));
        }

        private void CheckForCollision()
        {
            //for every ball check every other ball
            for (int i = 0; i < this.Balls.getBallsCount() - 1; ++i)
            {
                for (int j = i + 1; j < this.Balls.getBallsCount(); ++j)
                {
                    if (this.BallsDistance(getBall(i), getBall(j)) <= getBall(i).Radius + getBall(j).Radius)
                    {
                        lock (getBall(i))
                        {
                            lock (getBall(j))
                            {
                                CollisionLogic(getBall(i), getBall(j));
                                //check border for collision
                                //ball2 update ball position
                            }
                            //check border for collision
                            //ball1 update ball position
                        }

                    }

                }
            }

        }

        private void CollisionLogic(Ball ball1, Ball ball2)
        {
            float Vx1, Vy1, Vx2, Vy2;

            Vx1 = (a.Mass * a.Speed_X + b.Mass * b.Speed_X - b.Mass * (a.Speed_X - b.Speed_X)) / (a.Mass + b.Mass);
            Vy1 = (a.Mass * a.Speed_Y + b.Mass * b.Speed_Y - b.Mass * (a.Speed_Y - b.Speed_Y)) / (a.Mass + b.Mass);
            Vx2 = (a.Mass * a.Speed_X + b.Mass * b.Speed_X - a.Mass * (b.Speed_X - a.Speed_X)) / (a.Mass + b.Mass);
            Vy2 = (a.Mass * a.Speed_Y + b.Mass * b.Speed_Y - a.Mass * (b.Speed_Y - a.Speed_Y)) / (a.Mass + b.Mass);



        }


        public override void Stop()
        {
            this.CancelSimulationSource.Cancel();
        }

        //public void makeBall(double p)
        //{
        //    Random random = new Random();
        //    double x = ((double)random.NextDouble() * (BoardSize.Width - (2 * p)) + 1 + p);
        //    double y = ((double)random.NextDouble() * (BoardSize.Height - (2 * p)) + 1 + p);
        //}
    }
}
