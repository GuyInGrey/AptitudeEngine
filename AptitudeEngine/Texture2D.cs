using System;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

using OpenTK.Graphics.OpenGL;

namespace AptitudeEngine
{
    public struct Texture2D
    {
        public int Width { get; }

        public int Height { get; }

        public int ID { get; }

        public Texture2D(int id, int width, int height)
        {
			ID = id;
            Width = width;
            Height = height;
		}

		public static Texture2D FromStream(FileStream reader)
		{
			using (var bmp = new Bitmap(reader))
			{
				return FromBitmap(bmp);
			}
		}

		public static Texture2D FromBitmap(Bitmap bmp)
		{
			var id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, id);

			var data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly,
			System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
				OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			bmp.UnlockBits(data);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)
				TextureWrapMode.Clamp);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)
				TextureWrapMode.Clamp);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
				(int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
				(int)TextureMagFilter.Linear);

			var w = bmp.Width;
			var h = bmp.Height;
			bmp.Dispose();
			return new Texture2D(id, w, h);
		}
	}
}
