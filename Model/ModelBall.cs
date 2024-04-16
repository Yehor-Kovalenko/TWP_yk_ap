using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelBall : INotifyPropertyChanged
    {
        private int position_x;
        private int position_y;
        private int radius;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ModelBall(int position_x, int position_y, int radius)
        {
            this.position_x = position_x;
            this.position_y = position_y;
            this.radius = radius;
        }

        public int Position_X { get => position_x; set { position_x = value; NotifyPropertyChanged(nameof(position_x)); } }
        public int Position_Y { get => position_y; set { position_y = value; NotifyPropertyChanged(nameof(position_y)); } }
        public int Radius => radius;
        public void Update(Object s, PropertyChangedEventArgs e)
        {
            Ball ball = (Ball)s;
            switch (e.PropertyName)
            {
                case nameof(Position_X):
                    {
                        this.Position_X = ball.PosX;
                        break;
                    }

                case nameof(Position_Y):
                    {
                        this.Position_Y = ball.PosY;
                        break;
                    }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
