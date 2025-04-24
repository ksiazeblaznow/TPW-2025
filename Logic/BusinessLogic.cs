using Data;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Logic
{
    public class BusinessLogic : IBusinessLogic
    {
        private IDataAPI _dataAPI;

        public BusinessLogic()
        {
            _dataAPI = new DataAPI();
        }

        public bool CheckBoundaryCollision(Ball ball, double canvasWidth, double canvasHeight)
        {
            if ((ball.Position.X + ball.Radius >= canvasWidth) || 
                (ball.Position.X - ball.Radius <= 0) ||
                (ball.Position.Y + ball.Radius >= canvasHeight) ||
                (ball.Position.Y - ball.Radius <= 0)) 
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public Ball CreateBall(Vector2 position, float radius, Vector2 velocity)
        {
            return _dataAPI.ConstructBall(position, radius, velocity);
        }

        public void UpdateBall(Ball ball, double canvasWidth, double canvasHeight)
        {
            bool collision = CheckBoundaryCollision(ball, canvasWidth, canvasHeight);
            _dataAPI.MoveBall(ball);
            
            if (collision)
            {
                _dataAPI.SqueezeBall(ball, 0.0f);
            }
        }
    }
}
