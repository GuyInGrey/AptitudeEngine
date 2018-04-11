using System;
using System.Drawing;

namespace AptitudeEngine.Components.Flairs
{
    public class Button : Flair
    {
        public override void Start() => Rounding = Transform.Scale.Y / 4;

        /// <summary>
        /// The color that that is drawn to the background of the object when a mouse button is down over the object.
        /// </summary>
        public Color MouseDownBackColor { get; set; } = Color.Gray;

        public float Rounding { get; set; }

        public override void FlairRender()
        {
            var oldColor = BackColor;

            if (MouseStateDown)
            {
                BackColor = MouseDownBackColor;
            }

            base.FlairRender();

            BackColor = oldColor;
        }
    }
}