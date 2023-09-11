using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;

namespace LegendMod.misc 
{
    public class NeuronIntegration:ModItem
    {
        public override void SetStaticDefaults()
        {
             // DisplayName.SetDefault("Neuron integration");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = 100000;
            Item.rare = 8;
        
        }
    }
}