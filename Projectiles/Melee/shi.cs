using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using FighterSickle;

namespace LegendMod.Projectiles.Melee
{
    public class shi : ModProjectile
    {
        public SwingHelper swingHelper;
        public Player player;
        public bool InUse;
        public override string Texture => "LegendMod/Projectiles/Melee/shi";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.Size = new(141);
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.usesLocalNPCImmunity = false;
            Projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            if (player == null)
            {
                player = Main.player[Projectile.owner];
                swingHelper = new(Projectile, 10);
            }
            if (player.HeldItem.type == ItemID.Zenith)
            {
                Projectile.Kill();
                return;
            }
            Lighting.AddLight(Projectile.Center, 1, 1, 1);
            Projectile.timeLeft = 2;
            if (player.controlUseItem)
            {
                InUse = true;
            }
            if (Projectile.Center == player.Center)
            {
                Projectile.Kill();
                return;
            }
            if (InUse)
            {
                Projectile.hide = false;

                swingHelper.ProjFixedPlayerCenter(player, 0, true);
                switch ((int)Projectile.ai[0])
                {
                    case 0:
                        {
                            swingHelper.Change(-Vector2.UnitY.RotatedBy(-0.8), new(1, 0.7f), 0.2f);
                            Projectile.ai[1] += 0.2f;
                            if (Projectile.ai[1] > MathHelper.Pi + MathHelper.PiOver2)
                            {
                                Projectile.ai[0]++;
                                Projectile.ai[1] = 0;
                                Projectile.hide = true;
                                InUse = false;
                                return;
                            }
                            break;
                        }
                    case 1:
                        {
                            Projectile.hide = false;
                            swingHelper.Change_Lerp(-Vector2.UnitX, 0.1f, new(1, 0.4f), 1f, 0.7f, 0.5f);
                            if (MathF.Abs(Projectile.velocity.Y) < 0.05f)
                            {
                                Projectile.ai[0]++;
                            }
                            break;
                        }
                    case 2:
                        {
                            Projectile.ai[1] += 0.2f;
                            if (Projectile.ai[1] > MathHelper.Pi + MathHelper.PiOver2)
                            {
                                Projectile.ai[0]++;
                                Projectile.ai[1] = 0;
                                Projectile.Kill();
                                InUse = false;
                                return;
                            }
                            break;
                        }
                    default:
                        Projectile.Kill();
                        InUse = false;
                        Projectile.ai[0] = Projectile.ai[1] = 0;
                        break;
                }
                swingHelper.SwingAI(140, player.direction, Projectile.ai[1]);
            }
            else
            {
                Projectile.ai[0] = Projectile.ai[1] = 0;
                swingHelper.ProjFixedPlayerCenter(player);
                Projectile.hide = true;
                Projectile.position.X -= player.width * player.direction;
                Projectile.rotation = Vector2.UnitY.RotatedBy(-0.2 * player.direction).ToRotation();
            }
        }
        public override bool? CanDamage() => swingHelper != null;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float r = 0;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity,
                Projectile.width, ref r);
        }
        public override bool ShouldUpdatePosition() => false;
        public override bool PreDraw(ref Color lightColor)
        {
            if (swingHelper != null && InUse)
            {
                swingHelper.Swing_Draw(lightColor);
                swingHelper.Swing_TrailingDraw(TextureAssets.Extra[201].Value, (f) =>
                {
                    Color color = Color.Lerp(Color.Black, Color.Blue, f);
                    color.A = 0;
                    return color;
                });
                return false;
            }
            return base.PreDraw(ref lightColor);
        }
    }
}