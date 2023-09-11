using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Social;
using Terraria.Audio;

namespace LegendMod.Projectiles.Ranged
{
    public class pangpang : ModProjectile
    {
        protected int Timer
        {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("二踢脚 ");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            base.SetDefaults();
        }
        public override void AI()
        {
            for(int i = 0; i < 1; i++)
                {
                    Dust.NewDust(Projectile.Center, 40, 40, DustID.Firework_Red, 0, 0, 0, default, 1f);
                }
            if(Timer > 30)
            {
                for(int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.Center, 80, 80, DustID.FireflyHit, 0, 0, 0, default, 1.5f);
                }
                Projectile projectile = Projectile.NewProjectileDirect(Entity.GetSource_FromAI(), Projectile.Center, Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<Projectiles.Ranged.bang>(), 50, 0);
                Timer = 0;
                Projectile.Kill();
            }
            else
            {
                Projectile.velocity.X = 0;
                Projectile.velocity.Y = -Timer * 1f;
                Timer++;
            }
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for(int i = 0; i < 20; i++)
                {
                    Dust.NewDust(Projectile.Center, 40, 40, DustID.Firework_Yellow, 0, 0, 0, default, 1.2f);
                }
                Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<Projectiles.Ranged.bang>(), 50, 0);
                Timer = 0;
                Projectile.Kill();
        }
        public override bool PreKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14);
            return base.PreKill(timeLeft);
        }
    }
}