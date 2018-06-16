namespace StardewHaze
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using StardewHaze.Data;
    using StardewModdingAPI;
    using StardewValley;
    using System;
    using System.Linq;

    /// <summary>
    ///     Represents a tilesheet injector for object assets.
    /// </summary>
    public class CropTileSheetInjector : IAssetEditor
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly AssetGraph assetGraph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CropTileSheetInjector"/> class.
        /// </summary>
        /// <param name="helper">
        ///     An <see cref="IModHelper"/> to access the game context.
        /// </param>
        /// <param name="monitor">
        ///     A reference to the <see cref="IMonitor"/> for debugging purposes.
        /// </param>
        /// <param name="assetGraph">
        ///     The <see cref="AssetGraph"/> containing all object data and indices.
        /// </param>
        public CropTileSheetInjector(IModHelper helper, IMonitor monitor, AssetGraph assetGraph)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.assetGraph = assetGraph;
        }

        /// <inheritdoc />
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("TileSheets/crops");
        }

        /// <inheritdoc />
        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsImage().Data;

            asset.ReplaceWith(new Texture2D(
                Game1.graphics.GraphicsDevice, 
                data.Width, 
                Math.Max(data.Height, ((int)this.assetGraph.Crops.Select(cropPair => cropPair.Key).Max() / 2) * 32 + 32)));

            asset.AsImage().PatchImage(data);

            this.assetGraph.Crops
                .ToList()
                .ForEach(crop => 
                {
                    asset.AsImage().PatchImage(
                        this.helper.Content.Load<Texture2D>(crop.Value.CropTileLocation, ContentSource.ModFolder),
                        null,
                        new Rectangle(((int)crop.Key % 2) * 128, ((int)crop.Key / 2) * 32, 128, 32));
                });
        }
    }
}
