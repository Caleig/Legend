using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace LegendMod.Projectiles
{
    public class swords : ModProjectile
    {
		protected int Timer
        {
            get { return (int)Projectile.ai[1];}
            set { Projectile.ai[1] = value; }
        }
        public override void SetStaticDefaults() {
			// DisplayName.SetDefault("testsword");
			Main.projFrames[Projectile.type] = 5;//帧数
			
		}

		public override void SetDefaults() {
			Projectile.width = 270;
			Projectile.height = 170;
			Projectile.scale = 1f;//规模
			Projectile.damage = 5; //伤害
			Projectile.light = 1f; //发光?0.8f
			Projectile.friendly = true;
			Projectile.tileCollide = false; // false使仆从自由通过瓷砖
			Projectile.DamageType = DamageClass.Melee; // 伤害类型=伤害等级.近战
			Projectile.friendly = true; // 弹丸友好
			Projectile.ownerHitCheck = true; // 所有者点击检查   防止通过瓷砖击中。大多数使用射弹的近战武器都有这个
			Projectile.penetrate = -1;//穿透-1为无限
			Projectile.ignoreWater = false;//忽略水
			//Projectile.netImportant = true;
			Projectile.timeLeft = 22;//时间
			Projectile.extraUpdates = 2; // 额外更新 每次更新 1+额外更新 次
		}
        public override void AI() {
			Player player = Main.player[Projectile.owner];
			player.heldProj = Projectile.whoAmI;//Vector2.Normalize(Main.MouseWorld - Projectile.Center)
			if(player.direction > 0){
				Projectile.Center = player.Center+ Vector2.Normalize(Main.MouseWorld - player.Center) * 20;//+ Vector2.Normalize(Projectile.velocity);//  * (Timer - 1f);
			}
			else{
				base.Projectile.spriteDirection = base.Projectile.direction;
				Projectile.Center = player.Center+ Vector2.Normalize(Main.MouseWorld - player.Center) * 20;//
			}
			// 这是一个简单的“从上到下循环所有帧”动画
			int frameSpeed = 6;
			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;//画面帧

				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 4;
				}
			}
		}
        public override void OnSpawn(IEntitySource source)
        {
			Player player = Main.player[Projectile.owner];
			var shoot = Vector2.Normalize(Main.MouseWorld - player.Center) * 10;
			Projectile.rotation = Vector2.Normalize(Main.MouseWorld - player.Center).ToRotation();
			if(player.direction < 0)
			{
				Projectile.rotation = Vector2.Normalize(Projectile.velocity).ToRotation() + MathHelper.Pi;
			}
            base.OnSpawn(source);
        }
    }
}