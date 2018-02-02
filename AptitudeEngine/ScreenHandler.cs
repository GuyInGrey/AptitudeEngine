using AptitudeEngine.CoordinateSystem;
using OpenTK.Graphics.OpenGL;

namespace AptitudeEngine
{
    public static class ScreenHandler // The ScreenHandler class is for all GL calls for rendering
    {
        public static bool Blend { get; private set; }
        /// <summary>
        /// Draws the specified selected points.
        /// </summary>
        /// <param name="SelectedPoints">The selected points.</param>
        public static void Poly(PolyPoint[] SelectedPoints, Transform f)
        {
            if (SelectedPoints == null)
            {
                return;
            }

            GL.Disable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Polygon);

            for (var i = 0; i < SelectedPoints.Length; i++)
            {
                GL.Color4(SelectedPoints[i].Color);
                GL.Vertex2(SelectedPoints[i].Position.X + f.Position.X, SelectedPoints[i].Position.Y + f.Position.Y);
            }

            GL.End();
        }

        public static void Tex(Texture2D tex, AptRectangle window, AptRectangle frame)
        {
            var posVectors = ConvertRectangle(window);
            var frameVectors = ConvertRectangle(frame);

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, tex.ID);
            GL.Color3(System.Drawing.Color.Transparent);
            GL.Begin(PrimitiveType.Quads);

            for (var i = 0; i < 4; i++)
            {
                GL.TexCoord2(frameVectors[i].X, frameVectors[i].Y);
                GL.Vertex2(posVectors[i].X, posVectors[i].Y);
            }

            GL.End();
        }

        /// <summary>
        /// Draws a texture based on a location and size
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void Tex(Texture2D tex, float x, float y, float width, float height)
            => Tex(tex, new AptRectangle(x, y, width, height), new AptRectangle(0, 0, 1, 1));

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