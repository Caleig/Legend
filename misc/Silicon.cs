using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.misc 
{
    public class Silicon:ModItem
    {
        public override void SetStaticDefaults()
        {
             // DisplayName.SetDefault("Silicon");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = 300000;
            Item.rare = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 50);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
        }
    }
}