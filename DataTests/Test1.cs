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

            Ball ball = new Ball(position, radius);

            // Assert
            Assert.AreEqual(position, ball.Position);
            Assert.AreEqual(radius, ball.Radius);
        }
    }
}
