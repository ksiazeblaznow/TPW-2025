using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace LogicTests
{
    [TestClass]
    public class PhysicsLogicTests
    {
        private PhysicsLogic _physics;
        private float _canvasWidth = 500;
        private float _canvasHeight = 500;

        [TestInitialize]
        public void Setup()
        {
            _physics = new PhysicsLogic(_canvasWidth, _canvasHeight);
        }

        [TestMethod]
        public void CheckBoundaryCollision_ReturnsTrueWhenColliding()
        {
            Ball ball = new Ball(new Vector2(490, 250), 15, Vector2.Zero);
            bool collides = _physics.CheckBoundaryCollision(ball, _canvasWidth, _canvasHeight);
            Assert.IsTrue(collides);
        }

        [TestMethod]
        public void CheckBoundaryCollision_ReturnsFalseWhenNotColliding()
        {
            Ball ball = new Ball(new Vector2(250, 250), 10, Vector2.Zero);
            bool collides = _physics.CheckBoundaryCollision(ball, _canvasWidth, _canvasHeight);
            Assert.IsFalse(collides);
        }

        [TestMethod]
        public void HandleWallCollision_ReversesVelocityOnEdge()
        {
            Ball ball = new Ball(new Vector2(490, 10), 20, new Vector2(5, 5));
            Vector2 initialVelocity = ball.Velocity;

            _physics.HandleWallCollision(ball);

            Assert.AreEqual(-initialVelocity.X, ball.Velocity.X);
            Assert.AreEqual(initialVelocity.Y, ball.Velocity.Y);
        }

        [TestMethod]
        public async Task StartAsync_RunsWithoutException()
        {
            BusinessLogic logic = new BusinessLogic(500, 500);
            logic.CreateBalls(2); // Add balls to avoid empty loop

            var cts = new CancellationTokenSource();
            cts.CancelAfter(100); // Cancel after 100ms

            try
            {
                await _physics.StartAsync(cts.Token);
            }
            catch (TaskCanceledException)
            {
                // Expected due to CancelAfter(100)
            }

            Assert.IsTrue(true); // If it doesn't throw anything else, the test passes
        }

        [TestMethod]
        public void ResolveElasticCollision_ChangesVelocity()
        {
            Ball a = new Ball(new Vector2(0, 0), 10, new Vector2(2, 0));
            Ball b = new Ball(new Vector2(15, 0), 10, new Vector2(-2, 0));

            _physics.ResolveElasticCollision(
                a, b, Vector2.Normalize(b.Position - a.Position), 15f, 20f);

            Assert.AreNotEqual(2, a.Velocity.X);
            Assert.AreNotEqual(-2, b.Velocity.X);
        }
    }
}
