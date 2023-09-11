using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using LegendMod.Common.AuxiliaryMeans;
using LegendMod.Common.DrawTools;
using LegendMod.Common.WorldGeneration;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LegendMod.NPCs
{
    public class SkeletonGiant : ModNPC
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
            NPC.scale = 1;
            NPC.damage = 52;
            NPC.defense = 12;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 320;
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
            NPC.lifeMax = 320;
            NPC.damage = 38;
            if (bossAdjustment == 1)
            {
                NPC.lifeMax = 580;
                NPC.damage = 48;
            }
            if (bossAdjustment == 0.85f)
            {
                NPC.lifeMax = 700;
                NPC.damage = 58;
            }
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

                    if (ThisTimeValue > 30)
                    {
                        ThisTimeValue = 0;
                        Behavior = 1;
                    }
                }
                if (Behavior == 1)
                {
                    DrawUtils.ClockFrame(ref NPC.frameCounter, 10, 9, 7);

                    NPC.velocity.X = NPC.direction * AiBehavior.AsymptoticVelocity(new Vector2(NPC.Center.X, 0), new Vector2(target.Center.X, 0), 2f, 16);

                    if (NPC.frameCounter == 8 && Main.fpsCount % 2 == 0)
                    {
                        Vector2 vr = (toTarget.ToRotation() + MathHelper.ToRadians(HcMath.HcRandom.Next(-30, 10))).ToRotationVector2() * HcMath.HcRandom.Next(13, 20);
                        Vector2 spanOffset = MathHelper.ToRadians(20).ToRotationVector2() * 30 + new Vector2(13, -5 + HcMath.HcRandom.Next(-13, 0));
                        if (NPC.direction < 0)
                        {
                            vr = (toTarget.ToRotation() + MathHelper.ToRadians(HcMath.HcRandom.Next(-10, 30))).ToRotationVector2() * HcMath.HcRandom.Next(13, 20);
                            spanOffset = MathHelper.ToRadians(-20).ToRotationVector2() * 30 + new Vector2(-48, -5 + HcMath.HcRandom.Next(-13, 0));
                        }

                        int proj = Projectile.NewProjectile(AiBehavior.GetEntitySource_Parent(NPC), NPC.Center + spanOffset, vr, ProjectileID.Bone, (int)(NPC.damage * 0.75f), NPC.knockBackResist);
                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                        Main.projectile[proj].scale = HcMath.HcRandom.Next(8, 15) / 10f;
                    }

                    if (NPC.frameCounter == 9)
                    {
                        ThisTimeValue = 0;
                        Behavior = 0;
                    }
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
                        NPC.velocity = new Vector2(0, -8);
                    }
                }
            }

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D mainValue = DrawUtils.GetT2DValue(Texture);
            SpriteEffects spriteEffects = NPC.direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

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

            return false;
        }
    }
}
