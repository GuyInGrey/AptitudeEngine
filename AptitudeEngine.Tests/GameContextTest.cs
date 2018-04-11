using System;
using System.Drawing;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.Components.Flairs;

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
            context = new AptContext("Test Context", 1000, 1000, 60, 60);
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
            cameraObject.Transform.Scale = new Vector2(1, 1);
            var camera = cameraObject.AddComponent<Camera>();
            camera.SetAsActive();

            var GameOverFlair = context.Instantiate().AddComponent<Flair>();
            GameOverFlair.Transform.Position = new Vector2(0.025f);
            GameOverFlair.Transform.Scale = new Vector2(0.5f);
            GameOverFlair.AddText("Game Over!", 8).IndividualVisible = true;
        }
    }
}