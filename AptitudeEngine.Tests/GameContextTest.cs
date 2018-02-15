using System;
using System.Drawing;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using AptitudeEngine.Components.Pathing;

namespace AptitudeEngine.Tests
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            var test = new GameTest();
        }
    }

    public class GameTest
    {
        private AptContext context;

        public GameTest()
        {
            context = new AptContext("Test Context", 1000, 1000, 1000000, 1000000);
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

            var pathingObject = context.Instantiate();
            var pathFinder = pathingObject.AddComponent<PathFinder>();
            var turtle = pathingObject.AddComponent<Turtle>();

            pathFinder.GenerateNodes(20, -0.5f, -0.5f, 0.5f, 0.5f, 20);

            turtle.SetDebug(false);

            turtle.DrawCode = t =>
            {
                t.SetLineThickness(20);

                foreach (var n in pathFinder.Nodes)
                {
                    foreach (var m in n.ConnectedNodes)
                    {
                        ScreenHandler.Lines(new Vector2[] { n.Position, m.Position }, 2, Color.Black);
                    }

                    t.SetPosition(n.Position);
                    t.SetColor(Color.Red);
                    t.Circle(0.01f);
                }
            };
        }
    }
}