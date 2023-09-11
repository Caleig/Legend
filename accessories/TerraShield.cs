using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.accessories 
{
    public class TerraShield:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 44;
            Item.value = 150000;
            Item.accessory = true;
            Item.defense = 20;
            Item.rare = 7;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegenTime += 5;
            player.noKnockback = true;
            player.lifeRegen = 2;
            player.buffImmune[30] = true;
            player.buffImmune[31] = true;
            player.buffImmune[36] = true;
            player.buffImmune[67] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
            player.buffImmune[20] = true;
            player.buffImmune[35] = true;
            player.buffImmune[32] = true;
            player.buffImmune[33] = true;
            player.buffImmune[80] = true;
            player.buffImmune[156] = true;

        
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BeetleShell, 1);
            recipe.AddIngredient(ItemID.FrozenShield, 1);
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}