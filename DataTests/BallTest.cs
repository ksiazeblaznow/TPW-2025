using System;
using System.ComponentModel;
using System.Numerics;
using System.Drawing;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.AreEqual(position, ball.Position);
            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(velocity, ball.Velocity);

            float expectedVolume = (4f / 3f) * (float)Math.PI * (float)Math.Pow(radius, 3);
            float expectedMass = 11.34f * expectedVolume;
            Assert.AreEqual(expectedMass, ball.Mass, 0.01f);
        }

        [TestMethod]
        public void BallColor_IsRandomButValid()
        {
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, Vector2.Zero);
            Color color = ball.Color;

            Assert.IsTrue(color.R >= 0 && color.R <= 255);
            Assert.IsTrue(color.G >= 0 && color.G <= 255);
            Assert.IsTrue(color.B >= 0 && color.B <= 255);
            Assert.AreEqual(255, color.A);
        }

        [TestMethod]
        public void SettingX_RaisesPropertyChanged()
        {
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, Vector2.Zero);
            bool raised = false;

            ball.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "X")
                    raised = true;
            };

            ball.X = 10f;
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void SettingY_RaisesPropertyChanged()
        {
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, Vector2.Zero);
            bool raised = false;

            ball.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Y")
                    raised = true;
            };

            ball.Y = 20f;
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void SettingPosition_RaisesMultiplePropertyChanged()
        {
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, Vector2.Zero);
            int count = 0;

            ball.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Position" || e.PropertyName == "X" || e.PropertyName == "Y")
                    count++;
            };

            ball.Position = new Vector2(10, 20);
            Assert.IsTrue(count >= 3);
        }

        [TestMethod]
        public void SettingRadius_RaisesPropertyChanged()
        {
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, Vector2.Zero);
            bool raised = false;

            ball.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Radius")
                    raised = true;
            };

            ball.Radius = 3.0f;
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void Velocity_CanBeSetAndRetrieved()
        {
            Vector2 newVelocity = new Vector2(5.0f, 5.0f);
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, newVelocity);

            Assert.AreEqual(newVelocity, ball.Velocity);

            Vector2 updatedVelocity = new Vector2(2.0f, 3.0f);
            ball.Velocity = updatedVelocity;
            Assert.AreEqual(updatedVelocity, ball.Velocity);
        }

        [TestMethod]
        public void Mass_CanBeSetAndRetrieved()
        {
            Ball ball = new Ball(new Vector2(0, 0), 1.0f, Vector2.Zero);
            float newMass = 25.5f;
            ball.Mass = newMass;

            Assert.AreEqual(newMass, ball.Mass);
        }
    }
}
