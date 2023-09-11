using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.Items.Weapons
{
    public class Fatebearer:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.width = 114;
            Item.height = 114;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 9;
            Item.value = 550000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MagicMissile;
            Item.shootSpeed = 55f;

        }
    }
}