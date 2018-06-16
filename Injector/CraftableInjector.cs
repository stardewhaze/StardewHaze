namespace StardewHaze
{
    using StardewHaze.Data;
    using StardewModdingAPI;
    using System.Linq;

    /// <summary>
    ///     Represents an injector for craftable data.
    /// </summary>
    public class CraftableInjector : IAssetEditor
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly AssetGraph assetGraph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CraftableInjector"/> class.
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
        public CraftableInjector(IModHelper helper, IMonitor monitor, AssetGraph assetGraph)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.assetGraph = assetGraph;
        }

        /// <inheritdoc />
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Data/CraftingRecipes");
        }

        /// <inheritdoc />
        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsDictionary<string, string>().Data;

            this.assetGraph.Recipes
                .Select(objectPair => objectPair.Value)
                .Where(obj => !data.ContainsKey(obj.Name))
                .Where(obj => !obj.IsCookable)
                .ToList()
                .ForEach(obj => data.Add(obj.Name, obj.ToString()));
        }
    }
}
