using System;
using AptitudeEngine.Events;
using System.Drawing;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Components.Flairs
{
    public class FButton : AptComponent
    {
        public event EventHandler<MouseButtonEventArgs> Click;
        public event EventHandler<MouseButtonEventArgs> Up;
        public event EventHandler<MouseButtonEventArgs> Down;

        public Color BackColor { get; set; } = Color.AliceBlue;
        public Color MouseDownBackColor { get; set; } = Color.Gray;

        public override void Render(FrameEventArgs a)
        {
            var c = BackColor;

            if (MouseStateDown)
            {
                c = MouseDownBackColor;
            }

            ScreenHandler.Poly(new PolyPoint[] {
                new PolyPoint(Vector2.Zero, c),
                new PolyPoint(new Vector2(Transform.Size.X, 0), c),
                new PolyPoint(new Vector2(Transform.Size.X, Transform.Size.Y), c),
                new PolyPoint(new Vector2(0, Transform.Size.Y), c),
            }, owner);

            ScreenHandler.Lines(new Vector2[] {
                Vector2.Zero,
                new Vector2(Transform.Size.X, 0),
                new Vector2(Transform.Size.X, Transform.Size.Y),
                new Vector2(0, Transform.Size.Y),
                Vector2.Zero,
            }, 2f, Color.Black, owner);
        }

        public override void MouseClick(MouseButtonEventArgs e) => Click?.Invoke(this, e);
        public override void MouseDown(MouseButtonEventArgs e) => Down?.Invoke(this, e);
        public override void MouseUp(MouseButtonEventArgs e) => Up?.Invoke(this, e);
    }
}
