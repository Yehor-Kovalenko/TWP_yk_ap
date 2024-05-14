using System;
using System.Collections.Generic;
using System.Text;
using Data;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class BallMovement
    {
        private Ball ball;
        public int id;
        private BallController bc;

        public double Xend;
        public double Yend;

        public event EventHandler<BallMovement>? changePosition;

        
        public BallMovement(Ball k, int id, BallController bc, double Xend, double Yend)
        {
            this.ball = k;
            this.id = id;
            this.bc = bc;
            this.Xend = Xend;
            this.Yend = Yend;
        }

        public Ball getBall()
        {
            return ball;
        }


        public async void movement()
        {
            while (!bc.CancelSimulationSource.Token.IsCancellationRequested)
            {
                Vector2 newPosition = this.ball.Position + this.ball.Speed;

                if (newPosition.X < 0 || newPosition.X + this.ball.Radius > this.Xend)
                {
                    this.ball.Speed = this.ball.Speed * new Vector2(-1, 1);
                }

                if (newPosition.Y < 0 || newPosition.Y + this.ball.Radius > this.Yend)
                {
                    this.ball.Speed = this.ball.Speed * new Vector2(1, -1);
                }

                this.ball.Position = this.ball.Position + this.ball.Speed;
                changePosition?.Invoke(this, this);
                await Task.Delay(20, bc.CancelSimulationSource.Token).ContinueWith(_ => { });
            }
        }

        public async void checkColision()
        {
            //for every ball check every other ball
            for (int i = 0; i < bc.Balls.getBallsCount() - 1; ++i)
            {
                for (int j = i + 1; j < bc.Balls.getBallsCount(); ++j)
                {
                    if (//logic for coordinate)
                    {
                        lock {
                            lock {
                                changePosition position;
                            }
                            bc.getBall(i);
                            bc.getBall(j);
                        }

                    }

                }
            }
        }

    }


}

