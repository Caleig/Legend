using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;


namespace LegendMod.Projectiles
{
    public class CrescentMoonSlash:ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crescent Moon Slash");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 100;  
            Projectile.scale = 1f;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = 27;
            Projectile.tileCollide = false;
        
        }
    }
}