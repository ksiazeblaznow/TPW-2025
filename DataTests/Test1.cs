using Data;
namespace DataTests
{
    [TestClass]
    public class BallTest1
    {
        [TestMethod]
        public void BallCreationTest()
        {

            float x = 1.0f;
            float y = 2.0f;
            float radius = 2.5f;

            Ball ball = new Ball(x, y, radius);

            // Assert
            Assert.AreEqual(x, ball.PositionX);
            Assert.AreEqual(y, ball.PositionY);
            Assert.AreEqual(radius, ball.Radius);
        }
    }
}
