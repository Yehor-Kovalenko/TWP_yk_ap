using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class BallController : LogicAPI
    {
        public BallIndex Balls { get; set; }

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


        public void createBall(double p)
        {
            Random rng = new Random();
            double x = ((double)rng.NextDouble() * (BoardSize.Width - (2 * p)) + p);
            double y = ((double)rng.NextDouble() * (BoardSize.Height - (2 * p)) + p);
            double vx = (rng.NextDouble() - 0.5) * 20;
            double vy = (rng.NextDouble() - 0.5) * 20;
            this.Balls.addBall(new Ball(new Vector2((float)x, (float)y), p, new Vector2((float)vx, (float)vy)));
        }



        public override void addBall(int n)
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


        public void BallTravel(Ball k)
        {
            Vector2 newPosition = k.Position + k.Speed;
            if (newPosition.X - k.Radius < 0 && newPosition.X + k.Radius > BoardSize.Width)
            {
                k.Speed = k.Speed * new Vector2(1, -1);
            }

            if (newPosition.Y - k.Radius < 0 && newPosition.Y + k.Radius > BoardSize.Height)
            {
                k.Speed = k.Speed * new Vector2(-1, 1);
            }

            k.Position = k.Position + k.Speed;
        }


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

        }


        public override void Stop()
        {
            this.CancelSimulationSource.Cancel();
        }

        public void makeBall(double p)
        {
            Random random = new Random();
            double x = ((double)random.NextDouble() * (BoardSize.Width - (2 * p)) + 1 + p);
            double y = ((double)random.NextDouble() * (BoardSize.Height - (2 * p)) + 1 + p);
        }
    }
}
