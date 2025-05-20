using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PhysicsLogic : LogicDependencies, IPhysicsLogic
    {
        public PhysicsLogic(float canvasWidth, float canvasHeight)
        {
            CanvasWidth = canvasWidth;
            CanvasHeight = canvasHeight;
        }

        public bool CheckBoundaryCollision(Ball ball, float canvasWidth, float canvasHeight)
        {
            if ((ball.Position.X + ball.Radius >= canvasWidth) ||
                (ball.Position.X - ball.Radius <= 0) ||
                (ball.Position.Y + ball.Radius >= canvasHeight) ||
                (ball.Position.Y - ball.Radius <= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateBall(Ball ball, float canvasWidth, float canvasHeight)
        {
            _dataAPI.MoveBall(ball);
        }

        public async Task StartAsync(CancellationToken token)
        {
            //throw new NotImplementedException();
            while (!token.IsCancellationRequested)
            {
                lock (_dataAPI.ExtractRepositoryLock())
                {
                    foreach (Ball ball in _dataAPI.GetListOfBalls())
                    {
                        UpdateBall(ball, CanvasWidth, CanvasHeight);

                        HandleWallCollision(ball);
                    }

                    HandleBallCollisions();
                }
                   
                await Task.Delay(TimeSpan.FromMilliseconds(16), token);  // Odczekaj przed kolejnym krokiem symulacji
            }
        }

        public void HandleWallCollision(Ball ball)
        {
            // Przesuń punkt odniesienia z lewego górnego rogu do środka kulki
            Vector2 center = ball.Position + new Vector2(ball.Radius, ball.Radius);

            // Odbicie od lewej i prawej ściany
            if (center.X - ball.Radius < 0 || center.X + ball.Radius > CanvasWidth)
            {
                ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
            }

            // Odbicie od górnej i dolnej ściany
            if (center.Y - ball.Radius < 0 || center.Y + ball.Radius > CanvasHeight)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }
        }

        public void HandleBallCollisions()
        {
            ObservableCollection<Ball> balls = _dataAPI.GetListOfBalls();

            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    var a = balls[i];
                    var b = balls[j];

                    Vector2 a_position = a.Position + new Vector2(a.Radius, a.Radius);
                    Vector2 b_position = b.Position + new Vector2(b.Radius, b.Radius);

                    var delta = b_position - a_position;

                    float dist = delta.Length();
                    float minDist = a.Radius + b.Radius;

                    if (dist < minDist)
                    {
                        ResolveElasticCollision(a, b, delta / dist);
                    }
                }
            }
        }

        public void ResolveElasticCollision(Ball a, Ball b, Vector2 normal)
        {
            // Calculate the vector between centers
            var delta = b.Position - a.Position;
            float dist = delta.Length();

            // Handle the case of exact overlap (avoid division by zero)
            if (dist == 0f)
            {
                // Use relative velocity direction or arbitrary unit vector
                var rv = a.Velocity - b.Velocity;
                if (rv.LengthSquared() > 0f)
                {
                    normal = Vector2.Normalize(rv);
                }
                else
                {
                    normal = Vector2.UnitX;
                }
                dist = a.Radius + b.Radius;
            }

            // Compute relative velocity and its component along the normal
            var relativeVelocity = a.Velocity - b.Velocity;
            float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

            // Do not resolve if velocities are separating
            if (velocityAlongNormal > 0f) return;

            // Calculate impulse scalar for perfectly elastic collision
            float restitution = 1.0f;
            float invMassSum = 1 / a.Mass + 1 / b.Mass;
            float impulseScalar = -(1 + restitution) * velocityAlongNormal / invMassSum;

            Vector2 impulse = impulseScalar * normal;
            a.Velocity += impulse / a.Mass;
            b.Velocity -= impulse / b.Mass;

            // --- POSITION CORRECTION ---
            // Prevent sinking by projecting them apart
            const float percent = 0.8f; // usually 20% to 80%
            const float slop = 0.01f;   // typically small, e.g., 0.01
            float penetration = (a.Radius + b.Radius) - dist;

            if (penetration > slop)
            {
                Vector2 correction = normal * (penetration - slop) / invMassSum * percent;
                a.Position -= correction * (1 / a.Mass);
                b.Position += correction * (1 / b.Mass);
            }
        }
    }
}
