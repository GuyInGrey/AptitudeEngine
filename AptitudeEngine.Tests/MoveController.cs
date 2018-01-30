using System;
using AptitudeEngine.Enums;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Tests
{
    public class MoveController : AptComponent
    {
        private Camera camera;

        public float Speed { get; set; } = 0.5f;

        public override void Start() => camera = Owner.GetComponentOfType<Camera>();

        public override void Update()
        {
            var up = Input.GetKeyDown(InputCode.W);
            var left = Input.GetKeyDown(InputCode.A);
            var down = Input.GetKeyDown(InputCode.S);
            var right = Input.GetKeyDown(InputCode.D);

            var speed = Vector2.Zero;
            if (up)
            {
                speed.Y += 1f;
            }

            if (down)
            {
                speed.Y -= 1f;
            }

            if (right)
            {
                speed.X += 1f;
            }

            if (left)
            {
                speed.X -= 1f;
            }

            if (Math.Abs(speed.SquareMagnitude) > float.Epsilon)
            {
                camera.Move(speed.Normalized * Speed * Context.DeltaTime);
            }
        }
    }
}