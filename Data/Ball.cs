using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : IBall, INotifyPropertyChanged
    {
        private Vector2 _position;
        private float _radius;
        private Vector2 _velocity;
        private float _mass;
        public Color Color { get; set; }

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
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(Y));
                }
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

        public float Mass
        {
            get => _mass;
            set => _mass = value;
        }

        public Ball(Vector2 position, float radius, Vector2 velocity)
        {
            _position = position;
            _radius = radius;
            _velocity = velocity;

            // Calculate mass based on radius
            float density = 11.34f;
            float volume = (4.0f / 3.0f) * (float)Math.PI * (float)Math.Pow(radius, 3.0f);
            _mass = density * volume;

            Color = GenerateRandomColor();
        }

        private Color GenerateRandomColor()
        {
            Random rand = new Random();
            return Color.FromArgb(255, rand.Next(256), rand.Next(256), rand.Next(256));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}