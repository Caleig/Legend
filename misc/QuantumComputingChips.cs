using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using LegendMod.misc;
using Microsoft.Xna.Framework;

namespace LegendMod.misc 
{
    public class QuantumComputingChips:ModItem
    {
        public override void SetStaticDefaults()
        {
             // DisplayName.SetDefault("Quantum computing chips");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = 100000;
            Item.rare = 6;
        
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ItemID.Wire, 5);
            recipe.AddIngredient(ModContent.ItemType<Silicon>(), 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}