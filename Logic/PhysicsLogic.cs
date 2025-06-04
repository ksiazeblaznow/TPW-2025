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
                        ResolveElasticCollision(a, b, delta / dist, dist, minDist);
                    }
                }
            }
        }

        public void ResolveElasticCollision(Ball a, Ball b, Vector2 normal, float dist, float minDist)
        {
            // Relative velocity
            Vector2 relativeVelocity = b.Velocity - a.Velocity;

            // Velocity along the normal
            float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

            // If velocities are separating, no need to resolve
            if (velocityAlongNormal > 0)
                return;

            // Coefficient of restitution (1 for perfectly elastic collision)
            float restitution = 1.0f;

            // Calculate impulse scalar
            float impulseMagnitude = -(1 + restitution) * velocityAlongNormal;
            impulseMagnitude /= (1 / a.Mass) + (1 / b.Mass);

            // Apply impulse to the velocities
            Vector2 impulse = impulseMagnitude * normal;

            a.Velocity -= impulse / a.Mass;
            b.Velocity += impulse / b.Mass;

            // OPTIONAL: Positional correction to prevent sinking (overlapping)
            float percent = 0.8f; // usually 20% to 80%
            float slop = 0.01f;   // small tolerance

            Vector2 correction = MathF.Max(dist - minDist, 0) / ((1 / a.Mass) + (1 / b.Mass)) * percent * normal;
            a.Position -= correction / a.Mass;
            b.Position += correction / b.Mass;

            CollisionLogger.Instance.LogCollision(a, b);
        }
    }
}
