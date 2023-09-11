using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace LegendMod.Projectiles.Ranged
{
    public class bang : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("二踢脚 ");
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 5;//帧数
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 5;
            base.SetDefaults();
        }
        public override void AI()
        {
            int frameSpeed = 5;
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;//画面帧

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 5;
                }
            }
        }
    }
}