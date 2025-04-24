using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataAPI : IDataAPI
    {
        public Ball ConstructBall(Vector2 position, float radius, Vector2 velocity)
        {
            return new Ball(position, radius, velocity);
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
