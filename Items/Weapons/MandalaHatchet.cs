using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using LegendMod.Projectiles;
using Microsoft.Xna.Framework;

namespace LegendMod.Items.Weapons
{
    public class MandalaHatchet:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 87000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.MandalaHatchet>();
            Item.shootSpeed = 40f;

        }
    }
}