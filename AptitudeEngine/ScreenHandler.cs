using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace AptitudeEngine
{
    public static class ScreenHandler // The draw class is for all GL calls for rendering
    {
        /// <summary>
        /// Draws the specified selected points.
        /// </summary>
        /// <param name="SelectedPoints">The selected points.</param>
        public static void Poly(PolyPoint[] SelectedPoints)
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
                GL.Vertex2(SelectedPoints[i].Position.X, SelectedPoints[i].Position.Y);
            }

            GL.End();
        }

        public static void Tex(Texture2D tex, Rectangle window, Rectangle frame)
        {
            var posVectors = ConvertRectangle(window);
            var frameVectors = ConvertRectangle(frame);

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, tex.ID);
            GL.Color4(Color.Transparent);
            GL.Begin(PrimitiveType.Polygon);

            GL.TexCoord2(frameVectors[0].X, frameVectors[0].Y);
            GL.Vertex2(posVectors[0].X, posVectors[0].Y);

            GL.TexCoord2(frameVectors[1].X, frameVectors[1].Y);
            GL.Vertex2(posVectors[1].X, posVectors[1].Y);

            GL.TexCoord2(frameVectors[2].X, frameVectors[2].Y);
            GL.Vertex2(posVectors[2].X, posVectors[2].Y);

            GL.TexCoord2(frameVectors[3].X, frameVectors[3].Y);
            GL.Vertex2(posVectors[3].X, posVectors[3].Y);

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
            => Tex(tex, new Rectangle(x, y, width, height), new Rectangle(0, 0, 1, 1));

        /// <summary>
        /// Converts a rectangle into a Vector2 list
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector2[] ConvertRectangle(Rectangle r)
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