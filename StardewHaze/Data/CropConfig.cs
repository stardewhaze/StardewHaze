namespace StardewHaze.Data
{
    /// <summary>
    ///     Represents crop configuration.
    /// </summary>
    public class CropConfig
    {
        public string Identifier { get; set; } = "OrangeBud";
        public string CropName { get; set; } = "Orange Bud";
        public string SeedName { get; set; } = "Orange Seed";
        public string ProductName { get; set; } = "Orange Nug";
        public string ProductDescription { get; set; } = "";
        public string SeedDescription { get; set; } = "";
        public uint ProductPrice = 420;
        public uint SeedPrice = 80;
        public int ProductEdibility = 20;
        public string ProductType = "Flower -80";
        public string ProductRemainder = "";
        public string DaysPerGrowthStage { get; set; } = "1 1 1 1 1";
        public string GrowthSeasons { get; set; } = "spring summer";
        public int RegrowAfterHarvest { get; set; } = -1;
        public int HarvestMethod { get; set; } = 0;
        public string ExtraHarvest { get; set; } = "false";
        public bool RaisedSeeds { get; set; } = false;
        public string TintColor { get; set; } = "false";
        public string CropTileLocation { get; set; } = "resources/crops/OrangeBudCrop.png";
        public string SeedTileLocation { get; set; } = "resources/crops/OrangeSeedCrop.png";
        public string ProductTileLocation { get; set; } = "resources/crops/OrangeBudNug.png";
        public int CropId { get; set; } = -1;
        public int SeedId { get; set; } = -1;
        public int ProductId { get; set; } = -1;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{DaysPerGrowthStage}/{GrowthSeasons}/{CropId}/{ProductId}/{RegrowAfterHarvest}/{HarvestMethod}/{ExtraHarvest}/{RaisedSeeds.ToString().ToLower()}/{TintColor}";
        }
    }
}
