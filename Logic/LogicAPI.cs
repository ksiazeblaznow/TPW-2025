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
    }

    public interface IPhysicsLogic
    {
        public bool CheckBoundaryCollision(Ball ball, float canvasWidth, float canvasHeight);
        public void UpdateBall(Ball ball, float canvasWidth, float canvasHeight);
        Task StartAsync(CancellationToken token);
    }

    public interface ILogicAPI
    {
        public PhysicsLogic GetPhysicsLogic(float canvasWidth, float canvasHeight);
        public BusinessLogic GetBusinessLogic(float canvasWidth, float canvasHeight);
    }

    public abstract class LogicDependencies
    {
        public static IDataAPI _dataAPI;
        public static float CanvasWidth;
        public static float CanvasHeight;

        protected LogicDependencies()
        {
            _dataAPI = new DataAPI();
        }
    }

}
