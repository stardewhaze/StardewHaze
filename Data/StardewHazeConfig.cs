namespace StardewHaze.Data
{
    /// <summary>
    ///     Represents mod configuration.
    /// </summary>
    public class StardewHazeConfig
    {
        public uint ObjectTileOffset { get; set; } = 700;
        public uint CropTileOffset { get; set; } = 50;
        public CropConfig[] Crops { get; set; } = new CropConfig[] { };
        public ObjectConfig[] Objects { get; set; } = new ObjectConfig[] { };
        public RecipeConfig[] Recipes { get; set; } = new RecipeConfig[] { };
    }
}
