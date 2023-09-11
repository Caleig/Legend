using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace LegendMod.Buffs
{
    public class Vine : ModBuff
    {
        Texture2D texture = ModContent.Request<Texture2D>("LegendMod/Buffs/Vine").Value;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            base.SetStaticDefaults();
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity -= npc.velocity / 2;
            base.Update(npc, ref buffIndex);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            NPC nPC = Main.npc[buffIndex];
            spriteBatch.Draw(texture, new Vector2(nPC.position.X, nPC.position.Y - nPC.height + texture.Height), Color.White);
            return base.PreDraw(spriteBatch, buffIndex, ref drawParams);
        }
    }
}