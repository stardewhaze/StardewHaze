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
    public class ObjectTileSheetInjector : IAssetEditor
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly AssetGraph assetGraph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectTileSheetInjector"/> class.
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
        public ObjectTileSheetInjector(IModHelper helper, IMonitor monitor, AssetGraph assetGraph)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.assetGraph = assetGraph;
        }

        /// <inheritdoc />
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Maps/springobjects");
        }

        /// <inheritdoc />
        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsImage().Data;

            asset.ReplaceWith(new Texture2D(
                Game1.graphics.GraphicsDevice, 
                data.Width, 
                Math.Max(data.Height, ((int)this.assetGraph.Objects.Select(cropPair => cropPair.Key).Max() / 24) * 16 + 16)));

            asset.AsImage().PatchImage(data);

            this.assetGraph.Objects
                .ToList()
                .ForEach(obj =>
                {
                    asset.AsImage().PatchImage(
                        this.helper.Content.Load<Texture2D>(obj.Value.TilesheetLocation, ContentSource.ModFolder),
                        null,
                        new Rectangle(((int)obj.Key % 24) * 16, ((int)obj.Key / 24) * 16, 16, 16));
                });
        }
    }
}
