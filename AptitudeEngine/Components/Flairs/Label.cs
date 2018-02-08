using AptitudeEngine.Assets;
using AptitudeEngine.CoordinateSystem;
using System;
using System.Drawing;

namespace AptitudeEngine.Components.Flairs
{
    public class Label : Flair
    {
        private SpriteAsset textAsset;

        public float FontSize { get; set; } = 12f;

        public Color TextColor { get; set; } = Color.Black;
    
        public string Text { get; set; }

        public override void FlairRender()
        {
            base.FlairRender();

            if (textAsset != null)
            {
                ScreenHandler.Flags = DrawFlags.CustomBounds | DrawFlags.ParentCoordinateRelative;
                var size = new Vector2(Text.Length * (FontSize / 200), Text.LineCount() * (FontSize / 200) * (float)1.5);
                ScreenHandler.CustomBounds = new AptRectangle((Transform.Size - size) / 2, size);
                ScreenHandler.Texture(textAsset.Texture);
            }
        }

        public void Gen() => textAsset = Asset.LoadText(Text, TextColor, 1000);
    }
}