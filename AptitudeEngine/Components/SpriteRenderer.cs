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
            var thisSize = new Rectangle(Transform.Position, Transform.Size);

            if (Context.MainCamera.Transform.Bounds.IntersectsWith(thisSize))
            {
                ScreenHandler.Tex(Sprite.Texture, thisSize, Sprite.Frame);
            }
        }
    }
}