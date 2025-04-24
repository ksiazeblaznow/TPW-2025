using System.Numerics;

namespace Data
{
    public class Ball : IBall
    {
        private Vector2 _position;
        private float _radius;

        public float X
        {
            get => _position.X;
            set => _position.X = value;
        }

        public float Y
        {
            get => _position.Y;
            set => _position.Y = value;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public float Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public Ball(Vector2 position, float radius)
        {
            _position = position;
            _radius = radius;
        }
    }
}