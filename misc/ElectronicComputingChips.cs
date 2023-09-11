using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.misc 
{
    public class ElectronicComputingChips:ModItem
    {
        public override void SetStaticDefaults()
        {
             // DisplayName.SetDefault("Electronic computing chips");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 22;
            Item.value = 100000;
            Item.rare = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 2);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}