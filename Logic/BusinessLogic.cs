using Data;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Logic
{
    public class BusinessLogic : LogicDependencies, IBusinessLogic
    {
        public BusinessLogic(float canvasWidth, float canvasHeight)
        {
            CanvasWidth = canvasWidth;
            CanvasHeight = canvasHeight;
        }

        public Ball CreateBall(Vector2 position, float radius, Vector2 velocity)
        {
            return _dataAPI.ConstructBall(position, radius, velocity);
        }

        public void CreateBalls(int amount)
        {
            // #todo: change it to for loop, randoms etc.

            Ball ball1 = CreateBall(new Vector2(50.0f, 200.0f), 20.0f,
                new Vector2(2.0f, 2.0f));
            Ball ball2 = CreateBall(new Vector2(100.0f, 50.0f), 15.0f,
                new Vector2(3.0f, 2.0f));
            Ball ball3 = CreateBall(new Vector2(200.0f, 300.0f), 10.0f,
                new Vector2(-1.0f, 0.0f));
            Ball ball4 = CreateBall(new Vector2(300.0f, 50.0f), 20.0f,
                new Vector2(1.0f, 1.4f));

            _dataAPI.AddBallToRepository(ball1);
            _dataAPI.AddBallToRepository(ball2);
            _dataAPI.AddBallToRepository(ball3);
            _dataAPI.AddBallToRepository(ball4);
        }

        public ObservableCollection<Ball> GetBalls()
        {
            return _dataAPI.GetListOfBalls();
        }        
    }
}
