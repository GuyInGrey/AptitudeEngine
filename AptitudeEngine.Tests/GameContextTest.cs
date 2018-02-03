using System;
using System.Drawing;
using AptitudeEngine.Assets;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Logger;
using AptitudeEngine.Events;
using AptitudeEngine.Enums;

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
            context.ClearColor = Color.CornflowerBlue;
            ScreenHandler.Blending(true);

            context.CustomCursorPath = @"./assets/cursor_small.png";
            context.CustomCursor = true;

            var camera = context.Instantiate().AddComponent<Camera>();
            camera.SetAsMain();
            camera.Owner.AddComponent<MoveController>();
            camera.Owner.Transform.Size = new Vector2(2, 2);
            camera.Owner.Transform.Position = new Vector2(0, 0);

            var destroyerObject = context.Instantiate();
            var renderer = destroyerObject.AddComponent<SpriteRenderer>();
            renderer.Sprite = Asset.Load<SpriteAsset>("./assets/starDestroyer.png");
            destroyerObject.Transform.Size = new Vector2(1f, 1f);
            var movement = destroyerObject.AddComponent<DestroyerMovement>();
        }

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

    public class DestroyerMovement : AptComponent
    {
        public float Speed { get; set; } = 0.001f;
        public float TurnSpeed { get; set; } = 0.5f;

        public InputCode ForwardCode { get; set; } = InputCode.Up;
        public InputCode LeftCode { get; set; } = InputCode.Left;
        public InputCode RightCode { get; set; } = InputCode.Right;

        public override void Render(FrameEventArgs a)
        {
            if (Input.GetKeyDown(ForwardCode))
            {
                Transform.Position = Transform.Position.Move(Transform.Rotation - 90, Speed);
            }
            if (Input.GetKeyDown(RightCode))
            {
                Transform.Rotation += TurnSpeed;
            }
            if (Input.GetKeyDown(LeftCode))
            {
                Transform.Rotation -= TurnSpeed;
            }
            if (Input.GetKeyDown(InputCode.Down))
            {
                Transform.Position = Transform.Position.Move(Transform.Rotation - 90, -Speed);
            }
        }
    }
}