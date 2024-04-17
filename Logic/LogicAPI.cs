using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAPI
    {

        public event EventHandler<BallMovement>? PositionChangeEvent;

        public abstract Ball getBall(int index);

        public abstract void addBall(int liczbaKul);

        public abstract int getBallsCount();

        public abstract void Start();

        public abstract void Stop();

        protected virtual void changePosition(BallMovement k)
        {
            PositionChangeEvent?.Invoke(this, k);
        }
    }
}
