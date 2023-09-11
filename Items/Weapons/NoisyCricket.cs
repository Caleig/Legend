using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace LegendMod.Items.Weapons 
{
    public class NoisyCricket:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 1750;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 18;
            Item.useTime = 400;
            Item.useAnimation = 400;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = 470000;
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 90f;

        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
           player.velocity = -Vector2.Normalize(Main.MouseWorld - player.Center) * 20f;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}