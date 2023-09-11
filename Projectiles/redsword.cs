using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;

namespace LegendMod.Projectiles
{
    public class redsword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("red sword slash");
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;  
            Projectile.scale = 1f;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.light = 0.7f;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if(player.direction > 0)
            {
                Projectile.rotation = (float)(Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X));
            }
            else
            {
                Projectile.spriteDirection = Projectile.direction;
                Projectile.rotation = Vector2.Normalize(Projectile.velocity).ToRotation() + MathHelper.Pi;
            }
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 30; i++)
            {
                // 生成dust
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.RedMoss, 0, 0, 100, Color.LightBlue, 1.5f);
                
                // 粒子效果无重力
                d.noGravity = true;
                // 粒子效果初速度乘以二
                d.velocity *= 2;
            }
        }
    }
}