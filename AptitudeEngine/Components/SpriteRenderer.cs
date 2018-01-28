using System;
using AptitudeEngine;
using AptitudeEngine.Assets;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using AptitudeEngine.Logging;

namespace AptitudeEngine.Components
{
    public class SpriteRenderer : AptComponent
    {
        public SpriteAsset Sprite { get; set; }

        public override void Render(FrameEventArgs a)
        {
            var thisWindow = new Rectangle(Transform.Position, Transform.Size);

            ScreenHandler.Tex(Sprite.Texture, thisWindow, Sprite.Frame);
        }
    }
}