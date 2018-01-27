﻿using System;
using System.Drawing;
using System.Collections.Generic;

using AptitudeEngine;
using AptitudeEngine.Assets;
using AptitudeEngine.Components;
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
			context = new AptContext("Test Context");
			context.Load += Context_Load;
            context.Begin();
		}

        private void Context_Load(object sender, EventArgs e)
        {
			context.ClearColor = Color.Fuchsia;

            var camera = context.Instantiate().AddComponent<Camera>();
            camera.SetAsMain();
            camera.Owner.AddComponent<MoveController>();
            camera.Owner.Transform.Size = new Vector2(2, 2);
            camera.Owner.Transform.Position = new Vector2(-0.5f, -0.5f);

            //var someSprite = context.Instantiate().AddComponent<SpriteRenderer>();
            //someSprite.Sprite = Asset.Load<SpriteAsset>("./assets/arrow.png");
            //someSprite.Transform.Position = new Vector2(-0.5f, -0.5f);

            var x = -0.25f;
            for (var i = 0; i < 500; i++)
            {
                x += 0.001f;
                var somePoly = context.Instantiate().AddComponent<PolyRenderer>();
                somePoly.Points = new PolyPoint[3] {
                new PolyPoint(new Vector2(x-0.25f,-0.25f), Color.Red),
                new PolyPoint(new Vector2(x+0.25f, -0.25f), Color.White),
                new PolyPoint(new Vector2(x, 0.25f), Color.Blue),
                };
            }
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
