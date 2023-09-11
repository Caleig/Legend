using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.accessories 
{
    public class GuardianoftheHeroicSpirit:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 75000;
            Item.accessory = true;
            Item.defense = 3;
            Item.rare = 5;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegenTime += 5;
            player.lifeRegen = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShinePotion, 5);
            recipe.AddIngredient(ItemID.IronBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}