using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace LegendMod.Projectiles
{
    public class RedBigProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.netUpdate = true;
            Projectile.ownerHitCheck = true;
            Projectile.timeLeft = 30;
            Projectile.extraUpdates = 1;
            Projectile.width = 656;
            Projectile.height = 656;
            base.SetDefaults();
        }
        public override void AI()
        {
            Main.projFrames[Projectile.type] = 2;
            Projectile.frameCounter++;
            if(Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter=0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            base.AI();
        }
    }
}