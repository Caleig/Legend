using LegendMod.Common.AuxiliaryMeans;
using LegendMod.Common.DrawTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LegendMod.Bosses
{
    [AutoloadBossHead]
    public class Shieldfish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 12;
            NPCID.Sets.TrailingMode[Type] = 1;
            NPCID.Sets.TrailCacheLength[Type] = 16;
            base.SetStaticDefaults();
            // DisplayName.SetDefault("盾皮鱼");
        }

        public override void SetDefaults()
        {
            NPC.width = 192;
            NPC.height = 114;
            NPC.damage = 30;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 365;
            NPC.lifeMax = 2150;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.noGravity = true;
            
        }

        public override string BossHeadTexture => "LegendMod/Bosses/Shieldfish_Head_Boss";
        public override string Texture => "LegendMod/Bosses/Shieldfish";

        public int Status
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public int ThisTimeValue
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }

        public override void OnKill()
        {
            base.OnKill();
        }

        public override bool CheckDead()
        {
            return base.CheckDead();
        }

        Vector2 SprintVr = Vector2.Zero;

        float FirstStageWanderSpeed => 9f;//决定一阶段的游走速度
        float FirstStageSprintSpeed => 29f;//决定一阶段的冲刺速度
        int FirstStageWanderTime => 60;//决定一阶段一个游走周期的持续时间
        int FirstStageSprintTime => 29;//决定一阶段一个冲刺周期的持续时间，时间越长便会让其冲刺距离越远

        int TwoStageReadyTime => 30;//决定二阶段的冲刺等待时间
        int TwoStageSprintTime => 60;//决定二阶段的一个冲刺周期的持续时间，时间越长便会让其冲刺距离越远

        public override void AI()
        {
            if (Main.fpsCount % 5 == 0) NPC.frameCounter++;
            if (NPC.frameCounter > 11) NPC.frameCounter = 0;
            ThisTimeValue++;

            Player target = AiBehavior.FindingTarget(NPC, -1);
            if (target == null)
            {
                NPC.life = 0;
                NPC.active = false;
            }

            Vector2 ToTargVr = target.Center - NPC.Center;
            float TotargLengSp = AiBehavior.GetEntityDgSquared(NPC, target);
            float NewLifeConeng = NPC.life / (float)NPC.lifeMax;

            if (NewLifeConeng < 0.35f && Status != 3)
            {
                Status = 2;
            }
            if (Status != 3)
            {
                NPC.alpha = 255;
            }

            if (Status == 0)
            {
                if (TotargLengSp > 120 * 120) AiBehavior.ChasingBehavior(NPC, target.Center, FirstStageWanderSpeed, 16);
                else AiBehavior.AccelerationBehavior(NPC, target.Center, 0.02f);

                if (ThisTimeValue > FirstStageWanderTime)
                {
                    ThisTimeValue = 0;
                    Status = 1;
                }
            }
            if(Status == 1)
            {
                if (ThisTimeValue <= 1)
                {
                    SprintVr = (ToTargVr + target.velocity * 10f).SafeNormalize(Vector2.Zero) * FirstStageSprintSpeed;
                }

                NPC.velocity = SprintVr;

                if (ThisTimeValue > FirstStageSprintTime)
                {
                    ThisTimeValue = 0;
                    Status = 0;
                }
            }
            if (Status == 2)
            {
                ThisTimeValue = 0;
                Status = 3;
            }
            if (Status == 3)
            {
                int readyTime = TwoStageReadyTime;
                int sprintTime = TwoStageSprintTime;
                if (ThisTimeValue <= 1)
                {
                    NPC.alpha = 0;
                    Vector2 RandomVrPos = target.Center + HcMath.GetRandomVevtor(0, 360, HcMath.HcRandom.Next(170, 360));
                    NPC.Center = RandomVrPos;
                }
                else if (ThisTimeValue <= readyTime)
                {
                    NPC.velocity = Vector2.Zero;
                    NPC.rotation = ToTargVr.ToRotation() + MathHelper.Pi;
                    NPC.alpha += (int)( 255/ (float)readyTime);

                    if (ThisTimeValue % 15 == 0)
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            Vector2 Vr = (MathHelper.TwoPi / 64f * i).ToRotationVector2() * 23f;
                            int dust = Dust.NewDust(NPC.Center, 16, 16, DustID.DungeonWater, Vr.X, Vr.Y);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 2f;
                        }
                    }                   
                }
                else if (ThisTimeValue < readyTime + 3)
                {
                    SprintVr = (ToTargVr + target.velocity * 15f).SafeNormalize(Vector2.Zero) * 39f;
                }
                else if (ThisTimeValue < readyTime + 3 + sprintTime)
                {
                    NPC.velocity = SprintVr;
                }
                else
                {
                    ThisTimeValue = 0;
                }
            }

            AiBehavior.NPCToRot(NPC, NPC.velocity.ToRotation() + MathHelper.Pi, 0.15f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D mainValue = DrawUtils.GetT2DValue(Texture);
            Texture2D Men = DrawUtils.GetT2DValue("LegendMod/Asset/Masking/BigGreyscaleCircle");
            SpriteEffects spriteEffects = NPC.direction > 0? SpriteEffects.FlipVertically : SpriteEffects.None;

            if (Status != 0)
            {
                for (int i = 0; i < NPC.oldPos.Length - 1; i++)
                {
                    Vector2 oldPos = NPC.oldPos[i] + (NPC.Center - NPC.position);
                    float oldConengSnum = i / (float)NPC.oldPos.Length;
                    Main.EntitySpriteDraw(mainValue, DrawUtils.WDEpos(oldPos), DrawUtils.GetRec(mainValue, (int)NPC.frameCounter, 12), drawColor * (0.5f - oldConengSnum / 2f) * (NPC.alpha / 255f), NPC.rotation, DrawUtils.GetOrig(mainValue, (int)NPC.frameCounter, 12), NPC.scale - oldConengSnum / 3f, spriteEffects, 0);
                }
            }

            Main.EntitySpriteDraw(mainValue, DrawUtils.WDEpos(NPC), DrawUtils.GetRec(mainValue, (int)NPC.frameCounter, 12), drawColor * (NPC.alpha/255f), NPC.rotation, DrawUtils.GetOrig(mainValue, (int)NPC.frameCounter, 12), NPC.scale, spriteEffects, 0);
            return false;
        }
    }

    /*             _ooOoo_
                  o8888888o
                  88" . "88
                  (| -_- |)
                  O\  =  /O
               ____/`---'\____
             .'  \\|     |//  `.
            /  \\|||  :  |||//  \
           /  _||||| -:- |||||-  \
           |   | \\\  -  /// |   |
           | \_|  ''\---/''  |   |
           \  .-\__  `-`  ___/-. /
         ___`. .'  /--.--\  `. . __
      ."" '<  `.___\_<|>_/___.'  >'"".
     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
     \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======
                   `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    */
}