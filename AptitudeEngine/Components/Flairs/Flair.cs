using System;
using System.Drawing;
using AptitudeEngine.Assets;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;
using AptitudeEngine.Logger;

namespace AptitudeEngine.Components.Flairs
{
    public class Flair : AptComponent
    {
        #region Events

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

        #endregion

        /// <summary>
        /// Whether this Flair and all it's parent flairs are visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                var toCheck = owner.Parent;

                while (toCheck != null)
                {
                    foreach (var ac in toCheck.Components)
                    {
                        if (ac is Flair)
                        {
                            if (!((Flair)ac).IndividualVisible)
                            {
                                return false;
                            }
                        }
                    }

                    toCheck = toCheck.Parent;
                }

                return true;
            }
        }

        /// <summary>
        /// Whether the flair and it's sub-flairs are drawn.
        /// </summary>
        public bool IndividualVisible { get; set; } = true;

        /// <summary>
        /// Background image.
        /// </summary>
        public SpriteAsset BackImage { get; set; }

        /// <summary>
        /// The color that is normally drawn to the background of the object.
        /// </summary>
        public Color BackColor { get; set; } = Color.AliceBlue;

        /// <summary>
        /// The width of the border around the Flair.
        /// </summary>
        public float BorderWidth { get; set; } = 2f;

        /// <summary>
        /// Whether there is a border around the Flair.
        /// </summary>
        public bool Border { get; set; } = true;

        /// <summary>
        /// The color that is drawn as the outline of the flair.
        /// </summary>
        public Color BorderColor { get; set; } = Color.Black;

        public override void Render(FrameEventArgs a)
        {
            if (Visible && IndividualVisible)
            {
                FlairRender();
            }
        }

        public bool Background { get; set; } = true;

        /// <summary>
        /// Overridable render method for flairs.
        /// </summary>
        public virtual void FlairRender()
        {
            if (Background)
            {
                ScreenHandler.Flags = DrawFlags.ParentCoordinateRelative;

                var c = BackColor;

                ScreenHandler.Polygon(new PolyVector[] {
                    new PolyVector(Vector2.Zero, c),
                    new PolyVector(new Vector2(Transform.Size.X, 0), c),
                    new PolyVector(new Vector2(Transform.Size.X, Transform.Size.Y), c),
                    new PolyVector(new Vector2(0, Transform.Size.Y), c),
                });

                if (BackImage != null)
                {
                    ScreenHandler.Texture(BackImage.Texture);
                }

                if (Border)
                {
                    ScreenHandler.Lines(new Vector2[] {
                        Vector2.Zero,
                        new Vector2(Transform.Size.X, 0),
                        new Vector2(Transform.Size.X, Transform.Size.Y),
                        new Vector2(0, Transform.Size.Y),
                        Vector2.Zero,
                    }, BorderWidth, BorderColor);
                }
            }
        }

        public Label AddText(string text, float fontSize)
        {
            var l = owner.AddComponent<Label>();
            l.Background = false;
            l.FontSize = fontSize;
            l.Text = text;
            l.Gen();
            return l;
        }

        #region EventCalling

        public override void MouseClick(MouseButtonEventArgs e)
        {
            if (Visible)
            {
                Click?.Invoke(this, e);
            }
        }

        public override void MouseDown(MouseButtonEventArgs e)
        {
            if (Visible)
            {
                Down?.Invoke(this, e);
            }
        }

        public override void MouseUp(MouseButtonEventArgs e)
        {
            if (Visible)
            {
                Up?.Invoke(this, e);
            }
        }

        #endregion
    }
}