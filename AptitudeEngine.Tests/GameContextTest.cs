using System;
using System.Drawing;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.Components.Flairs;
using AptitudeEngine.Components;

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

        Flair f;
        Label t;

        private void Context_Load(object sender, EventArgs e)
        {
            context.ClearColor = Color.White;
            ScreenHandler.Blending = true;

            context.CustomCursorPath = @"./assets/cursor_small.png";
            context.CustomCursor = true;

            var cameraObject = context.Instantiate();
            cameraObject.Transform.Scale = new Vector2(1, 1);
            var camera = cameraObject.AddComponent<Camera>();
            camera.SetAsActive();

            f = context.Instantiate().AddComponent<Flair>();
            f.Transform.Position = new Vector2(-0.5f);
            f.Transform.Scale = new Vector2(1f);

            var theTimer = context.Instantiate().AddComponent<Timer>();
            theTimer.IntervalInSeconds = 4f;
            theTimer.Tick += TheTimer_Tick;

            t = f.Owner.AddComponent<Label>();
            
            TheTimer_Tick(null, null);
        }

        int currentIndex = 0;

        private void TheTimer_Tick(object sender, Events.TimerEventArgs e)
        {
            f.BackColor = Colors[currentIndex];
            
            t.Background = false;
            t.FontSize = 5f;
            t.Text = Colors[currentIndex].ToString();
            t.Gen();
            
            currentIndex++;
            if (currentIndex >= Colors.Length)
            {
                currentIndex = 0;
            }
        }

        public static Color[] Colors = {
            Color.Teal,
            Color.FromArgb(0,110,122),
            Color.FromArgb(0,128,135),
        };
    }
}