using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : IBall, INotifyPropertyChanged
    {
        private Vector2 _position;
        private float _radius;
        private Vector2 _velocity;

        public float X
        {
            get => _position.X;
            set
            {
                if (_position.X != value)
                {
                    _position.X = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Y
        {
            get => _position.Y;
            set
            {
                if (_position.Y != value)
                {
                    _position.Y = value;
                    OnPropertyChanged();
                }
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(X));
                OnPropertyChanged(nameof(Y));
            }
        }

        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                OnPropertyChanged(nameof(Radius));
            }
        }

        public Ball(Vector2 position, float radius, Vector2 velocity)
        {
            _position = position;
            _radius = radius;
            _velocity = velocity;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}