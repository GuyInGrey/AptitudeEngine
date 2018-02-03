using AptitudeEngine.Assets;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class SpriteRenderer : AptComponent
    {
        public SpriteAsset Sprite { get; set; }
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