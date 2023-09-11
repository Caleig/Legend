using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.NPCs
{
    public class Alligator:ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alligator");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 122;
            NPC.height = 112/4;
            NPC.damage = 100;
            NPC.lifeMax = 400;
            NPC.defense = 35;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 700;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            Main.npcFrameCount[NPC.type] = 4;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.boss=false;
        
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(Main.hardMode)
            if(spawnInfo.Player.ZoneJungle)
            return 0.05f;

            return 0f;

        
        }
    }
}