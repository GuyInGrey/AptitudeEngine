using System.Collections.Generic;
using AptitudeEngine.Assets;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Components.Tiling
{
    public class TileScreen : AptComponent
    {
        public float TileSize { get; set; } = 1;

        /// <summary>
        /// Adds a tile to the TileScreen.
        /// </summary>
        /// <param name="layer">Which layer the tile is placed on.</param>
        /// <param name="row">Which row the tile is placed on.</param>
        /// <param name="tilePath">The path of the texture the tile uses.</param>
        /// <param name="frames">The frames in the tile's animation.</param>
        /// <param name="frameRate">The framerate of the tile's animation.</param>
        /// <returns>Returns the index in the row where the tile is.</returns>
        public int AddTileToRow(int layer, int row, string tilePath, List<AptRectangle> frames, int frameRate)
        {
            var tileObject = Context.Instantiate(Owner.Children[layer].Children[row]);
            tileObject.Transform.Position = new Vector2();
            tileObject.Transform.Size = new Vector2(TileSize, TileSize);

            var renderer = tileObject.AddComponent<SpriteRenderer>();
            var animator = tileObject.AddComponent<SpriteAnimator>();
            renderer.Sprite = Asset.Load<SpriteAsset>(tilePath);
            animator.Animation = new Animation
            {
                FrameRate = frameRate,
                Frames = frames,
            };

            var tileObjectIndex =
                new List<AptObject>(Owner.Children).FindIndex(a => a.GetHashCode() == tileObject.GetHashCode());
            return tileObjectIndex;
        }

        /// <summary>
        /// Adds a tile to the TileScreen
        /// </summary>
        /// <param name="layer">Which layer the tile is placed on.</param>
        /// <param name="row">Which row the tile is placed on.</param>
        /// <param name="tilePath">The path of the texture the tile uses.</param>
        /// <returns>Returns the index in the row where the tile is.</returns>
        public int AddTileToRow(int layer, int row, string tilePath)
            => AddTileToRow(layer, row, tilePath, new List<AptRectangle>() {new AptRectangle(0, 0, 1, 1)}, 1);

        /// <summary>
        /// Removes a tile from the TileScreen.
        /// </summary>
        /// <param name="layer">Which layer the tile is removed from.</param>
        /// <param name="row">Which row the tile is removed from.</param>
        /// <param name="tile">Index of the tile to be removed.</param>
        public void RemoveTile(int layer, int row, int tile)
            => Owner.Children[layer].Children[row].RemoveChild(tile);
    }
}