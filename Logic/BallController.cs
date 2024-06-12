using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Logic
{
    public class BallController : LogicAPI
    {
        public BallIndex Balls { get; set; } //list of balls

        public Board BoardSize { get; set; }

        public CancellationTokenSource CancelSimulationSource { get; private set; }
        private DataLogger _logger;
        private System.Timers.Timer _timer;




        public BallController(double w, double h)
        {
            BoardSize = new Board(w, h);
            Balls = new BallIndex();
            CancelSimulationSource = new CancellationTokenSource();
            this._timer = new System.Timers.Timer(10000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                Ball logBall = this.Balls.getBall(0);
                for (int i = 0; i < this.Balls.getBallsCount(); i++)
                {
                    logBall = this.Balls.getBall(i);
                    DataLogger.Instance.Log(logBall.Position, logBall.Speed, logBall.Radius);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message);
            }
            
        }


        protected override void changePosition(BallMovement args)
        {
            base.changePosition(args);
        }


        public override int getBallsCount()
        {
            return Balls.getBallsCount();
        }



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
            Debug.WriteLine($"Utworzono {n} kul");
        }


        public override Ball getBall(int i)
        {
            return Balls.getBall(i);
        }


        public override async void Start()
        {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();
            for (int i = 0; i < Balls.getBallsCount(); i++)
            {
                BallMovement ball = new BallMovement(Balls.getBall(i), i, this, BoardSize.Width, BoardSize.Height);
                ball.changePosition += (_, args) => changePosition(ball);
                Task.Factory.StartNew(ball.movement, CancelSimulationSource.Token);
            }
            while (!CancelSimulationSource.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    CheckForCollision();
                });

            }
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
                                Debug.WriteLine($"Kolizja wykryta");
                                CollisionLogic(getBall(i), getBall(j));
                                Move(getBall(j));
                                //ball2 update ball position
                            }
                            Move(getBall(i));
                            //ball1 update ball position
                        }

                    }

                }
            }

        }

        private void CollisionLogic(Ball a, Ball b)
        {
            double Vx1, Vy1, Vx2, Vy2;

            Vx1 = (a.Mass * a.Speed[0] + b.Mass * b.Speed[0] - b.Mass * (a.Speed[0] - b.Speed[0])) / (a.Mass + b.Mass);
            Vy1 = (a.Mass * a.Speed[1] + b.Mass * b.Speed[1] - b.Mass * (a.Speed[1] - b.Speed[1])) / (a.Mass + b.Mass);
            Vx2 = (a.Mass * a.Speed[0] + b.Mass * b.Speed[0] - a.Mass * (b.Speed[0] - a.Speed[0])) / (a.Mass + b.Mass);
            Vy2 = (a.Mass * a.Speed[1] + b.Mass * b.Speed[1] - a.Mass * (b.Speed[1] - a.Speed[1])) / (a.Mass + b.Mass);
            
            a.Speed *= new Vector2(-1, 1);
            a.Speed *= new Vector2(1, -1);
            Move(a);
            a.Speed = new Vector2((float)Vx1, (float)Vy1);

            b.Speed *= new Vector2(-1, 1);
            b.Speed *= new Vector2(1, -1);
            Move(b);
            b.Speed = new Vector2((float)Vx2, (float)Vy2);


        }

        public void Move(Ball a)
        {
            double boardWidth = BoardSize.Width;
            double boardHeight = BoardSize.Height;

            if (a.Position[0] + a.Speed[0] >= boardWidth - a.Radius || a.Position[0] + a.Speed[0] <= a.Radius)
            {
                //_logger.Log(a.Position, a.Speed, a.Radius);
                a.Speed *= new Vector2(-1, 1);
                //_logger.Log(a.Position, a.Speed, a.Radius);
            }
            if (a.Position[1] + a.Speed[1] >= boardHeight - a.Radius || a.Position[1] + a.Speed[1] <= a.Radius)
            {
                //_logger.Log(a.Position, a.Speed, a.Radius);
                a.Speed *= new Vector2(1, -1);
                //_logger.Log(a.Position, a.Speed, a.Radius);
            }
            a.Position += a.Speed;
        }


        public override void Stop()
        {
            this.CancelSimulationSource.Cancel();
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
