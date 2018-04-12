using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System;

namespace AptitudeEngine
{
    public static class ScreenHandler // The ScreenHandler class is for all GL calls for rendering
    {
        private static bool _Blending = false;

        /// <summary>
        /// Whether transparency works.
        /// </summary>
        /// <param name="toggle"></param>
        public static bool Blending
        {
            set
            {
                _Blending = value;
                if (value)
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                }
                else
                {
                    GL.Disable(EnableCap.Blend);
                }
            }
            get => Blending;
        }

        public static AptObject CurrentDrawingObject { get; set; }

        public static Rectangle CustomBounds { get; set; }

        public static DrawFlags Flags { get; set; }

        public static bool Option(DrawFlags d) => (Flags & d) == d;

        public static Rectangle DefaultFrame { get; set; } = Rectangle.One;

        /// <summary>
        /// Draws the specified selected points.
        /// </summary>
        /// <param name="SelectedPoints">The selected points.</param>
        public static void Polygon(PolyVector[] CustomVertices)
        {
            if (CustomVertices == null)
            {
                return;
            }

            GL.Disable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Polygon);

            var toAdd = Vector2.Zero;

            if (Option(DrawFlags.ParentCoordinateRelative))
            {
                toAdd = CurrentDrawingObject.TotalPosition;
            }

            for (var i = 0; i < CustomVertices.Length; i++)
            {
                GL.Color4(CustomVertices[i].Color);
                GL.Vertex2(CustomVertices[i].Position.X + toAdd.X, CustomVertices[i].Position.Y + toAdd.Y);
            }

            GL.End();
        }

        /// <summary>
        /// Draws a texture.
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="frame"></param>
        public static void Texture(Texture2D tex, Rectangle frame)
        {
            var Position = Vector2.Zero;

            if (Option(DrawFlags.ParentCoordinateRelative))
            {
                Position = CurrentDrawingObject.TotalPosition;
            }
            if (Option(DrawFlags.CustomBounds))
            {
                Position += CustomBounds.Position;
            }

            Rectangle bounds;

            if (!Option(DrawFlags.CustomBounds))
            {
                bounds = new Rectangle(Position, CurrentDrawingObject.Transform.Scale);
            }
            else
            {
                bounds = new Rectangle(Position, CustomBounds.Size);
            }

            var posVectors = GetBoundCorners(bounds);

            //Rotate vectors
            for (var i = 0; i < posVectors.Length; i++)
            {
                posVectors[i] = posVectors[i].Rotate(bounds.Center, CurrentDrawingObject.Transform.RotationRadians);
            }
            
            var frameVectors = GetBoundCorners(frame);

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

            var d = Flags;
            Flags = DrawFlags.None;
            if (DebugHandler.BorderTextures)
            {
                ScreenHandler.Lines(new Vector2[] {
                    posVectors[0],
                    posVectors[1],
                    posVectors[2],
                    posVectors[3],
                    posVectors[0],
                }, 4, Color.Orange);
            }
            Flags = d;
        }

        /// <summary>
        /// Draws a texture.
        /// </summary>
        /// <param name="tex">The <see cref="Texture2D"/> to draw, usually off of a <see cref="Assets.SpriteAsset"/>.</param>
        /// <param name="t">The <see cref="Transform"/> to draw with.</param>
        public static void Texture(Texture2D tex)
            => Texture(tex, DefaultFrame);

        /// <summary>
        /// Draws lines where each vertex on the line is a <see cref="Vector2"/> in v.
        /// </summary>
        /// <param name="v">The array of vectors representing the vertexes on the line.</param>
        /// <param name="thickness">The thickness of the line.</param>
        /// <param name="c">The color of the line.</param>
        public static void Lines(Vector2[] v, float thickness, Color c)
        {
            GL.Enable(EnableCap.LineSmooth);
            GL.Disable(EnableCap.Texture2D);
            GL.Color4(c);
            GL.LineWidth(thickness);

            var toAdd = Vector2.Zero;

            if (Option(DrawFlags.ParentCoordinateRelative))
            {
                toAdd += CurrentDrawingObject.TotalPosition;
            }

            GL.Begin(PrimitiveType.Lines);

            for (var i = 0; i < v.Length - 1; i++)
            {
                GL.Vertex2(v[i] + toAdd);
                GL.Vertex2(v[i + 1] + toAdd);
            }

            GL.End();
        }

        public static void Circle(Vector2 center, float radius, Color c)
        {
            var currentOther = new Vector2(center.X, center.Y - radius);

            for (var i = 0; i < 360; i++)
            {
                Lines(new Vector2[] { center, currentOther }, radius * 200, c);
                currentOther = currentOther.Rotate(center, 1 * (float)(Math.PI / 180));
            }

            if (Option(DrawFlags.ParentCoordinateRelative))
            {
                center += CurrentDrawingObject.TotalPosition;
            }

            var d = Flags;
            Flags = DrawFlags.None;
            if (DebugHandler.BorderTextures)
            {
                ScreenHandler.Lines(new Vector2[] {
                    center - radius,
                    new Vector2(center.X + radius, center.Y - radius),
                    center + radius,
                    new Vector2(center.X - radius, center.Y + radius),
                    center - radius,
                }, 4, Color.Orange);
            }
            Flags = d;
        }

        public static void RoundedRectangle(float r)
        {
            
        }

        /// <summary>
        /// Converts a rectangle into a Vector2 list representing the corners of the rectangle.
        /// </summary>
        /// <param name="r">The rectangle to convert.</param>
        /// <returns></returns>
        public static Vector2[] GetBoundCorners(Rectangle r)
            => new[]
            {
                new Vector2(r.X, r.Y),
                new Vector2(r.X + r.Width, r.Y),
                new Vector2(r.X + r.Width, r.Y + r.Height),
                new Vector2(r.X, r.Y + r.Height)
            };
    }
}