using System.Collections.ObjectModel;
using System.Numerics;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class DataApiTests
    {
        private DataAPI _dataAPI;

        [TestInitialize]
        public void Setup()
        {
            _dataAPI = new DataAPI();
        }

        [TestMethod]
        public void ConstructBall_CreatesBallWithCorrectProperties()
        {
            Vector2 position = new Vector2(2.0f, 3.0f);
            float radius = 1.5f;
            Vector2 velocity = new Vector2(1.0f, 1.0f);

            Ball ball = _dataAPI.ConstructBall(position, radius, velocity);

            Assert.AreEqual(position, ball.Position);
            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(velocity, ball.Velocity);
        }

        [TestMethod]
        public void AddBallToRepository_AddsBallSuccessfully()
        {
            Ball ball = _dataAPI.ConstructBall(new Vector2(1, 1), 1.0f, new Vector2(0.5f, 0.5f));
            _dataAPI.AddBallToRepository(ball);

            ObservableCollection<Ball> balls = _dataAPI.GetListOfBalls();

            Assert.IsTrue(balls.Contains(ball));
        }

        [TestMethod]
        public void GetListOfBalls_ReturnsRepositoryCollection()
        {
            var list = _dataAPI.GetListOfBalls();
            Assert.IsNotNull(list);
            Assert.IsInstanceOfType(list, typeof(ObservableCollection<Ball>));
        }

        [TestMethod]
        public void MoveBall_ChangesPositionByVelocity()
        {
            Ball ball = _dataAPI.ConstructBall(new Vector2(1, 1), 1.0f, new Vector2(2, 3));
            _dataAPI.MoveBall(ball);

            Assert.AreEqual(new Vector2(3, 4), ball.Position);
        }

        [TestMethod]
        public void SqueezeBall_UpdatesRadius()
        {
            Ball ball = _dataAPI.ConstructBall(new Vector2(0, 0), 2.0f, Vector2.Zero);
            _dataAPI.SqueezeBall(ball, 5.0f);

            Assert.AreEqual(5.0f, ball.Radius);
        }

        [TestMethod]
        public void ExtractRepositoryLock_ReturnsSameObjectReference()
        {
            object lock1 = _dataAPI.ExtractRepositoryLock();
            object lock2 = _dataAPI.ExtractRepositoryLock();

            Assert.IsNotNull(lock1);
            Assert.AreSame(lock1, lock2);
        }
    }
}
