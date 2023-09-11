using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.misc 
{
    public class HighStrengthAlloy:ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("High Strength Alloy");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 22;
            Item.value = 10000;
            Item.rare = 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MeteoriteBar, 1);
            recipe.AddIngredient(ItemID.GoldBar, 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 2);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.MeteoriteBar, 1);
            recipe2.AddIngredient(ItemID.PlatinumBar, 1);
            recipe2.AddIngredient(ItemID.DemoniteBar, 2);
            recipe2.AddTile(TileID.Hellforge);
            recipe2.Register();
        
            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.MeteoriteBar, 1);
            recipe3.AddIngredient(ItemID.GoldBar, 1);
            recipe3.AddIngredient(ItemID.CrimtaneBar, 2);
            recipe3.AddTile(TileID.Hellforge);
            recipe3.Register();
        
            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ItemID.MeteoriteBar, 1);
            recipe4.AddIngredient(ItemID.PlatinumBar, 1);
            recipe4.AddIngredient(ItemID.CrimtaneBar, 2);
            recipe4.AddTile(TileID.Hellforge);
            recipe4.Register();
        }
    }
}