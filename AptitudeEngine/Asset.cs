using AptitudeEngine.Assets;
using System.Drawing;
using System.IO;

namespace AptitudeEngine
{
    public abstract class Asset
    {
        public int ID { get; private set; }
        public string Path { get; private set; }

        public abstract void Load(FileStream file);

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

        public static SpriteAsset LoadText(string s, float fontSize, Color c)
        {
            var lineCnt = s.Split('\n').Length;

            var f = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Regular);
            using (var b = new Bitmap((int)(fontSize * s.Length), ((int)(fontSize + (fontSize / 2))) * lineCnt))
            {
                using (var g = Graphics.FromImage(b))
                {
                    g.Clear(Color.Transparent);

                    var stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;

                    g.DrawString(s, f, new SolidBrush(c), new Rectangle(0,0,b.Width,b.Height), stringFormat);
                }

                var sa = new SpriteAsset();

                sa.LoadFromBitmap(b);
                return sa;
            }
        }
    }
}