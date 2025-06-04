using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CollisionEvent
    {
        public DateTime Time { get; set; }
        public BallData Ball1 { get; set; }
        public BallData Ball2 { get; set; }

        public CollisionEvent(Ball a, Ball b)
        {
            Time = DateTime.UtcNow;
            Ball1 = new BallData(a);
            Ball2 = new BallData(b);
        }

        public class BallData
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Radius { get; set; }
            public float Mass { get; set; }
            public float VelocityX { get; set; }
            public float VelocityY { get; set; }
            public string Color { get; set; }

            public BallData(Ball b)
            {
                X = b.X;
                Y = b.Y;
                Radius = b.Radius;
                Mass = b.Mass;
                VelocityX = b.Velocity.X;
                VelocityY = b.Velocity.Y;
                Color = ColorTranslator.ToHtml(b.Color);
            }
        }
    }
}
