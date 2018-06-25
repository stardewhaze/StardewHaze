using System.Collections.Generic;

namespace StardewHaze.Data
{
    using System.Collections.Generic;
    using System.Linq;

    public class RecipeConfig
    {
        public RecipeIngredients[] Ingredients { get; set; }
        public bool IsStackable { get; set; } = true;
        public string Name { get; set; }
        public int ObjectId { get; set; } = -1;
        public string UsageLocation { get; set; } = "Home";
        public string SkillAndLevel { get; set; } = "null";
        public bool IsCookable { get; set; } = true;
        public bool LearnOnLoad { get; set; } = true;

        public override string ToString()
        {
            return Ingredients.Select(ingredient => ingredient.ToString()).Aggregate((l, r) => l + " " + r)
                + $"/{UsageLocation}/{ObjectId}/{IsCookable.ToString().ToLower()}/{SkillAndLevel}";
        }
    }

    public class RecipeIngredients
    {
        public string Name { get; set; }
        public uint Quantity { get; set; } = 1;
        public int ObjectId = -1;

        public override string ToString()
        {
            return $"{ObjectId} {Quantity}";
        }
    }
}
