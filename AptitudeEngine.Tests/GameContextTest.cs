using System;
using System.Drawing;
using AptitudeEngine.Assets;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.Components.Flairs;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Logger;
using AptitudeEngine.Events;

namespace AptitudeEngine.Tests
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            var test = new GameTest();
            test.GameTestStart();
        }
    }

    public class GameTest : IDisposable
    {
        private bool disposed;

        private AptContext context;

        public void GameTestStart()
        {
            context = new AptContext("Test Context", 600, 600);
            context.Load += Context_Load;
            context.Begin();
        }

        private void Context_Load(object sender, EventArgs e)
        {
            context.ClearColor = Color.Fuchsia;
            ScreenHandler.Blending(true);

            context.CustomCursorPath = @"./assets/cursor_small.png";
            context.CustomCursor = true;

            var camera = context.Instantiate().AddComponent<Camera>();
            camera.SetAsMain();
            camera.Owner.AddComponent<MoveController>();
            camera.Owner.Transform.Size = new Vector2(2, 2);
            camera.Owner.Transform.Position = new Vector2(0, 0);
            camera.ArrowMovement = true;

            var someSprite = context.Instantiate().AddComponent<SpriteRenderer>();
            someSprite.Sprite = Asset.Load<SpriteAsset>("./assets/testingImage.png");
            someSprite.Transform.Position = new Vector2(-0.5f, -0.5f);

            var somePoly = context.Instantiate().AddComponent<PolyRenderer>();
            somePoly.Points = new PolyPoint[3]
            {
                new PolyPoint(new Vector2(-0.5f, -0.5f), Color.FromArgb(0,255,0,0)),
                new PolyPoint(new Vector2(0.5f, -0.5f), Color.FromArgb(128,255,255,255)),
                new PolyPoint(new Vector2(0, 0.5f), Color.FromArgb(128,0,0,255)),
            };
            somePoly.Transform.Size = new Vector2(0f, 0f);

            var someCanvas = context.Instantiate().AddComponent<FlairCanvas>();
            var someFlair = context.Instantiate().AddComponent<Flair>();
            someFlair.Transform.Size = new Vector2(0.25f, 0.25f);
            someFlair.Owner.SetParent(someCanvas.Owner);
            someFlair.FMouseClick += ButtonPressed;
            someFlair.DefaultDraw = false;

            var flairSpriteRenderer = someFlair.Owner.AddComponent<SpriteRenderer>();
            flairSpriteRenderer.Sprite = Asset.Load<SpriteAsset>("./assets/deer.png");
            var flairSpriteAnimator = someFlair.Owner.AddComponent<SpriteAnimator>();
            var animation = Animation.EasyMake(4, 2, 1f, 1f, 8);
            flairSpriteAnimator.Animation = animation;

            var ctc = somePoly.Owner.AddComponent<CustomTestingComponent>();

            for (var i = 0; i < 7; i++)
            {
                LoggingHandler.Log("TESTING COLORING: " + (LogMessageType)i, (LogMessageType)i);
            }
        }

        public void ButtonPressed(object sender, MouseButtonEventArgs e) => LoggingHandler.Log("Button Click!", LogMessageType.Fine);

        public AptRectangle Rec(float x, float y, float width, float height) =>
            new AptRectangle(x, y, width, height);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            context = null;

            disposed = true;
        }
    }

    public class CustomTestingComponent : AptComponent
    {
        public override void MouseDown(MouseButtonEventArgs mouseCode) =>
            LoggingHandler.Log("CustomTestingComponent: MouseDown, Button " + mouseCode.Key, LogMessageType.Info);

        public override void MouseUp(MouseButtonEventArgs mouseCode) =>
            LoggingHandler.Log("CustomTestingComponent: MouseUp, Button " + mouseCode.Key, LogMessageType.Info);

        public override void MouseClick(MouseButtonEventArgs mouseCode) =>
            LoggingHandler.Log("CustomTestingComponent: MouseClick, Button " + mouseCode.Key, LogMessageType.Info);
    }
}