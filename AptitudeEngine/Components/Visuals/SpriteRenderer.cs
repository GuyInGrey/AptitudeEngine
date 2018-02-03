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
            if (Drawing)
            {
                ScreenHandler.Tex(Sprite.Texture, owner, Sprite.Frame);
            }
        }
    }
}