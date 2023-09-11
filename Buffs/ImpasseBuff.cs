using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace LegendMod.Buffs
{
    public class ImpasseBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;//退出世界后不会消失
        }

        public override string Texture => "LegendMod/Asset/Buffs/ImpasseBuff";

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = 0;
        }
    }

    public class BuffModPlayer : ModPlayer
    {
        public NPC.HitInfo hitInfo = new NPC.HitInfo();
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Main.LocalPlayer.HasBuff(ModContent.BuffType<ImpasseBuff>())) ;
            {
                hitInfo.Damage = (int)(hurtInfo.Damage * 1.5f);
                npc.StrikeNPC(hitInfo);
            }
            base.OnHitByNPC(npc, hurtInfo);
        }
    }
}
