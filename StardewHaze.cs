namespace StardewHaze
{
    using StardewHaze.Data;
    using StardewModdingAPI;

    /// <summary>The mod entry point.</summary>
    public class StardewHazeEntry : Mod
    {
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // load configuration and build asset graph
            var assetGraph = 
                AssetGraph.BuildAssetGraph(
                    helper.ReadJsonFile<StardewHazeConfig>("config/stardewhaze.json") 
                    ?? new StardewHazeConfig());

            // attach asset injectors
            helper.Content.AssetEditors.Add(new CropInjector(helper, this.Monitor, assetGraph));
            helper.Content.AssetEditors.Add(new CropTileSheetInjector(helper, this.Monitor, assetGraph));
            helper.Content.AssetEditors.Add(new ObjectInjector(helper, this.Monitor, assetGraph));
            helper.Content.AssetEditors.Add(new ObjectTileSheetInjector(helper, this.Monitor, assetGraph));
        }
    }
}
