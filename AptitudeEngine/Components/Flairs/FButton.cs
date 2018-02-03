using System;
using AptitudeEngine.Events;
using System.Drawing;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Components.Flairs
{
    public class FButton : AptComponent
    {
        /// <summary>
        /// Occurs when the object is clicked. A click is defined as Up and Down within 750 milliseconds.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Click;

        /// <summary>
        /// Occurs when the object has the mouse go up over it.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Up;

        /// <summary>
        /// Occurs when the object has the mouse go down over it.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Down;

        /// <summary>
        /// The color that is normally drawn to the background of the object.
        /// </summary>
        public Color BackColor { get; set; } = Color.AliceBlue;

        /// <summary>
        /// The color that is drawn to the background of the object when the mouse is down over the object.
        /// </summary>
        public Color MouseDownBackColor { get; set; } = Color.Gray;
        
        public override void Render(FrameEventArgs a)
        {
            var c = BackColor;

            if (MouseStateDown)
            {
                c = MouseDownBackColor;
            }

            ScreenHandler.Poly(new PolyVector[] {
                new PolyVector(Vector2.Zero, c),
                new PolyVector(new Vector2(Transform.Size.X, 0), c),
                new PolyVector(new Vector2(Transform.Size.X, Transform.Size.Y), c),
                new PolyVector(new Vector2(0, Transform.Size.Y), c),
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
