using Data;
using System.Collections.ObjectModel;
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

        public void CreateBalls(int amount)
        {
            // #todo: change it to for loop, randoms etc.

            Ball ball1 = CreateBall(new Vector2(50.0f, 200.0f), 40.0f,
                new Vector2(2.0f, 2.0f));
            Ball ball2 = CreateBall(new Vector2(100.0f, 50.0f), 30.0f,
                new Vector2(3.0f, 2.0f));
            Ball ball3 = CreateBall(new Vector2(200.0f, 300.0f), 30.0f,
                new Vector2(-1.0f, 0.0f));

            _dataAPI.AddBallToRepository(ball1);
            _dataAPI.AddBallToRepository(ball2);
            _dataAPI.AddBallToRepository(ball3);
        }

        public ObservableCollection<Ball> GetBalls()
        {
            return _dataAPI.GetListOfBalls();
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
