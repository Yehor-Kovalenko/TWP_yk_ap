using System;
//using Timer = System.Timers.Timer;
using System.Threading;
using System.Numerics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {

        int posX;
        int posY;
        int speedX;
        int speedY;
        int radius;
        private Timer? ballTimer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int PosX
        {
            get => posX;
            set { posX = value; }
        }

        public int PosY
        {
            get => posY;
            set { posY = value; }
        }

        public int SpeedX
        {
            get => speedX;
            set { speedX = value; }
        }

        public int SpeedY
        {
            get => speedY;
            set { speedY = value; }
        }

        public int Radius
        {
            get => radius;
            set { radius = value; }
        }

        public Timer? BallTimer
        {
            get => ballTimer;
            set { ballTimer = value; }
        }


        public Ball(int pos_X, int pos_Y, int speed_X, int speed_Y, int rad)
        {
            posX = pos_X;
            posY = pos_Y;
            speedX = speed_X;
            speedY = speed_Y;
            radius = rad;
        }

        public void move(object? obj)
        { 
            if (obj == null) throw new ArgumentNullException("object is null");
            if (obj is Vector2)
            {
                Vector2 boardSize = (Vector2)obj;
                if (posX + speedX >= boardSize[0] - radius || posX + speedX <= radius)
                {
                    changeXdirection();
                }
                if (posY + speedY >= boardSize[1] - radius || posY + speedY <= radius)
                {
                    changeYdirection();
                }
                posX += speedX;
                posY += speedY;
            }
            else
            {
                throw new ArgumentException("object is not a vector");
            }
        }

        void changeXdirection()
        {
            speedX *= (-1);
        }

        void changeYdirection()
        {
            speedY *= (-1);
        }

        public void startMovement(int bWidth, int bHeight)
        { 
            Vector2 bSize = new Vector2(bWidth, bHeight);
            BallTimer = new Timer(move, bSize, 0, 16);
        }
        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

