using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall
    {
        
    }

    public interface IDataAPI
    {
        public Ball ConstructBall(Vector2 position, float radius, Vector2 velocity);
        public void MoveBall(Ball ball);
        public void SqueezeBall(Ball ball, float radius);

        public void AddBallToRepository(Ball ball);

        public ObservableCollection<Ball> GetListOfBalls();
    }
}
