namespace StardewHaze.Data
{
    /// <summary>
    ///     Represents object configuration.
    /// </summary>
    public class ObjectConfig
    {
        public string Name { get; set; } = "Orange Nug";
        public uint Price { get; set; } = 420;
        public int Edibility { get; set; } = 20;
        public string Type { get; set; } = "Basic -26";
        public string DisplayName { get; set; } = "Orange Nug";
        public string Description { get; set; } = "It's almost the sweetest thing you've ever smelled.";
        public string Remainder { get; set; } = "";
        public string TilesheetLocation { get; set; } = "resources/crops/OrangeBudNug.png";
        public int ObjectId = -1;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}/{Price}/{Edibility}/{Type}/{DisplayName}/{Description}/{Remainder}";
        }
    }
}
