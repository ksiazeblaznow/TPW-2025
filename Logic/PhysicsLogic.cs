using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PhysicsLogic : LogicDependencies, IPhysicsLogic
    {
        public PhysicsLogic(float canvasWidth, float canvasHeight)
        {
            CanvasWidth = canvasWidth;
            CanvasHeight = canvasHeight;
        }

        public bool CheckBoundaryCollision(Ball ball, float canvasWidth, float canvasHeight)
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

        public void UpdateBall(Ball ball, float canvasWidth, float canvasHeight)
        {
            bool collision = CheckBoundaryCollision(ball, canvasWidth, canvasHeight);

            _dataAPI.MoveBall(ball);

            if (collision)
            {
                _dataAPI.SqueezeBall(ball, 0.0f);
            }
        }

        public async Task StartAsync(CancellationToken token)
        {
            //throw new NotImplementedException();
            while (!token.IsCancellationRequested)
            {
                lock(_dataAPI.ExtractRepositoryLock())
                {
                    foreach (Ball ball in _dataAPI.GetListOfBalls())
                    {
                        UpdateBall(ball, CanvasWidth, CanvasHeight);
                        Console.WriteLine(ball.Position);
                    }

                    //Console.WriteLine("physics async");
                }
                await Task.Delay(TimeSpan.FromMilliseconds(16), token);  // Odczekaj przed kolejnym krokiem symulacji
            }
        }
    }
}
