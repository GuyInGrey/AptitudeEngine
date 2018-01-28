using System.IO;

namespace AptitudeEngine.Assets
{
    public class SpriteAsset : Asset
    {
        public Texture2D Texture { get; private set; }
        public Rectangle Frame { get; set; }

        public override void Load(FileStream file)
        {
            Texture = Texture2D.FromStream(file);
            Frame = new Rectangle(0f, 0f, 1f, 1f);
        }
    }
}