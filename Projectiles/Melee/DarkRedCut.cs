using LegendMod.Common.AuxiliaryMeans;
using LegendMod.Common.DrawTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LegendMod.Projectiles.Melee
{
    public class DarkRedCut : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.scale = 1f;//缩放
            Projectile.ignoreWater = true;//无视水阻力
            Projectile.tileCollide = false;//无视实心方块碰撞
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;//存活时间.tick
            Projectile.alpha = 0;
            Projectile.friendly = true;//开启敌对伤害
            Projectile.hostile = false;//关闭友方伤害
            Projectile.damage = 38;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;

            Projectile.usesIDStaticNPCImmunity = true;//自定义对NPC无敌帧影响
            Projectile.idStaticNPCHitCooldown = 5;
        }

        public override string Texture => "LegendMod/Asset/Projectiles/DarkRedCut";

        public int Status
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public int Behavior
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public int ThisTimeValue
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }

        public override bool? CanHitNPC(NPC target)
        {
            for (int i = 0; i < 32; i++)
            {
                Vector2 vr = (MathHelper.TwoPi / 64f * i).ToRotationVector2() * 3f;
                int dust = Dust.NewDust(target.Center, 8, 8, DustID.RedTorch, vr.X, vr.Y);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 2f;
            }
            
            return base.CanHitNPC(target);
        }

        public override void OnSpawn(IEntitySource source)
        {
            Lighting.AddLight(Projectile.Center, Color.Red.ToVector3() * 10);
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void AI()
        {
            ThisTimeValue++;
            Player owner = Main.player[Projectile.owner];
            Vector2 ToMou = Main.MouseWorld - owner.Center;
            Projectile.Center = owner.Center;
            AiBehavior.EntityToRot(Projectile, ToMou.ToRotation(), 0.1f);
            DrawUtils.ClockFrame(ref Projectile.frameCounter, 5, 5);

            if (Main.fpsCount % 5 == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Vector2 spanPos = (MathHelper.TwoPi / 16f * i).ToRotationVector2() * 80f + Projectile.Center;
                    Vector2 vr = Vector2.Normalize(owner.Center - spanPos) * 13f;
                    int dust = Dust.NewDust(spanPos, 8, 8, DustID.RedTorch, vr.X, vr.Y);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;

            Vector2 startPos = Projectile.rotation.ToRotationVector2() * -200 + Projectile.Center;
            Vector2 endPos = Projectile.rotation.ToRotationVector2() * 300 + Projectile.Center;

            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                startPos,
                endPos,
                84,
                ref point
                );
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D mainValue = DrawUtils.GetT2DValue(Texture);
            SpriteEffects spriteEffects = Projectile.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;
            Vector2 Sep = new Vector2(1, 2);

            Main.EntitySpriteDraw(
                mainValue,
                DrawUtils.WDEpos(Projectile.Center),
                DrawUtils.GetRec(mainValue, (int)Projectile.frameCounter, 6),
                Color.White,
                Projectile.rotation,
                DrawUtils.GetOrig(mainValue, (int)Projectile.frameCounter, 6),
                Sep * Projectile.scale,
                spriteEffects,
                0
                );
            return false;
        }
    }
}
