using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataAPI : IDataAPI
    {
        private Repository _repository = new();

        public Ball ConstructBall(Vector2 position, float radius, Vector2 velocity)
        {
            return new Ball(position, radius, velocity);
        }

        public void AddBallToRepository(Ball ball)
        {
            _repository.Balls.Add(ball);
        }

        public ObservableCollection<Ball> GetListOfBalls()
        {
            return _repository.Balls;
        }

        public void MoveBall(Ball ball)
        {
            ball.Position += ball.Velocity;
        }

        public void SqueezeBall(Ball ball, float radius)
        {
            ball.Radius = radius;
        }

    }
}
