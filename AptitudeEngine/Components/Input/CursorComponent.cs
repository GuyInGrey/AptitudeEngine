using System;
using AptitudeEngine.Events;
using AptitudeEngine.Assets;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Components.Input
{
    public class CursorComponent : AptComponent
    {
        public SpriteAsset CursorAsset { get; set; }

        public override void Render(FrameEventArgs a)
        {
            Transform.Position = Context.Input.MouseWorldPosition;
            if (CursorAsset != null)
            {
                ScreenHandler.Tex(CursorAsset.Texture, Transform.Bounds, new AptRectangle(0,0,1,1));
            }
        }
    }
}
