using AptitudeEngine.CoordinateSystem;
using System.IO;

namespace AptitudeEngine.Assets
{
    public class SpriteAsset : Asset
    {
        /// <summary>
        /// The texture of the <see cref="SpriteAsset"/>.
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// The current image frame selected.
        /// </summary>
        public AptRectangle Frame { get; set; }

        /// <summary>
        /// Loads the SpriteAsset.
        /// </summary>
        /// <param name="file">The file to load the asset from.</param>
        public override void Load(FileStream file)
        {
            Texture = Texture2D.FromStream(file);
            Frame = new AptRectangle(0f, 0f, 1f, 1f);
        }
    }
}