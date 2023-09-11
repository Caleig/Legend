using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.Items.Weapons
{
    public class MoltenDragonBreathCannon:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 82;
            Item.height = 38;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.knockBack = 5;
            Item.value = 100000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.DD2FlameBurstTowerT3Shot;
            Item.useAmmo = AmmoID.Rocket;
            Item.shootSpeed = 55f;

        
        }
    }
}