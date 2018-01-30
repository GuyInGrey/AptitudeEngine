﻿using System.Drawing;
using AptitudeEngine.Assets;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Flairs
{
    //https://github.com/PhoenixGameDevelopmentTeam/AptitudeEngine/issues/15
    public class Flair : AptComponent
    {
        public Flair()
        {
            ForeColor = DefaultForeColor;
            BackColor = DefaultBackColor;
        }

        public FlairCanvas GetCanvas() => GetCanvasLooper(owner);

        private FlairCanvas GetCanvasLooper(AptObject b)
        {
            if (b.Parent == null)
            {
                return null;
            }
            foreach (var ac in b.Parent.Components)
            {
                if (ac is FlairCanvas)
                {
                    return (FlairCanvas)ac;
                }
            }
            return GetCanvasLooper(b.Parent);
        }

        public static Color DefaultForeColor { get; set; } = Color.White;
        public static Color DefaultBackColor { get; set; } = Color.Black;
        public string Name { get; set; }
        public object Tag { get; set; }
        public Vector2 Position => Transform.Position;
        public Vector2 Size => Transform.Size;
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public AptRectangle Bounds => new AptRectangle(Position, Size);
        public SpriteAsset BackImage { get; set; }

        public override void Render(FrameEventArgs a)
        {
            ScreenHandler.Poly(new PolyPoint[]
            {
                new PolyPoint(Position + GetCanvas()?.Transform.Position, BackColor),
                new PolyPoint(new Vector2(Position.X + Size.X, Position.Y) + GetCanvas()?.Transform.Position, BackColor),
                new PolyPoint(Position + Size + GetCanvas()?.Transform.Position, BackColor),
                new PolyPoint(new Vector2(Position.X, Position.Y + Size.Y) + GetCanvas()?.Transform.Position, BackColor),
            });

            if (BackImage != null)
            {
                ScreenHandler.Tex(BackImage.Texture, Transform.Bounds, BackImage.Frame);
            }
        }
    }
}