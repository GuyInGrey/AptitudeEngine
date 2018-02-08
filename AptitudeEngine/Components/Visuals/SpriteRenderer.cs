using AptitudeEngine.Assets;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class SpriteRenderer : AptComponent
    {
        /// <summary>
        /// The sprite being drawn.
        /// </summary>
        public SpriteAsset Sprite { get; set; }

        /// <summary>
        /// Whether the sprite will be drawn.
        /// </summary>
        public bool Drawing { get; set; } = true;

        public override void Render(FrameEventArgs a)
        {
            ScreenHandler.Flags = DrawFlags.ParentCoordinateRelative;
            if (Drawing && Sprite != null)
            {
                ScreenHandler.Texture(Sprite.Texture, Sprite.Frame);
            }
        }
    }
}