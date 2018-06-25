namespace StardewHaze
{
    using StardewHaze.Data;
    using StardewModdingAPI;
    using StardewModdingAPI.Events;
    using StardewValley;
    using System;
    using System.Linq;

    /// <summary>The mod entry point.</summary>
    public class StardewHazeEntry : Mod
    {
        private AssetGraph assetGraph;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // load configuration and build asset graph
            this.assetGraph = 
                AssetGraph.BuildAssetGraph(
                    helper.ReadJsonFile<StardewHazeConfig>("config/stardewhaze.json") 
                    ?? new StardewHazeConfig());

            // attach asset injectors
            helper.Content.AssetEditors.Add(new CropInjector(helper, this.Monitor, this.assetGraph));
            helper.Content.AssetEditors.Add(new CropTileSheetInjector(helper, this.Monitor, this.assetGraph));
            helper.Content.AssetEditors.Add(new ObjectInjector(helper, this.Monitor, this.assetGraph));
            helper.Content.AssetEditors.Add(new ObjectTileSheetInjector(helper, this.Monitor, this.assetGraph));
            helper.Content.AssetEditors.Add(new CraftableInjector(helper, this.Monitor, this.assetGraph));

            SaveEvents.AfterLoad += this.AddRecipe;
        }

        public void AddRecipe(object sendor, EventArgs e)
        {
            // skip if save not loaded yet
            if (!Context.IsWorldReady)
                return;

            this.assetGraph.Recipes
                .Where(recipe => recipe.Value.LearnOnLoad)
                .Where(recipe => !Game1.player.craftingRecipes.ContainsKey(recipe.Key))
                .ToList()
                .ForEach(recipe => {
                    if (recipe.Value.IsCookable)
                        Game1.player.cookingRecipes.Add(recipe.Key, 0);
                    else
                        Game1.player.craftingRecipes.Add(recipe.Key, 0);
                });
           
        }
    }
}
