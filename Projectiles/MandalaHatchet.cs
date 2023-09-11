using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;


namespace LegendMod.Projectiles
{
    public class MandalaHatchet:ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("fly!");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 28;  
            Projectile.scale = 1f;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        
        }
        public override void AI()
        {
            Projectile.rotation += 0.2f;
            Projectile.velocity.Y += 0.15f;
        }
    }
}