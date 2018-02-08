using System;
using System.Drawing;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Components;

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
            context = new AptContext("Test Context", 1000, 1000, 1000000,1000000);
            context.Load += Context_Load;
            context.Begin();
        }

        private void Context_Load(object sender, EventArgs e)
        {
            context.ClearColor = Color.CornflowerBlue;
            ScreenHandler.Blending = true;

            context.CustomCursorPath = @"./assets/cursor_small.png";
            context.CustomCursor = true;
            
            var cameraObject = context.Instantiate();
            cameraObject.Transform.Size = new Vector2(1, 1);
            var camera = cameraObject.AddComponent<Camera>();
            camera.SetAsActive();
            camera.SetPosition(new Vector2(0, 0));

            var TurtleObject = context.Instantiate();
            var turtle = TurtleObject.AddComponent<Turtle>();

            var waveObject = context.Instantiate();
            waveObject.Transform.Position = new Vector2(0.5f, 0);
            var wave = waveObject.AddComponent<WaveGenerator>();
            wave.Radius = 0.1f;

            turtle.DrawCode = t =>
            {
                t.SetLineThickness(2);
                var q = 0.001f;

                for (var i = 0; i < 100; i++)
                {
                    t.Move(q);
                    t.Turn(110);
                    q += 0.005f;
                }
            };
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
}