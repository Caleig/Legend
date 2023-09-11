using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework;

namespace LegendMod
{
    public class swordlight : BTLcProj
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.tileCollide = false; 
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5 ;
            Projectile.light = 0.5f; 
            Projectile.friendly = false;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Center = target.Center;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}