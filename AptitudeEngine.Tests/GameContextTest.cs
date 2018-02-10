using System;
using System.Drawing;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;

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
            var timer = cameraObject.AddComponent<Timer>();
            timer.IntervalInSeconds = 1f;
            timer.Tick += Timer_Tick;
            var tweener = cameraObject.AddComponent<Tweener>();
            tweener.SetPosition(new Vector2(f), TweenType.QuadraticInOut, ti);

            var parallaxObject = context.Instantiate();
            parallaxObject.Transform.Position = new Vector2(-0.5f, 0);
            var parallax = parallaxObject.AddComponent<ParallaxManager>();
            parallax.Image = "./assets/parallax_background.jpg";

            var TurtleObject = context.Instantiate();
            var turtle = TurtleObject.AddComponent<Turtle>();

            var waveObject = context.Instantiate();
            var wave = waveObject.AddComponent<WaveGenerator>();
            wave.Radius = 180f;
            wave.Frequency = 0.001f;

            var labelObject = context.Instantiate();

            turtle.DrawCode = t =>
            {
                t.SetLineThickness(2);
                var q = 0.001f;

                for (var i = 0; i < 200; i++)
                {
                    t.Move(q);
                    t.Turn(wave.ValueX);
                    q += 0.005f;
                }

                Console.Title = wave.ValueX.ToString("0") + " / 180";
            };
        }

        bool stage;
        float f = 0.25f;
        float ti = 2;

        private void Timer_Tick(object sender, TimerEventArgs e)
        {
            Tweener tweener;

            foreach (var a in e.Timer.Owner.Components)
            {
                if (a is Tweener)
                {
                    tweener = (Tweener)a;
                    goto a;
                }
            }

            return;

            a:;

            if (stage)
            {
                tweener.SetPosition(new Vector2(f), TweenType.QuadraticInOut, ti);
            }
            else
            {
                tweener.SetPosition(new Vector2(-f), TweenType.QuadraticInOut, ti);
            }

            stage = !stage;
        }
    }
}