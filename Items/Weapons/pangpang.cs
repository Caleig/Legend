using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace LegendMod.Items.Weapons
{
    public class pangpang : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("二踢脚");
            // Tooltip.SetDefault("过年了，嘿嘿");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.damage = 55;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 90;
            Item.height = 90;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = 200000;
            Item.noMelee = true;
            Item.rare = 3;
            Item.UseSound = SoundID.Item14;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.pangpang>();
            Item.useTurn = true;
            Item.maxStack = 999;
            Item.consumable = true;
            base.SetDefaults();
        }
    }
}