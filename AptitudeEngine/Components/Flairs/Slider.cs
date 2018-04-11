using System.Drawing;
using System;

namespace AptitudeEngine.Components.Flairs
{
    public class Slider : Flair
    {
        public float BitSize { get; set; } = 0.01f;

        public float LineWidth { get; set; } = 0.01f;

        public Color BitColor { get; set; } = Color.DarkSlateGray;

        public float ProgressFloat { get; set; } = 50f;

        public int Progress => (int)Math.Round(ProgressFloat);

        public SliderMode Mode { get; set; } = SliderMode.Horizontal;

        public override void FlairRender()
        {
            if (MouseStateDown)
            {
                var b = Transform.Bounds;
                b.Position = owner.TotalPosition;

                if (b.ContainsVector(Context.Input.MouseWorldPosition))
                {
                    if (Mode == SliderMode.Horizontal)
                    {
                        ProgressFloat = (Context.Input.MouseWorldPositionObjectRelative.X / Transform.Scale.X).Clamp(0, 1);
                    }
                    if (Mode == SliderMode.Vertical)
                    {
                        ProgressFloat = (Context.Input.MouseWorldPositionObjectRelative.Y / Transform.Scale.Y).Clamp(0, 1);
                    }
                    ProgressFloat *= 100;
                }
            }

            base.FlairRender();

            ScreenHandler.Flags = DrawFlags.ParentCoordinateRelative | DrawFlags.CustomBounds;

            if (Mode == SliderMode.Horizontal)
            {
                ScreenHandler.Lines(new Vector2[] {
                    new Vector2(0, Transform.Scale.Y / 2),
                    new Vector2(Transform.Scale.X, Transform.Scale.Y / 2),
                }, 0.01f, Color.Black);

                ScreenHandler.Circle(new Vector2(Transform.Scale.X * (ProgressFloat/100), Transform.Scale.Y / 2), BitSize, BitColor);
            }
            else if (Mode == SliderMode.Vertical)
            {
                ScreenHandler.Lines(new Vector2[] {
                    new Vector2(Transform.Scale.X / 2, 0),
                    new Vector2(Transform.Scale.X / 2, Transform.Scale.Y),
                }, 0.01f, Color.Black);

                ScreenHandler.Circle(new Vector2(Transform.Scale.X / 2, Transform.Scale.Y * (ProgressFloat/100)), BitSize, BitColor);
            }
        }
    }

    public enum SliderMode
    {
        Horizontal,
        Vertical,
    }
}