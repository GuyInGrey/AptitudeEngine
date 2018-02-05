using AptitudeEngine.CoordinateSystem;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using AptitudeEngine.Logger;
using System.Collections.Generic;
using AptitudeEngine.Enums;

namespace AptitudeEngine
{
    public static class ScreenHandler // The ScreenHandler class is for all GL calls for rendering
    {
        /// <summary>
        /// Whether blending is enabled. To set, use <see cref="Blending(bool)"/>.
        /// </summary>
        public static bool Blend { get; private set; }

        public static List<DebugMode> DebugModes { get; set; } = new List<DebugMode>();

        /// <summary>
        /// Draws the specified selected points.
        /// </summary>
        /// <param name="SelectedPoints">The selected points.</param>
        public static void Poly(PolyVector[] SelectedPoints, AptObject o)
        {
            if (SelectedPoints == null)
            {
                return;
            }

            GL.Disable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Polygon);

            var toAdd = o.TotalPosition;

            for (var i = 0; i < SelectedPoints.Length; i++)
            {
                GL.Color4(SelectedPoints[i].Color);
                GL.Vertex2(SelectedPoints[i].Position.X + toAdd.X, SelectedPoints[i].Position.Y + toAdd.Y);
            }

            GL.End();
        }

        public static void Tex(Texture2D tex, AptObject o, AptRectangle frame)
        {
            var Position = o.TotalPosition;
            var posVectors = ConvertRectangle(new AptRectangle(Position, o.Transform.Size));

            for (var i = 0; i < posVectors.Length; i++)
            {
                posVectors[i] = posVectors[i].Rotate(new Vector2(o.Transform.Position.X + (o.Transform.Size.X / 2), o.Transform.Position.Y + (o.Transform.Size.Y / 2)), o.Transform.RotationRadians);
            }
            
            var frameVectors = ConvertRectangle(frame);

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, tex.ID);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);



            for (var i = 0; i < 4; i++)
            {
                GL.TexCoord2(frameVectors[i]);
                GL.Vertex2(posVectors[i]);
            }

            GL.End();

            if (DebugModes.Contains(DebugMode.BorderTextures))
            {
                ScreenHandler.Lines(new Vector2[] {
                    posVectors[0],
                    posVectors[1],
                    posVectors[2],
                    posVectors[3],
                    posVectors[0],
                }, 5f, Color.Orange);
            }
        }

        /// <summary>
        /// Draws a texture based on a location and size
        /// </summary>
        /// <param name="tex">The <see cref="Texture2D"/> to draw, usually off of a <see cref="Assets.SpriteAsset"/>.</param>
        /// <param name="t">The <see cref="Transform"/> to draw with.</param>
        public static void Tex(Texture2D tex, AptObject o)
            => Tex(tex, o, new AptRectangle(0, 0, 1, 1));

        public static void Lines(Vector2[] v, float thickness, Color c, AptObject o)
        {
            GL.Color3(c);
            GL.LineWidth(thickness);

            var toAdd = o.TotalPosition;

            GL.Begin(PrimitiveType.Lines);

            for (var i = 0; i < v.Length - 1; i++)
            {
                GL.Vertex2(v[i] + toAdd);
                GL.Vertex2(v[i + 1] + toAdd);
            }

            GL.End();
        }

        private static void Lines(Vector2[] v, float thickness, Color c)
        {
            GL.Color3(c);
            GL.LineWidth(thickness);

            GL.Disable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Lines);

            for (var i = 0; i < v.Length - 1; i++)
            {
                GL.Vertex2(v[i]);
                GL.Vertex2(v[i + 1]);
            }

            GL.End();
        }

        /// <summary>
        /// Converts a rectangle into a Vector2 list
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector2[] ConvertRectangle(AptRectangle r)
            => new[]
            {
                new Vector2(r.X, r.Y),
                new Vector2(r.X + r.Width, r.Y),
                new Vector2(r.X + r.Width, r.Y + r.Height),
                new Vector2(r.X, r.Y + r.Height)
            };

        /// <summary>
        /// Toggles Blending
        /// </summary>
        /// <param name="toggle"></param>
        public static void Blending(bool toggle)
        {
            Blend = toggle;
            if (toggle)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            }
            else
            {
                GL.Disable(EnableCap.Blend);
            }
        }
    }
}