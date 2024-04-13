using System;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace Logic
{
    public class Ball
    {

        int posX;
        int posY;
        int speedX;
        int speedY;
        int radius;

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


        public Ball(int pos_X, int pos_Y, int speed_X, int speed_Y, int rad)
        {
            posX = pos_X;
            posY = pos_Y;
            speedX = speed_X;
            speedY = speed_Y;
            radius = rad;
        }

        public void move(int bWidth, int bHeight)
        {
            if (posX + speedX >= bWidth - radius || posX + speedX <= radius)
            {
                changeXdirection();
            }
            if (posY + speedY >= bHeight - radius || posY + speedY <= radius)
            {
                changeYdirection();
            }
            posX += speedX;
            posY += speedY;
        }

        void changeXdirection()
        {
            speedX *= -1;
        }

        void changeYdirection()
        {
            speedY *= -1;
        }

    }
}

