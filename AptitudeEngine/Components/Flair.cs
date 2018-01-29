using System;
using System.Drawing;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components
{
    //https://github.com/PhoenixGameDevelopmentTeam/AptitudeEngine/issues/15
    public class Flair : AptComponent
    {
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
        public static Color DefaultBackColor { get; set; } = Color.FromArgb(128,0,0,0);
        public string Name { get; set; }
        public object Tag { get; set; }
        public Vector2 Position => Transform.Position;
        public Vector2 Size => Transform.Size;
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Rectangle Bounds => new Rectangle(Position, Size);

        public override void Render(FrameEventArgs a)
        {
            var fc = GetCanvas();

            if (fc == null)
            {
                ScreenHandler.Poly(new PolyPoint[]
{
                new PolyPoint(Position + fc.Transform.Position, BackColor),
                new PolyPoint(new Vector2(Position.X + Size.X, Position.Y) + fc.Transform.Position, BackColor),
                new PolyPoint(Position + Size + fc.Transform.Position, BackColor),
                new PolyPoint(new Vector2(Position.X, Position.Y + Size.Y) + fc.Transform.Position, BackColor),
                });
            }
            else
            {
                ScreenHandler.Poly(new PolyPoint[]
{
                new PolyPoint(Position, BackColor),
                new PolyPoint(new Vector2(Position.X + Size.X, Position.Y), BackColor),
                new PolyPoint(Position + Size, BackColor),
                new PolyPoint(new Vector2(Position.X, Position.Y + Size.Y), BackColor),
                });
            }

            return;
        }
    }
}
