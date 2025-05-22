using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System.Numerics;
using System.Collections.ObjectModel;

namespace LogicTests
{
    [TestClass]
    public class BusinessLogicTests
    {
        private BusinessLogic _logic;

        [TestInitialize]
        public void Setup()
        {
            _logic = new BusinessLogic(500, 500);
        }

        [TestMethod]
        public void CreateBall_CreatesBallWithCorrectProperties()
        {
            Vector2 pos = new Vector2(10, 10);
            float radius = 5;
            Vector2 vel = new Vector2(2, 3);

            Ball ball = _logic.CreateBall(pos, radius, vel);

            Assert.AreEqual(pos, ball.Position);
            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(vel, ball.Velocity);
        }

        [TestMethod]
        public void CreateBalls_AddsFourBallsToRepository()
        {
            _logic.CreateBalls(4);

            ObservableCollection<Ball> balls = _logic.GetBalls();

            Assert.AreEqual(4, balls.Count);
        }

        [TestMethod]
        public void GetBalls_ReturnsObservableCollection()
        {
            var balls = _logic.GetBalls();
            Assert.IsInstanceOfType(balls, typeof(ObservableCollection<Ball>));
        }
    }
}
