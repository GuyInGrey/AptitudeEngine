using AptitudeEngine.Assets;
using System.Drawing;
using System.IO;

namespace AptitudeEngine
{
    public abstract class Asset
    {
        public int ID { get; private set; }
        public string Path { get; private set; }

        /// <summary>
        /// For new Asset types.
        /// </summary>
        /// <param name="file"></param>
        public abstract void Load(FileStream file);

        /// <summary>
        /// Returns an Asset of the specified type.
        /// </summary>
        /// <typeparam name="T">The Asset type to load.</typeparam>
        /// <param name="path">The physical path of the Asset to load.</param>
        public static T Load<T>(string path)
            where T : Asset, new()
        {
            // we dont care if the file doesnt exist since we will want File.Open to cause an exception for stack tracing.
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var t = new T();
                t.Load(file);
                t.Path = path;

                return t;
            }
        }

        /// <summary>
        /// Returns an image <see cref="SpriteAsset"/> that shows text.
        /// </summary>
        /// <param name="s">The text to display.</param>
        /// <param name="c">The color of the text to display.</param>
        /// <param name="quality">The quality of the text.</param>
        public static SpriteAsset LoadText(string s, Color c, float quality)
        {
            var lineCnt = s.Split('\n').Length;

            var f = new Font(FontFamily.GenericSerif, quality, FontStyle.Regular);
            using (var bitmap = new Bitmap((int)(quality * s.Length), (int)(quality * 1.5f) * lineCnt))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Transparent);

                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center
                    };

                    graphics.DrawString(s, f, new SolidBrush(c), new System.Drawing.Rectangle(0,0, bitmap.Width, bitmap.Height), stringFormat);
                }

                var sa = new SpriteAsset();

                sa.Load(bitmap);
                return sa;
            }
        }
    }
}