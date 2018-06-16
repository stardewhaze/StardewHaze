namespace StardewHaze.Data
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     An asset graph which maps game assets by unique tile/object index.
    /// </summary>
    public class AssetGraph
    {
        protected uint NextCropIndex { get; } = 70;
        protected uint NextObjectIndex { get; } = 900;
        public IDictionary<uint, CropConfig> Crops { get; }
        public IDictionary<uint, ObjectConfig> Objects { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssetGraph"/> class.
        /// </summary>
        /// <param name="nextCropIndex"></param>
        /// <param name="nextObjectIndex"></param>
        /// <param name="cropData"></param>
        /// <param name="objectData"></param>
        protected AssetGraph(
            uint nextCropIndex,
            uint nextObjectIndex,
            IDictionary<uint, CropConfig> cropData,
            IDictionary<uint, ObjectConfig> objectData)
        {
            this.NextCropIndex = nextCropIndex;
            this.NextObjectIndex = nextObjectIndex;
            this.Crops = cropData;
            this.Objects = objectData;
        }

        /// <summary>
        ///     Builds an asset graph with unique tiling and database indices, given
        ///     a configuration containing index offsets.
        /// </summary>
        /// <param name="stardewHazeConfig">
        ///     The <see cref="StardewHazeConfig"/> configuration file containing
        ///     index offsets.
        /// </param>
        /// <returns>
        ///     An asset graph containing all crops and objects defined in configuration.
        /// </returns>
        public static AssetGraph BuildAssetGraph(StardewHazeConfig stardewHazeConfig)
        {
            // start building the asset graph
            return stardewHazeConfig.Crops
                // aggregate by crops, since crops require objects (seed and product)
                .Aggregate(
                    new AssetGraph(
                        // start of tiling at indices provided by configuration
                        stardewHazeConfig.CropTileOffset,
                        stardewHazeConfig.ObjectTileOffset,
                        // start off with an empty crop database
                        new Dictionary<uint, CropConfig>(),
                        // and seed the object database with objects provided by configuration
                        Enumerable.ToDictionary(
                            stardewHazeConfig.Objects,
                            objectDictionary => (uint)objectDictionary.ObjectId,
                            objectDictionary => objectDictionary)),
                    // with intermediate results, for every crop in configuration
                    (assetGraph, crop) =>
                        // update the intermediate asset graph
                        new AssetGraph(
                            // by incrementing the tiling indices
                            assetGraph.NextCropIndex + 1,
                            assetGraph.NextObjectIndex + 2,
                            // and copying the crops already there
                            new Dictionary<uint, CropConfig>(assetGraph.Crops)
                            {
                                // and adding the current crop
                                {
                                    assetGraph.NextCropIndex,
                                    new CropConfig {
                                        Identifier = crop.Identifier,
                                        CropName = crop.CropName,
                                        SeedName = crop.SeedName,
                                        ProductName = crop.ProductName,
                                        ProductDescription = crop.ProductDescription,
                                        SeedDescription = crop.SeedDescription,
                                        ProductPrice = crop.ProductPrice,
                                        SeedPrice = crop.SeedPrice,
                                        ProductEdibility = crop.ProductEdibility,
                                        ProductType = crop.ProductType,
                                        ProductRemainder = crop.ProductRemainder,
                                        DaysPerGrowthStage = crop.DaysPerGrowthStage,
                                        GrowthSeasons = crop.GrowthSeasons,
                                        RegrowAfterHarvest = crop.RegrowAfterHarvest,
                                        HarvestMethod = crop.HarvestMethod,
                                        ExtraHarvest = crop.ExtraHarvest,
                                        RaisedSeeds = crop.RaisedSeeds,
                                        TintColor = crop.TintColor,
                                        CropTileLocation = crop.CropTileLocation,
                                        SeedTileLocation = crop.SeedTileLocation,
                                        ProductTileLocation = crop.ProductTileLocation,
                                        CropId = (int)assetGraph.NextCropIndex,
                                        SeedId = (int)assetGraph.NextObjectIndex + 1,
                                        ProductId = (int)assetGraph.NextObjectIndex
                                    }
                                }
                            },
                            // and copying the crops already there
                            new Dictionary<uint, ObjectConfig>(assetGraph.Objects)
                            {
                                // and adding the current crop's product
                                {
                                    (uint)assetGraph.NextObjectIndex,
                                    new ObjectConfig
                                    {
                                        Name = crop.ProductName,
                                        Price = crop.ProductPrice,
                                        Edibility = crop.ProductEdibility,
                                        Type = crop.ProductType,
                                        DisplayName = crop.ProductName,
                                        Description = crop.ProductDescription,
                                        Remainder = crop.ProductRemainder,
                                        ObjectId = (int)assetGraph.NextObjectIndex,
                                        TilesheetLocation = crop.ProductTileLocation
                                    }
                                },
                                // and adding the current crop's seed
                                {
                                    (uint)assetGraph.NextObjectIndex + 1,
                                    new ObjectConfig
                                    {
                                        Name = crop.SeedName,
                                        Price = crop.SeedPrice,
                                        Edibility = -300,
                                        Type = "Basic -74",
                                        DisplayName = crop.SeedName,
                                        Description = crop.SeedDescription,
                                        ObjectId = (int)assetGraph.NextObjectIndex + 1,
                                        TilesheetLocation = crop.SeedTileLocation
                                    }
                                }
                            }));
        }
    }
}