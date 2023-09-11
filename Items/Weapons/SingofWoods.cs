using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.CodeAnalysis.Operations;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace LegendMod.Items.Weapons
{
    public class SingofWoods : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("森林轻语");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 40;
            Item.height = 40;
            Item.noMelee = true;
            Item.useTime = 20;
            Item.useAnimation = 60;
            Item.UseSound = SoundID.Item39;
            Item.useStyle = 5;
            Item.shootSpeed = 10f;
            Item.damage = 30;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.SingtheWoods>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (float r = 0f; r < MathHelper.TwoPi; r += MathHelper.TwoPi / 6f)
            {
                // 发射速度是与x轴正半轴夹角为r，长度为
                Vector2 Velocity = new Vector2((float)Math.Cos(r), (float)Math.Sin(r)) * 10f;
                Projectile.NewProjectile(source, position, Velocity, type, 100, 10f, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}