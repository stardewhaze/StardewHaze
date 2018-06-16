namespace StardewHaze
{
    using StardewHaze.Data;
    using StardewModdingAPI;
    using System.Linq;

    /// <summary>
    ///     Represents an injector for crop data.
    /// </summary>
    public class CropInjector : IAssetEditor
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly AssetGraph assetGraph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CropInjector"/> class.
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
        public CropInjector(IModHelper helper, IMonitor monitor, AssetGraph assetGraph)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.assetGraph = assetGraph;
        }

        /// <inheritdoc />
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Data/Crops");
        }

        /// <inheritdoc />
        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsDictionary<int, string>().Data;

            this.assetGraph.Crops.Where(cropPair => !data.ContainsKey(cropPair.Value.SeedId))
                .Select(cropPair => cropPair.Value)
                .ToList()
                .ForEach(crop => data.Add(crop.SeedId, crop.ToString()));
        }
    }
}
