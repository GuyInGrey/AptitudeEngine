using System;
using System.Drawing;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Turtle
{
    public class Turtle : AptComponent
    {
        public Action<Turtle> DrawCode;
        private Color currentColor = Color.Black;
        private float thickness = 5;
        private bool debug = true;

        public void SetDebug(bool b) => debug = b;

        public void SetColor(Color c) => currentColor = c;

        public void SetLineThickness(float f) => thickness = f;

        public void Jump(float distance) => Transform.Position = NewPos(distance);

        public void Turn(float degrees) => Transform.Rotation += degrees;

        public void Move(float distance)
        {
            ScreenHandler.Lines(new Vector2[] { Transform.Position, NewPos(distance) }, thickness, currentColor);
            Transform.Position = NewPos(distance);
        }

        public void SetPosition(Vector2 v) => Transform.Position = v;

        public void SetRotation(float f) => Transform.Rotation = f;

        private Vector2 NewPos(float distance) => (new Vector2(Transform.Position.X, Transform.Position.Y - distance)).Rotate(Transform.Position, Transform.RotationRadians);

        public override void Render(FrameEventArgs a)
        {
            ScreenHandler.Flags = DrawFlags.None;

            thickness = 5;
            currentColor = Color.Black;
            Transform.Rotation = 90;
            Transform.Position = Vector2.Zero;
            DrawCode(this);

            if (debug)
            {
                ScreenHandler.Circle(Vector2.Zero, 0.005f, Color.Green);
                ScreenHandler.Circle(Transform.Position, 0.005f, Color.Red);
            }
        }
    }
}