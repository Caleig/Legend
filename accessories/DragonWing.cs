using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class DragonWing : ModItem 
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            base.SetStaticDefaults();
            base.SetDefaults();
            Item.width = 36;
            Item.height = 36;
            Item.value = 150000;
            Item.rare = 3;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual) 
        {
            base.UpdateAccessory(player, hideVisual);
            player.wingTimeMax = 90;
        }
        public override void AddRecipes() 
        {
            base.AddRecipes();
        
            
        
        }
    }
}