using System;

using OpenTK;

using AptitudeEngine;
using AptitudeEngine.Assets;
using AptitudeEngine.Components;

namespace AptitudeEngine.Components
{
	public class SpriteRenderer : AptComponent
	{
        public SpriteAsset Sprite { get; set; }
		
		public override void Render(FrameEventArgs a)
        {
            var thisSize = new Rectangle(Owner.Position, Owner.Size);

			ScreenHandler.Tex(Sprite.Texture, thisSize, Sprite.Frame);
			if (Context.MainCamera.Owner.Bounds.IntersectsWith(thisSize))
            {
                
            }
        }
	}
}