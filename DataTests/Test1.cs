using System.Numerics;
using Data;
namespace DataTests
{
    [TestClass]
    public class BallTest1
    {
        [TestMethod]
        public void BallCreationTest()
        {
            Vector2 position = new Vector2(1.0f, 2.0f);
            float radius = 2.5f;
            Vector2 velocity = new Vector2(1.0f, 1.0f);

            Ball ball = new Ball(position, radius, velocity);

            // Assert
            Assert.AreEqual(position, ball.Position);
            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(velocity, ball.Velocity);
        }
    }
}
