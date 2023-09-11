using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Threading;
using System;

namespace LegendMod.Projectiles
{
    public class SingtheWoods : ModProjectile
    {
		public float tmr = 0;
        public override void SetDefaults() 
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.damage = 5;
			Projectile.light = 0.2f;

			AIType = ProjectileID.Bullet;
		}
        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.GreenTorch, 0, 0, 0, default, 1.2f);
            if (Projectile.timeLeft < 280)
			{
                tmr++;
                int frequency = 60;//转一圈需要的时间(帧)
                float radius = 80;//半径
                tmr = tmr % frequency;
                
                NPC target = null;
                Player player = Main.player[Projectile.owner];
                float Xdeviation = 0;//x偏移量
                float Ydeviation = 0;//y偏移量
                                     //x^2+y^2=r^2变式
                if (tmr < frequency / 2)
                {
                    float counter = tmr / 15 - 1;
                    Xdeviation = (float)(Math.Sqrt(1 - (counter * counter)) * radius);
                }
                else
                {
                    float counter = tmr / 15 - 3;
                    Xdeviation = -(float)(Math.Sqrt(1 - (counter * counter)) * radius);
                }
                if (tmr < frequency / 4 * 3 && tmr >= frequency / 4)
                {
                    Ydeviation = -(float)(Math.Sqrt(radius * radius - (Xdeviation * Xdeviation)));
                }
                else
                {
                    Ydeviation = (float)(Math.Sqrt(radius * radius - (Xdeviation * Xdeviation)));
                }
                Projectile.position.X = player.position.X + Xdeviation;
                Projectile.position.Y = player.position.Y + Ydeviation;
            }

        }
    }
}