using AptitudeEngine.Assets;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class SpriteRenderer : AptComponent
    {
        public SpriteAsset Sprite { get; set; }

        public override void Render(FrameEventArgs a) =>
            ScreenHandler.Tex(Sprite.Texture, Transform.Bounds, Sprite.Frame);
    }
}