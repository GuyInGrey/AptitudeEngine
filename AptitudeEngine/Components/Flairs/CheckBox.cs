using System;
using System.Drawing;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Components.Flairs
{
    public class CheckBox : Flair
    {
        public override void Awake() => Click += CheckBox_Click;

        private void CheckBox_Click(object sender, Events.MouseButtonEventArgs e) => Checked = !Checked;

        public bool Checked { get; set; } = false;

        public Color CheckedColor { get; set; } = Color.DarkGray;

        public float CheckedMargin { get; set; } = 0.01f;

        public override void FlairRender()
        {
            base.FlairRender();
            
            if (Checked)
            {
                ScreenHandler.Flags = DrawFlags.ParentCoordinateRelative;
                ScreenHandler.Polygon(new PolyVector[] {
                    new PolyVector(Vector2.Zero + CheckedMargin, CheckedColor),
                    new PolyVector(new Vector2(Transform.Size.X - CheckedMargin, CheckedMargin), CheckedColor),
                    new PolyVector(new Vector2(Transform.Size - CheckedMargin), CheckedColor),
                    new PolyVector(new Vector2(CheckedMargin, Transform.Size.Y - CheckedMargin), CheckedColor),
                });
            }
        }
    }
}