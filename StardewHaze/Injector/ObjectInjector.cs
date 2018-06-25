namespace StardewHaze
{
    using StardewHaze.Data;
    using StardewModdingAPI;
    using System.Linq;

    /// <summary>
    ///     Represents an injector for object data.
    /// </summary>
    public class ObjectInjector : IAssetEditor
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly AssetGraph assetGraph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectInjector"/> class.
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
        public ObjectInjector(IModHelper helper, IMonitor monitor, AssetGraph assetGraph)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.assetGraph = assetGraph;
        }

        /// <inheritdoc />
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Data/ObjectInformation");
        }

        /// <inheritdoc />
        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsDictionary<int, string>().Data;

            this.assetGraph.Objects
                .Select(objectPair => objectPair.Value)
                .Where(obj => !data.ContainsKey(obj.ObjectId))
                .ToList()
                .ForEach(obj => data.Add(obj.ObjectId, obj.ToString()));
        }
    }
}
