using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public interface IBusinessLogic
    {
        public void CreateBalls(int amount);
        public ObservableCollection<Ball> GetBalls();
        public Ball CreateBall(Vector2 position, float radius, Vector2 velocity);
        public bool CheckBoundaryCollision(Ball ball, double canvasWidth, double canvasHeight);
        public void UpdateBall(Ball ball, double canvasWidth, double canvasHeight);
    }

    public interface IPhysicsLogic
    {
        Task StartAsync(CancellationToken token);
    }

}
