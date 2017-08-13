using System;
using System.Drawing;
using System.Collections.Generic;

using AptitudeEngine;
using AptitudeEngine.Assets;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;

using OpenTK.Graphics;
using OpenTK;

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
			context = new AptContext("Test Context", 750, 750, 60, 60, GraphicsMode.Default, VSyncMode.Off, GameWindowFlags.FixedWindow, DisplayDevice.Default);
			context.Load += Context_Load;
            context.Begin();
		}

        private void Context_Load(object sender, EventArgs e)
        {
			context.ClearColor = Color.Fuchsia;

			var camera = context.Instantiate().AddComponent<Camera>();
			camera.SetAsMain();
			camera.Owner.AddComponent<MoveController>();

			var someSprite = context.Instantiate().AddComponent<SpriteRenderer>();
			someSprite.Sprite = Asset.Load<SpriteAsset>("./assets/me.jpg");
			someSprite.Owner.Position = new Vector2(-0.5f, -0.5f);
		}

        public Rectangle Rec(float x, float y, float width, float height) =>
            new Rectangle(x, y, width, height);

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
