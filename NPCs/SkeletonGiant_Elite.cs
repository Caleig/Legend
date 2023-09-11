using LegendMod.Common.AuxiliaryMeans;
using LegendMod.Common.DrawTools;
using LegendMod.Common.WorldGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LegendMod.NPCs
{
    public class SkeletonGiant_Elite : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 10;
            NPCID.Sets.TrailingMode[Type] = 1;
            NPCID.Sets.TrailCacheLength[Type] = 16;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 68;
            NPC.scale = 2;
            NPC.damage = 52;
            NPC.defense = 12;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 820;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
        }

        public override string Texture => "LegendMod/Asset/NPCs/SkeletonGiant";

        public int Status
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public int Behavior
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public int ThisTimeValue
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public int Counter
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 1020;
            NPC.damage = 58;
            if (bossAdjustment == 1)
            {
                NPC.lifeMax = 1580;
                NPC.damage = 78;
            }
            if (bossAdjustment == 0.85f)
            {
                NPC.lifeMax = 2200;
                NPC.damage = 98;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            base.HitEffect(hit);
        }

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByItem(player, item, hit, damageDone);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            base.OnHitPlayer(target, hurtInfo);
        }

        public override void OnKill()
        {
            base.OnKill();
        }

        int AttackRange = 660 * 660;
        public override void AI()
        {
            ThisTimeValue++;

            Player target = AiBehavior.FindingTarget(NPC, 1800);
            Vector2 toTarget = Vector2.Zero;
            AttackRange = 1260 * 1260;

            if (AiBehavior.PlayerAlive(target) == false)
            {
                Status = 2;
            }
            else
            {
                toTarget = target.Center - NPC.Center;
            }

            if (Status == 0)
            {
                DrawUtils.ClockFrame(ref NPC.frameCounter, 10, 6);

                if (Behavior == 0)
                {

                    NPC.velocity.X = NPC.direction * AiBehavior.AsymptoticVelocity(new Vector2(NPC.Center.X, 0), new Vector2(target.Center.X, 0), 4f, 16);

                    if (toTarget.LengthSquared() < AttackRange)
                    {
                        ThisTimeValue = 0;
                        Behavior = 0;
                        Status = 1;
                    }
                }
            }

            if (Status == 1)
            {
                if (Behavior == 0)
                {
                    DrawUtils.ClockFrame(ref NPC.frameCounter, 10, 6);

                    NPC.velocity.X = NPC.direction * AiBehavior.AsymptoticVelocity(new Vector2(NPC.Center.X, 0), new Vector2(target.Center.X, 0), 3f, 16);

                    if (Counter > 3)
                    {
                        Counter = 0;
                        ThisTimeValue = 0;
                        Behavior = 2;
                    }

                    if (ThisTimeValue > 30)
                    {
                        Counter++;
                        ThisTimeValue = 0;
                        Behavior = 1;
                    }
                }
                if (Behavior == 1)
                {
                    DrawUtils.ClockFrame(ref NPC.frameCounter, 10, 9, 7);

                    NPC.velocity.X = NPC.direction * AiBehavior.AsymptoticVelocity(new Vector2(NPC.Center.X, 0), new Vector2(target.Center.X, 0), 2f, 16);



                    if (NPC.frameCounter == 8)
                    {
                        Vector2 vr = (toTarget.ToRotation() + MathHelper.ToRadians(HcMath.HcRandom.Next(-60, 10))).ToRotationVector2() * HcMath.HcRandom.Next(23, 32);
                        Vector2 spanOffset = MathHelper.ToRadians(20).ToRotationVector2() * 30 + new Vector2(13, -13 + HcMath.HcRandom.Next(-13, 0));
                        if (NPC.direction < 0)
                        {
                            vr = (toTarget.ToRotation() + MathHelper.ToRadians(HcMath.HcRandom.Next(-10, 60))).ToRotationVector2() * HcMath.HcRandom.Next(23, 32);
                            spanOffset = MathHelper.ToRadians(-20).ToRotationVector2() * 30 + new Vector2(-68, -13 + HcMath.HcRandom.Next(-13, 0));
                        }

                        int proj = Projectile.NewProjectile(AiBehavior.GetEntitySource_Parent(NPC), NPC.Center + spanOffset, vr, ProjectileID.Bone, (int)(NPC.damage * 1.5f), NPC.knockBackResist);
                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                        Main.projectile[proj].scale = HcMath.HcRandom.Next(15, 25) / 10f;
                    }

                    if (NPC.frameCounter == 9)
                    {
                        ThisTimeValue = 0;
                        Behavior = 0;
                    }
                }
                if (Behavior == 2)
                {
                    if (ThisTimeValue < 45)
                    {
                        Vector2 toPos = target.Center + new Vector2(0, -600);
                        AiBehavior.ChasingBehavior(NPC, toPos, 32, 16);
                    }
                    else if (ThisTimeValue < 120)
                    {
                        NPC.velocity.X = 0f;
                        NPC.Center += new Vector2(0, 33);
                        Vector2 ceedTilePos = AiBehavior.WEPosToTilePos(NPC.Bottom + new Vector2(0, 16));
                        Tile tile = TileHelper.GetTile(ceedTilePos);

                        if (NPC.Bottom.Y >= target.Center.Y || tile.HasSolidTile())
                        {
                            ThisTimeValue = 0;
                            Behavior = 3;
                        }
                    }
                    else
                    {
                        ThisTimeValue = 0;
                        Behavior = 3;
                    }
                }
                if (Behavior == 3)
                {
                    NPC.velocity = Vector2.Zero;

                    for (int i = 0; i < 13; i++)
                    {
                        Vector2 vr = (MathHelper.TwoPi / 13f * i).ToRotationVector2() * 13f;
                        int proj = Projectile.NewProjectile(AiBehavior.GetEntitySource_Parent(NPC), NPC.Center, vr, ProjectileID.Bone, NPC.damage * 2, NPC.knockBackResist);
                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                        Main.projectile[proj].scale = 3f;
                    }
                    for (int i = 0; i < 63; i++)
                    {
                        Vector2 vr = HcMath.GetRandomVevtor(0, 360, HcMath.HcRandom.Next(23, 32));
                        int dust = Dust.NewDust(NPC.Center, 64, 64, DustID.Bone, vr.X, vr.Y);
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 3f;
                    }
                    for (int i = 0; i < 63; i++)
                    {
                        Vector2 vr = HcMath.GetRandomVevtor(0, 360, HcMath.HcRandom.Next(3, 5));
                        int dust = Dust.NewDust(NPC.Center, 64, 64, DustID.Bone, vr.X, vr.Y);
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 5f;
                    }
                    ThisTimeValue = 0;
                    Behavior = 0;
                }
              

                if (toTarget.LengthSquared() >= AttackRange)
                {
                    ThisTimeValue = 0;
                    Behavior = 0;
                    Status = 0;
                }
            }

            if (Status == 2)
            {
                NPC.velocity = Vector2.Zero;

                if (AiBehavior.PlayerAlive(target))
                {
                    Status = 0;
                }
            }

            CoilTile();
        }

        public void CoilTile()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Vector2 offSet = new Vector2(i * NPC.direction, -1 - j) * 16;
                    Vector2 tilePos = AiBehavior.WEPosToTilePos(NPC.Bottom + offSet);
                    Tile tile = TileHelper.GetTile(tilePos);
                    if (tile.HasSolidTile() && Main.fpsCount == 30)
                    {
                        NPC.velocity = new Vector2(0, -16);
                    }
                }
            }
            
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D mainValue = DrawUtils.GetT2DValue(Texture);
            SpriteEffects spriteEffects = NPC.direction > 0 ? SpriteEffects.FlipHorizontally :  SpriteEffects.None;

            Main.EntitySpriteDraw(
                mainValue, 
                DrawUtils.WDEpos(NPC.Center), 
                DrawUtils.GetRec(mainValue, (int)NPC.frameCounter, 10), 
                drawColor, 
                NPC.rotation, 
                DrawUtils.GetOrig(mainValue, (int)NPC.frameCounter, 10), 
                NPC.scale,
                spriteEffects, 
                0
                );

            if (Status == 1 && Behavior == 2)
            {
                for (int i = 0; i < NPC.oldPos.Length - 1; i++)
                {
                    Vector2 oldPos = NPC.oldPos[i] + (NPC.Center - NPC.position);
                    float oldConengSnum = i / (float)NPC.oldPos.Length;
                    Main.EntitySpriteDraw(
                        mainValue,
                        DrawUtils.WDEpos(oldPos),
                        DrawUtils.GetRec(mainValue, (int)NPC.frameCounter, 10),
                        drawColor * (1 - oldConengSnum) * 0.5f,
                        NPC.rotation,
                        DrawUtils.GetOrig(mainValue, (int)NPC.frameCounter, 10),
                        NPC.scale,
                        spriteEffects,
                        0
                        );
                }
            }
            return false;
        }
    }
}
