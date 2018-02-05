using AptitudeEngine.Assets;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class ParallaxManager : AptComponent
    {
        public Vector2 Speed { get; set; } = new Vector2(0.001f,0);
        public Vector2 DistanceTraveled { get; private set; } = Vector2.Zero;
        public string Image { get; set; }

        public static readonly Vector2[] FinalPositions = new Vector2[] {
            new Vector2(-1, -1),
            new Vector2(0 , -1),
            new Vector2(1 , -1),
            new Vector2(-1,  0),
            new Vector2(0 ,  0),
            new Vector2(1 ,  0),
            new Vector2(-1,  1),
            new Vector2(0 ,  1),
            new Vector2(1 ,  1),
        };

        public override void Start()
        {
            if (Image == null)
            {
                return;
            }

            for (var i = 0; i < 9; i++)
            {
                var ao = Context.Instantiate();
                owner.AddChild(ao);
                ao.Name = "P" + i;
                ao.Transform.Size = new Vector2(1,1);
                ao.Transform.Position = FinalPositions[i];
                var renderer = ao.AddComponent<SpriteRenderer>();
                renderer.Sprite = Asset.Load<SpriteAsset>(Image);
            }
        }

        public override void Update()
        {
            for (var i = 0; i < 9; i++)
            {
                owner.Children[i].Transform.Position += Speed;
            }

            var resetX = false;
            var resetY = false;

            for (var m = 0; m < 9; m++)
            {
                if (DistanceTraveled.X >= 1)
                {
                    owner.Children[m].Transform.Position = new Vector2(FinalPositions[m].X, owner.Children[m].Transform.Position.Y);
                    resetX = true;
                }

                if (DistanceTraveled.Y >= 1)
                {
                    owner.Children[m].Transform.Position = new Vector2(owner.Children[m].Transform.Position.X, FinalPositions[m].Y);
                    resetY = true;
                }
            }

            if (resetX)
            {
                DistanceTraveled = new Vector2(0, DistanceTraveled.Y);
            }
            if (resetY)
            {
                DistanceTraveled = new Vector2(DistanceTraveled.X, 0);
            }

            DistanceTraveled += Speed;
        }

        public override void Render(FrameEventArgs a)
        =>
            ScreenHandler.Lines(new Vector2[] {
                Vector2.Zero,
                new Vector2(1,0),
                new Vector2(1,1),
                new Vector2(0,1),
                Vector2.Zero,
            }, 10f, System.Drawing.Color.Green, Owner);
    }
}