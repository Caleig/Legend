using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.NPCs 
{
    public class ProbeRobot:ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Probe Robot");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 120/4;
            NPC.damage = 10;
            NPC.lifeMax = 100;
            NPC.defense = 1;
            NPC.knockBackResist = 1f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 55;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            Main.npcFrameCount[NPC.type] = 4;
            NPC.noGravity = false;
            NPC.aiStyle = 3;
            AIType = 3;
            AnimationType = 3;
            NPC.noTileCollide = false;
            NPC.boss=false;
        
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(spawnInfo.Player.ZoneOverworldHeight)
            return 0.2f;

            return 0f;

        
        }
    }
}