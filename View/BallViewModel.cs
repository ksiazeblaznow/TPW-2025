using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Data;

namespace Presentation
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private readonly Ball _ball;

        public BallViewModel(Ball ball)
        {
            _ball = ball;
            _ball.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Ball.Position))
                {
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(Y));
                }
                else if (e.PropertyName == nameof(Ball.Radius))
                {
                    OnPropertyChanged(nameof(Radius));
                }
            };
        }

        public float X => _ball.Position.X;
        public float Y => _ball.Position.Y;
        public float Radius => _ball.Radius;

        public SolidColorBrush Brush =>
            new SolidColorBrush(Color.FromArgb(_ball.Color.A, _ball.Color.R, _ball.Color.G, _ball.Color.B));


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
